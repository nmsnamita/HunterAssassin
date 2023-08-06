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
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }
}
