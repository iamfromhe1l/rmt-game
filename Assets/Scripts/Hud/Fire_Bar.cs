using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class Fire_Bar : MonoBehaviour
{
    private Slider slider;
    public Gradient colorGradient;
    [SerializeField] int Temp = 100;
    [SerializeField] private MagicFire fire;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void Start()
    {

    }

    void Update()
    {
        slider.value = fire.GetCurrentTimeOut() / fire.GetTimeOut();
        Debug.Log(fire.GetTimeOut());
        Debug.Log(fire.GetCurrentTimeOut());
    }
}
