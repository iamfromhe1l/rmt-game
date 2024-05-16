using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.Find("Panel").gameObject.SetActive(false);
        SceneManager.LoadScene("CastleScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
