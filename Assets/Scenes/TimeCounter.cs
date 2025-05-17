using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    [SerializeField] private DayNightCycle dayNightCycle;
    [SerializeField] private TextMeshProUGUI daysCounter;
    [SerializeField] private TextMeshProUGUI timeCounter;
    [SerializeField] private float hoverDelay = 0.5f;

    private int currentDay = 0;
    private float hoverTimer = 0f;
    private bool isHovering = false;
    private float elapsedTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if (timeCounter != null)
            timeCounter.gameObject.SetActive(false);
        UpdateDayCounter();
    }

    // Update is called once per frame
    void Update()
    {
        if (dayNightCycle == null) return;

        elapsedTime += Time.deltaTime;
        float dayTime = elapsedTime % dayNightCycle.dayLength;
        int newDay = Mathf.FloorToInt(elapsedTime / dayNightCycle.dayLength);

        if (newDay != currentDay)
        {
            currentDay = newDay;
            UpdateDayCounter();
        }
        HandleHover();
    }
    private void HandleHover()
    {
        if (daysCounter != null && timeCounter != null)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(
                daysCounter.rectTransform,
                Input.mousePosition))
            {
                if (!isHovering)
                {
                    isHovering = true;
                    hoverTimer = 0f;
                }

                hoverTimer += Time.deltaTime;
                if (hoverTimer >= hoverDelay)
                {
                    timeCounter.gameObject.SetActive(true);
                    float currentTime = elapsedTime % dayNightCycle.dayLength;
                    UpdateTimeCounter(currentTime);
                }
            }
            else
            {
                isHovering = false;
                timeCounter.gameObject.SetActive(false);
            }
        }
    }
    private void UpdateDayCounter()
    {
        if (daysCounter != null)
            daysCounter.text = $"Day {currentDay}";
    }
    private void UpdateTimeCounter(float dayTime)
    {
        if (timeCounter != null && dayNightCycle != null)
        {
            float totalHours = (dayTime / dayNightCycle.dayLength) * 24f;
            int hours = Mathf.FloorToInt(totalHours);
            int minutes = Mathf.FloorToInt((totalHours - hours) * 60f);
            timeCounter.text = $"{hours:00}:{minutes:00}";
        }
    }
}
