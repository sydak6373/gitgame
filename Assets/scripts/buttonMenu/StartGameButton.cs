using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class StartGameButton : MonoBehaviour
{
    public GameObject startGameButton;
    public static void StartGame()
    {
        

        SceneManager.LoadScene("level1");
        Time.timeScale = 1.0f;
        GamePause.GameIsPause = false;
    }

    public void Update()
    {
        
        if (SaveSystem.isSaved) startGameButton.SetActive(true);
        else startGameButton.SetActive(false);
    }

    public static void IsSavedUpdateTrue()
    {
        SaveSystem.isSaved = true;
    }

    public static void IsSavedUpdateFalse()
    {
        SaveSystem.DeletePlayer();
    }
    public void Start()
    {
        if (PlayerPrefs.HasKey("isSaved")) SaveSystem.isSaved = true;
        else SaveSystem.isSaved = false;
    }

}
