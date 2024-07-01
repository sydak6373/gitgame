using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePause : MonoBehaviour
{
    public static bool GameIsPause = false;
    public GameObject PauseMenuUI;

    private void Start()
    {
        DethState.deth += DethUpdate;

    }

    void Update()
    {
       
      if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPause = true;
       
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsPause = false;
       
        
    }

    private void DethUpdate()
    {
        Debug.Log("начало смерти");
        transform.Find("DethPanel").gameObject.SetActive(true);
        StartCoroutine(DethCoroutine());
         
    }

    private IEnumerator DethCoroutine()
    {
        
        yield return new WaitForSeconds(5f);
        StartGameButton.StartGame();

    }

    private void OnDestroy()
    {
        DethState.deth -= DethUpdate;
    }

}
