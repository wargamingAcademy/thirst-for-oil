using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Tasks;

public class EventGenerator : MonoBehaviour
{
    public MenuEvent eventMenu; // Todo: use menu manager for access

#pragma warning disable CS0649
    [SerializeField]
    float _cumulativeWeightThreshold;
    [SerializeField]
    EventSettingsBase[] _events;
#pragma warning restore CS0649

    float[] _cumulativeWeights;
    bool _initialized;

    void Awake()
    {
        Init();
        TurnController.TurnStartEvent += GenerateEvents;
    }

    public void Init()
    {
        if (_initialized)
            return;
        _cumulativeWeights = new float[_events.Length];
        _initialized = true;
    }

    private void OnDestroy()
    {
        TurnController.TurnStartEvent -= GenerateEvents;
    }

    [ContextMenu("GenerateEvents")]
    void GenerateEvents()
    {
        var newEvents = new List<TaskSettingsBase>();

        for (int i = 0; i < _events.Length; i++)
        {
            EventSettingsBase eventItem = _events[i];
            if (!eventItem.CheckConditions())
                continue;
            if (!eventItem.isRandom)
            {
                newEvents.Add(eventItem.taskSettings);
                continue;
            }
            _cumulativeWeights[i] += eventItem.GetCalculatedWeight();
        }

        EventSettingsBase betterRandomEvent = null;
        float betterWeight = 0f;
        int betterEventIndex = 0;
        for (int i = 0; i < _events.Length; i++)
        {
            if (_cumulativeWeights[i] > betterWeight)
            {
                betterWeight = _cumulativeWeights[i];
                betterRandomEvent = _events[i];
                betterEventIndex = i;
            }
        }

        if (betterRandomEvent != null && betterWeight >= _cumulativeWeightThreshold)
        {
            newEvents.Add(betterRandomEvent.taskSettings);
            _cumulativeWeights[betterEventIndex] = 0f;
        }

        StartCoroutine(ProcessEventQueue(newEvents));
    }

    IEnumerator ProcessEventQueue(List<TaskSettingsBase> tasksSettings)
    {
        foreach (var taskSetting in tasksSettings)
        {
            ITask newTask = TaskManager.instance.AddTask(taskSetting);
            eventMenu.ShowMenu(newTask);
            yield return new WaitUntil(() => eventMenu.isShowed);
        }
    }
}
