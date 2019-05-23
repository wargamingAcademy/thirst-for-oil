using System;
using UnityEngine;

namespace Assets.Scripts.SoundManager
{
    /// <summary>
    /// Скрипт для массовой загрузки звуков. В будущем можно дополнить SoundManager чтобы он воспроизводил
    /// сразу целый набор звуков из этого класса + добавить возможность выставлять задержку перед воспроизведением для каждого звука отдельно.
    /// Тогда можно будет делать сложные звуки на основе простых прямо в редакторе.
    /// </summary>
    public class SoundPack:MonoBehaviour
    {
        [SerializeField]
        private Audio[] sounds;

        private SoundManager soundManager;
        public void Awake()
        {
            var soundManager=FindObjectOfType<SoundManager>();
            if (sounds != null)
            {
                soundManager.RegisterSoundPack(sounds);
            }
          
        }

      /*  public void Awake()
        {
            var soundManager = FindObjectOfType<SoundManager>();
            if (sounds != null)
            {
                soundManager.RegisterSoundPack(sounds);
            }
        }*/
    }
    [Serializable]
    public class Audio
    {
        public AudioClip audioClip;
        public string name;
    }
}
