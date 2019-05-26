using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseEvents : MonoBehaviour
{
    public GameObject winDialogue;
    public GameObject loseDialogue;

    // Start is called before the first frame update
    void Start()
    {
        ResourceManager.LoseEvent += Lose;
        ResourceManager.WinEvent += Win;
        winDialogue.SetActive(false);
        loseDialogue.SetActive(false);
    }

    public void Win()
    {
        winDialogue.SetActive(true);
    }

    public void Lose()
    {
        loseDialogue.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Reload()
    {
        SceneManager.UnloadScene("SampleScene");
        SceneManager.LoadScene("SampleScene");        
    }
}
