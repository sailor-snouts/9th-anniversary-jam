using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public void Title()
    {
        SceneManager.LoadScene("Title");
    }
    
    public void PlayGame()
    {
        SceneManager.LoadScene("Main");
    }
    
    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
          Application.Quit();
#endif
    }
}
