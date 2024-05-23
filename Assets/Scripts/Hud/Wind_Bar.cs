using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wind_Bar : MonoBehaviour
{
    private Slider slider;
    public Gradient colorGradient;
    [SerializeField] int Temp = 100;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void Start()
    {

    }

    void Update()
    {
        // Обновляем значение Temp в зависимости от значения слайдера
        slider.value = Temp;
    }
}

