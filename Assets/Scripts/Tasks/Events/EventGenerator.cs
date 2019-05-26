using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Tasks;

public class EventGenerator : MonoBehaviour
{
    public static EventGenerator instance => _instance;

    static EventGenerator _instance;
    public MenuEvent eventMenu; // Todo: use menu manager for access

#pragma warning disable CS0649
    [SerializeField]
    float _cumulativeWeightThreshold;
    [SerializeField]
    EventSettingsBase[] _events;
#pragma warning restore CS0649

    float[] _cumulativeWeights;
    bool _initialized;
    List<DelayedEvent> _delayedEvents;

    void Awake()
    {
        if (_instance != null)
        {
            Debug.LogError("Multiple EventGenerators");
            Destroy(this);
            return;
        }
        _instance = this;
        Init();
        TurnController.TurnStartEvent += GenerateEvents;
    }

    public void Init()
    {
        if (_initialized)
            return;
        _cumulativeWeights = new float[_events.Length];
        _delayedEvents = new List<DelayedEvent>();
        _initialized = true;
    }

    private void OnDestroy()
    {
        if (_instance == this)
            _instance = null;
        TurnController.TurnStartEvent -= GenerateEvents;
    }

    public void AddDelayedEvent(int delay, TaskSettingsBase taskSet)
    {
        _delayedEvents.Add(new DelayedEvent
        {
            updatesLeft = delay,
            taskSettings = taskSet
        });
    }

    public void RunEventDirectly(TaskSettingsBase taskSet)
    {
        var newEvents = new List<TaskSettingsBase>();
        newEvents.Add(taskSet);
        StartCoroutine(ProcessEventQueue(newEvents));
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

        List<DelayedEvent> removeDelayedEvents = null;
        foreach (var delayedEvent in _delayedEvents)
        {
            if (delayedEvent.updatesLeft <= 0)
            {
                newEvents.Add(delayedEvent.taskSettings);
                if (removeDelayedEvents == null)
                    removeDelayedEvents = new List<DelayedEvent>();
                removeDelayedEvents.Add(delayedEvent);
            }
            delayedEvent.updatesLeft--;
        }

        if (removeDelayedEvents != null)
            foreach (var item in removeDelayedEvents)
                _delayedEvents.Remove(item);

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
        yield return new WaitWhile(() => eventMenu.isShowed);
        foreach (var taskSetting in tasksSettings)
        {
            ITask newTask = TaskManager.instance.AddTask(taskSetting);
            eventMenu.ShowMenu(newTask);
            yield return new WaitWhile(() => eventMenu.isShowed);
        }
    }

    class DelayedEvent
    {
        public int updatesLeft;
        public TaskSettingsBase taskSettings;
    }
}
