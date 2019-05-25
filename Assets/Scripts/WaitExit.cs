using Gamekit2D;
using UnityEngine;
//Временное решение

/// <summary>
/// Скрипт для выхода из игры
/// </summary>
public class WaitExit : MonoBehaviour
{
    void Update()
    {
        if (PlayerInput.Instance.Exit.Down)
        {
            Application.Quit();
        }
    }
}