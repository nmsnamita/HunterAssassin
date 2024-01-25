using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq.Expressions;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] TMP_Text gemCountText;

    [Header("Reward Buttons")]
    [SerializeField] RewardsButton button1;
    [SerializeField] RewardsButton button2;
    [SerializeField] RewardsButton button3;
    public GameObject playerprefab;
    [SerializeField] Material[] shopextras;

    public int gemCount;

    void Start()
    {
        button1.Initialize(this);
        button2.Initialize(this);
        button3.Initialize(this);

        gemCount = PlayerDataManager.LoadGemCount();
        gemCountText.text = gemCount.ToString();
        displayingmaterials();
        changingplayerskin();
    }

    void Update()
    {
        gemCountText.text = gemCount.ToString();
    }

    public void IncreaseGems(RewardsButton button)
    {
        gemCount += button.gemValue;
        PlayerDataManager.SaveGemCount(gemCount);
    }

    public void DecreaseGems()
    {
        gemCount -= FindObjectOfType<ShopItemButton>().gemValue;
        PlayerDataManager.SaveGemCount(gemCount);
    }
    public void displayingmaterials()
    {
        GameObject child = playerprefab.transform.GetChild(1).gameObject;
        Material[] list  = child.GetComponent<SkinnedMeshRenderer>().sharedMaterials;
        Debug.LogError("the player has" + list.Length+" materials attached to itself");
        for (int i = 0; i < list.Length; i++)
        {
            Debug.Log(list[i].name);
        }
    }
    void changingplayerskin()
    {
        GameObject child = playerprefab.transform.GetChild(1).gameObject;
        Material[] list  = child.GetComponent<SkinnedMeshRenderer>().sharedMaterials;
        int temp = PlayerPrefs.GetInt("PlayerType");
        list[1] = shopextras[temp];
        list[5] = shopextras[temp];
        list[8] = shopextras[temp];
        child.GetComponent<SkinnedMeshRenderer>().sharedMaterials =list;
        changepayerfacts();
    }

    void changepayerfacts()
    {
        int temp = PlayerPrefs.GetInt("PlayerType");
        if(temp%2 == 0)//playerprefs was even
        {
            float initialspeed =playerprefab.GetComponent<PlayerMovement>().playerMoveSpeed;
            switch (temp)
            {
                
                case 0:initialspeed = initialspeed;
                    break;
                case 2: initialspeed = initialspeed+(initialspeed*0.1f);
                    break;
                case 4:initialspeed = initialspeed+(initialspeed*0.25f);
                    break;
                case 6:initialspeed = initialspeed+(initialspeed*0.5f);
                    break;
            }
            playerprefab.GetComponent<PlayerMovement>().playerMoveSpeed =initialspeed;
        }
        else
        {
            float health = playerprefab.GetComponent<PlayerHealth>().hitPoints;
            switch (temp)
            {
                
                case 1:health = health;
                    break;
                case 3: health = health+(health*0.1f);
                    break;
                case 5:health = health+(health*0.25f);
                    break;
                case 7:health = health+(health*0.5f);
                    break;
            }
            playerprefab.GetComponent<PlayerHealth>().hitPoints = health;

        }
    }
}
