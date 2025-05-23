using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance { get; private set; }
    public GameObject PauseMenuUI;
    public bool isPaused = false;
    private bool isResuming = false;
    private float inputBlockDuration = 0.5f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        PauseMenuUI.SetActive(false);
    }

    void Update()
    {
        if (isResuming)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) && isPaused)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ContinueWithDelay();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Continue()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void ContinueWithDelay()
    {
        if (isResuming) return;
        isResuming = true;
        StartCoroutine(ResumeAfterDelay(inputBlockDuration));
    }

    public IEnumerator ResumeAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        isResuming = false;
    }
}