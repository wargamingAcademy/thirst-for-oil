using PoolsManagement;
using UnityEngine;

namespace Tasks
{
    [CreateAssetMenu(fileName = "BasicTask", menuName = "Tasks/BasicTask")]
    public class BasicTaskSettings : TaskSettingsBase
    {
        public BasicTaskProgress[] progress;

        public override ITask CreateNewTask()
        {
            return new BasicTask();
        }

        [System.Serializable]
        public class BasicTaskProgress
        {
            public string key;
            public string shortDescription;
            [TextArea]
            public string description;
            public AnswerOptionSet[] answerOptions;
        }
    }

    [System.Serializable]
    public class AnswerOptionSet
    {
        public string key;
        [TextArea]
        public string text;
        public PoolSettingsSO poolSet;
    }
}
