using UnityEngine;

namespace Assets.Scripts.SoundManager
{
    public class SoundPlayer:MonoBehaviour
    {
        private SoundManager soundManager;
        public void Awake()
        {
            soundManager = FindObjectOfType<SoundManager>();
            soundManager.PlaySound("tbs",1f,true);
        }
        
    }
}
