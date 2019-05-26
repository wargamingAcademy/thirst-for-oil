using UnityEngine;

namespace Tasks
{
    [CreateAssetMenu(fileName = "RandomEvent", menuName = "Tasks/Events/Random")]
    public class EventSettingsBase : ScriptableObject
    {
        public TaskSettingsBase taskSettings;

#pragma warning disable CS0649
        [SerializeField]
        protected float _minWeight;
        [SerializeField]
        protected float _maxWeight;
#pragma warning restore CS0649

        public virtual bool isRandom { get {return true;} }

        public virtual float GetCalculatedWeight()
        {
            return Random.Range(_minWeight, _maxWeight);
        }

        public virtual bool CheckConditions()
        {
            return true;
        }
    }
}