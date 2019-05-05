using UnityEngine;

namespace Tasks
{
    [CreateAssetMenu(fileName = "RandomEvent", menuName = "Tasks/Events/Random")]
    public class EventSettingsBase : ScriptableObject
    {
        public TaskSettingsBase taskSettings;

#pragma warning disable CS0649
        [SerializeField]
        protected float _weight;
#pragma warning restore CS0649

        public virtual bool isRandom { get {return true;} }

        public virtual float GetCalculatedWeight()
        {
            return _weight;
        }

        public virtual bool CheckConditions()
        {
            return true;
        }
    }
}