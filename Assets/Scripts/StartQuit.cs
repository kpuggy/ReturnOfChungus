using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartQuit : MonoBehaviour
{

    public void PlayGame ()

    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void PlayGame(int level)

    {
        SceneManager.LoadScene(level);
    }
    public void QuitGame ()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
        
    

}

