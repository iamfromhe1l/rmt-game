using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Sounds;

public class GameOver : MonoBehaviour
{

    private SoundsController soundsController;

    public GameObject deathScreenUI;

    public static bool GameIsOver = false;

    private void Awake()
    {
        soundsController = GetComponent<SoundsController>();
        //подписаться на событие смерти у класса Person    
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPlayerDeath();
        }
    }

    public void OnPlayerDeath()
    {
        Debug.Log("You dead");
        GameIsOver = true;
        deathScreenUI.SetActive(true);
        soundsController.PlaySound(soundsController.sounds[0]);
    }

    public void Respawn()
    {
        deathScreenUI.SetActive(false);
        SceneManager.LoadScene("CastleScene");
    }
    public void GoMenu()
    {
        deathScreenUI.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }
}