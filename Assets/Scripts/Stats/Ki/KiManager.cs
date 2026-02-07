using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KiManager : MonoBehaviour
{
    public float maxKi = 100f;
    public float currentKi;
    public float regenRate = 5f;
    public Slider kiSlider; 

    // Start is called before the first frame update
    void Start()
    {
        currentKi = maxKi;
        if (kiSlider != null) kiSlider.maxValue = maxKi;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentKi < maxKi)
        {
            currentKi += regenRate * Time.deltaTime;
            UpdateUI();
        }
    }
    public bool CanUseKi(float amount) => currentKi >= amount;

    public void ConsumeKi(float amount)
    {
        currentKi -= amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (kiSlider != null) kiSlider.value = currentKi;
    }
}
