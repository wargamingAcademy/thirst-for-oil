using UnityEngine;

/// <summary>
/// Скрипт отвечающий за события окончания/начала хода
/// </summary>
public class TurnController : MonoBehaviour
{
    public delegate void MethodContainer();
    public static event MethodContainer TurnEndEvent;
    public static event MethodContainer TurnStartEvent;

    public void EndTurn()
    {
        TurnEndEvent?.Invoke();
        TurnStartEvent?.Invoke();
    }
}