using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class ShopMenu : MonoBehaviour
{
    public GameObject shopMenuUI;
    private bool isShopUIActive;

    private void Start()
    {
        isShopUIActive = false;
        shopMenuUI.SetActive(false);
    }

    void Update()
    {
        if (!PauseMenu.GameIsPaused)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                Console.WriteLine(isShopUIActive);
                if (isShopUIActive) CloseShop(); else OpenShop();
            }
        }
    }
    
    void OpenShop()
    {
        shopMenuUI.SetActive(true);
        isShopUIActive = true;
        Time.timeScale = 0f;
    }
    void CloseShop()
    {
        shopMenuUI.SetActive(false);
        isShopUIActive = false;
        Time.timeScale = 1f;
    }
}