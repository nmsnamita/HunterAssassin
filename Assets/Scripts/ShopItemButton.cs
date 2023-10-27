using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemButton : MonoBehaviour
{
    MainMenuUIManager mainMenuUIManager;

    public int gemValue;

    void Start()
    {
        mainMenuUIManager = FindObjectOfType<MainMenuUIManager>();
    }

    void Update()
    {
        if (mainMenuUIManager.gemCount <= gemValue)
        {
            gameObject.GetComponent<Button>().interactable = false;
        }
    }
}
