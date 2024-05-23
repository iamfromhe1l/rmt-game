using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class Wind_Bar : MonoBehaviour
{
    private Slider slider;
    public Gradient colorGradient;
    [SerializeField] int Temp = 100;
    [SerializeField] private MagicWind wind;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void Start()
    {

    }

    void Update()
    {
        slider.value = wind.GetCurrentTimeOut() / wind.GetTimeOut();
    }
}

