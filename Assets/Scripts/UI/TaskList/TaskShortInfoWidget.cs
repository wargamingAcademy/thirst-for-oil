using UnityEngine;
using TMPro;
using Tasks;
using UnityEngine.UI;
using PoolsManagement;

public class TaskShortInfoWidget : PoolableObject
{
    public int index => _index;
    [SerializeField]
    TMP_Text _Text;
    [SerializeField]
    Button _button;

    int _index;
    ITask _task;

    public void Initialize(int index, ITask task)
    {
        _index = index;
        _task = task;
        InfoUpdate(_task);
    }

    public void OnClickSubscribe(System.Action<ITask> callback)
    {
        _button.onClick.AddListener(() => callback(_task));
    }

    public override void ReturnToPool()
    {
        _button.onClick.RemoveAllListeners();
        base.ReturnToPool();
    }

    public void InfoUpdate(ITask task)
    {
        _Text.text = task.GetTaskShortInfo(); // Todo: format text
    }
}
