using UnityEngine;
using UnityEditor;

public class TurnController : MonoBehaviour
{
    public delegate void MethodContainer();
    public static event MethodContainer TurnEndEvent;
    public static event MethodContainer TurnStartEvent;
    public void Awake()
    {
    }
    public void EndTurn()
    {
        TurnEndEvent();
        TurnStartEvent();
    }
}