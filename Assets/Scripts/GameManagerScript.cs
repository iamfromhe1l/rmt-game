using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public GameObject gameOverUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameOver();
        }
    }

    public void gameOver()
    {
        gameOverUI.SetActive(true);
    }
    public void Respawn()
    { 
        Debug.Log("Respawn");
        gameOverUI.SetActive(false);
        SceneManager.LoadScene("CastleScene");
    }
    public void GoMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
