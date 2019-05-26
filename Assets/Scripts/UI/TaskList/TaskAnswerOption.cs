using UnityEngine;
using PoolsManagement;
using UnityEngine.UI;
using TMPro;

public class TaskAnswerOption : PoolableObject
{
#pragma warning disable CS0649
    [SerializeField]
    TMP_Text _text;
    [SerializeField]
    Button _button;
#pragma warning restore CS0649

    int _answerIndex;

    public void SetAllowed(bool allowed)
    {
        _button.interactable = allowed;
    }

    public void SetValues(int index, string text)
    {
        _answerIndex = index;
        _text.text = text;
    }

    public void OnClickSubscribe(System.Action<int> callback)
    {
        _button.onClick.AddListener(() => callback(_answerIndex));
    }

    public override void ReturnToPool()
    {
        _button.onClick.RemoveAllListeners();
        base.ReturnToPool();
    }
}
