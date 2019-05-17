using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string LoadingSceneName;
    public string HelpSceneName;

    public void LoadStart() {
        SceneManager.LoadScene(LoadingSceneName);
    }

    public void IsHelp()
    {
        print("도움말!");
        SceneManager.LoadScene(HelpSceneName);
    }

    public void IsExit() {
        print("종료!");
        Application.Quit();
    }
    
}
