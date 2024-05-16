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

    public GameManagerScript gameManager;

    public static bool GameIsOver = false;

    private void Awake()
    {
        soundsController = GetComponent<SoundsController>();
        OnPlayerDeath();
        //подписаться на событие смерти у класса Person    
    }
    void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void OnPlayerDeath()
    {
        Debug.Log("You dead");
        GameIsOver = true;
        gameObject.SetActive(true);
        soundsController.PlaySound(soundsController.sounds[0]);
    }
}