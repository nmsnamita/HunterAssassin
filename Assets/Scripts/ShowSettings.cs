using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSettings : MonoBehaviour
{
    [SerializeField] GameObject settingsPanel;

    private void Start()
    {
        settingsPanel.SetActive(false);
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        PauseGame();
    }

    public void CloseSettings()
    {
        StartCoroutine(UnPauseGame());
    }

    IEnumerator UnPauseGame()
    {
        ResumeGame();
        yield return new WaitForSeconds(.1f);
        settingsPanel.SetActive(false);
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
    }

    void ResumeGame()
    {
        Time.timeScale = 1f;
    }
}
