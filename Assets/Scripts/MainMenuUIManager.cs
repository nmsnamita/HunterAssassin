using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] TMP_Text gemCountText;

    public int gemCount;

    void Start()
    {
        gemCount = PlayerDataManager.LoadGemCount();
        gemCountText.text = gemCount.ToString();
    }

    void Update()
    {
        gemCountText.text = gemCount.ToString();
    }

    public void IncreaseGems()
    {
        gemCount += FindObjectOfType<ShopItemButton>().gemValue;
        PlayerDataManager.SaveGemCount(gemCount);
    }

    public void DecreaseGems()
    {
        gemCount -= FindObjectOfType<ShopItemButton>().gemValue;
        PlayerDataManager.SaveGemCount(gemCount);
    }
}
