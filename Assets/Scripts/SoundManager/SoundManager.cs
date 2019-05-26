using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.SoundManager
{
    /// <summary>
    /// Т.к. у нас звуки будут искаться по названию, наиболее подходящая для этого структура - Dictionary.
    /// Но мы очевидно хотим храним и по несколько копий одного звука, что Dictionary сделать не позволяет.
    /// Поэтому в Dictionary в качестве значения будет хранится звуковая запись а также список индексов,
    /// а копии звуков будут храниться в листе. Сложность поиска в List по индексу - константа.
    /// Этот класс идеально подходит для паттерна Singleton, но почему я его не использую? :
    /// 1. Сложности с юнит- тестированием
    /// 2. Мы не сможем иметь два экземпляра класса. Не знаю зачем,но вдруг когда-нибудь понадобится?
    /// </summary>
    public class SoundManager : MonoBehaviour
    {
        private const float MAX_DISTANCE=2000;
        public int GetCountSoundsPlaying
        {
            get { return sounds.Count; }
        }

        private Dictionary<string, AudioStructure> soundsDictionary;
        private List<Sound> sounds;
        public delegate void SoundEndPlaying();
        public static event SoundEndPlaying SoundsChangePlayingCountEvent;

        public void Awake()
        {
            soundsDictionary=new Dictionary<string, AudioStructure>();
            sounds=new List<Sound>();
        }

        public void Update()
        {
            for(var i=0;i<sounds.Count;i++)
            {
                if (!sounds[i].source.isPlaying)
                {                   
                    soundsDictionary.TryGetValue(sounds[i].name, out var audioStructure);
                    if (audioStructure != null)
                    {
                        audioStructure.soundIndexes.Remove(i);
                    }
                    sounds.Remove(sounds[i]);
                  //  SoundsChangePlayingCountEvent();
                }
            }
        }

        public void Play3DSound(string soundName, GameObject target, bool isMoveWithObject, float volume = 1f, bool isLooped = false)
        {
            if (string.IsNullOrEmpty(soundName))
            {
                throw new ArgumentNullException("Sound null or empty");
            }

            if (!soundsDictionary.TryGetValue(soundName, out var audioStructure))
            {
                throw new KeyNotFoundException();
            }
            GameObject soundGameObject = new GameObject("Sound: " + soundName);
            AudioSource soundSource = soundGameObject.AddComponent<AudioSource>();
            soundSource.clip = audioStructure.audioClip;
            soundGameObject.transform.parent = transform;
            soundSource.playOnAwake = false;
            soundSource.volume = volume;
            soundSource.loop = isLooped;
            soundSource.rolloffMode = AudioRolloffMode.Linear;
            soundSource.spatialBlend = 1;
            soundSource.dopplerLevel = 0;
            soundSource.reverbZoneMix = 0;
            soundSource.maxDistance = MAX_DISTANCE;
            soundGameObject.transform.position = target.transform.position;
            if (isMoveWithObject)
            {
                soundGameObject.transform.parent = target.transform;
            }             
            soundSource.Play();
            sounds.Add(new Sound(soundName, soundSource));
           // SoundsChangePlayingCountEvent();
        }
    
       public void PlaySound(string soundName, float volume = 1f, bool isLooped = false)
        {
            if (string.IsNullOrEmpty(soundName))
            {
                throw new ArgumentNullException("Sound null or empty");
            }

            if (!soundsDictionary.TryGetValue(soundName, out var audioStructure))
            {
                throw new KeyNotFoundException();
            }
            AudioSource soundSource = gameObject.AddComponent<AudioSource>();
            soundSource.clip = audioStructure.audioClip;
            soundSource.playOnAwake = false;
            soundSource.volume = volume;
            soundSource.loop = isLooped;
            soundSource.spatialBlend = 0;
            soundSource.Play();
            sounds.Add(new Sound(soundName,soundSource));
         //   SoundsChangePlayingCountEvent();
        }

       public void StopSound(int index)
       {
           if (index >= sounds.Count)
           {
                throw new ArgumentOutOfRangeException();
           }

           Sound sound = sounds[index];
           sound.source.Stop();
           Destroy(sound.source);
           soundsDictionary.TryGetValue(sound.name, out var audioStructure);
           if (audioStructure != null)
           {
               audioStructure.soundIndexes.Remove(index);
           }
           sounds.RemoveAt(index);
          // SoundsChangePlayingCountEvent();
        }

       public IEnumerable<string> GetPlayableSounds()
       {
           List<string> list=new List<string>();
           for(int i=0;i<sounds.Count;i++)
           {
               list.Add(sounds[i].name);
           }
           return list;
       }
  
            public void RegisterSoundPack(Audio[] sounds)
        {
            try
            {
                foreach (var sound in sounds)
                {
                    soundsDictionary.Add(sound.name,new AudioStructure(sound.audioClip));
                }
            }
            catch (ArgumentException e)
            {
                Debug.Log(e);
            }

        }
    }

    public class AudioStructure
    {
        public AudioClip audioClip;
        public List<int> soundIndexes;

        public AudioStructure(AudioClip audioClip)
        {
            this.audioClip = audioClip;
            soundIndexes=new List<int>();
        }       
    }
}


