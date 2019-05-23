using System;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
   public class Sound
    {
        public string name;
        public AudioSource source;

        public Sound(string name, AudioSource source)
        {
            this.name = name;
            this.source = source;
        }
    }
}
