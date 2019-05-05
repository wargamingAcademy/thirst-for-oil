using UnityEngine;
using Tasks;
using PoolsManagement;

public class MenuEvent : MonoBehaviour
{
#pragma warning disable CS0649
    [SerializeField]
    Transform _taskWidgetContainer;
#pragma warning restore CS0649

    BasicTaskWidget _taskWidget; // Todo: replace with interface
    ITask _task;

    public bool isShowed
    {
        get
        {
            return gameObject.activeSelf;
        }
    }

    public void ShowMenu(ITask task)
    {
        if (_taskWidget != null)
            _taskWidget.ReturnToPool();

        _task = task;

        PoolSettingsSO taskWidgetPoolSet = task.GetTaskWidgetPoolSet();
        _taskWidget = taskWidgetPoolSet.GetNewObject(_taskWidgetContainer) as BasicTaskWidget;
        _taskWidget.TaskUpdate(task);

        TaskManager.instance.OnTaskChangedSubscribe(OnTaskChanged);

        gameObject.SetActive(true); // Todo: run window show animation
        _taskWidget.gameObject.SetActive(true); // Todo: run widget show animation
    }

    public void HideMenu()
    {
        if (_taskWidget != null)
            _taskWidget.ReturnToPool();
        _taskWidget = null;
        _task = null;
        gameObject.SetActive(false); // Todo: run window hide animation

        if (TaskManager.instance != null)
            TaskManager.instance.OnTaskChangedUnscribe(OnTaskChanged);
    }

    void OnTaskChanged(TaskEventType eventType, ITask task)
    {
        if (task != _task)
            return;

        switch (eventType)
        {
            case TaskEventType.Added:
                ShowMenu(task);
                break;
            case TaskEventType.Changed:
                _taskWidget.TaskUpdate(task);
                break;
            case TaskEventType.Removed:
                HideMenu();
                break;
        }
    }

}
