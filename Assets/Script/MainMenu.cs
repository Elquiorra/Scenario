using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject MainMenuUI;
    private string Game = "Game";
    

    public void LoadGame(){
        Time.timeScale = 1f;
        SceneManager.LoadScene(Game);
    }

    public void QuitGame(){
        Application.Quit();
    }
}
