using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq.Expressions;

using System;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] TMP_Text gemCountText;

    [Header("Reward Buttons")]
    [SerializeField] RewardsButton button1;
    [SerializeField] RewardsButton button2;
    [SerializeField] RewardsButton button3;
    public GameObject playerprefab;
    [SerializeField] Material[] shopextras;
    [SerializeField] Image[] heartui;
    [SerializeField] GameObject timer_ui;
    private TMP_Text timer;

    public int gemCount;

    void Start()
    {
        button1.Initialize(this);
        button2.Initialize(this);
        button3.Initialize(this);
        timer = timer_ui.GetComponent<TextMeshProUGUI>();
        gemCount = PlayerDataManager.LoadGemCount();
        gemCountText.text = gemCount.ToString();
        displayingmaterials();
        changingplayerskin();
        //int temp = PlayerPrefs.GetInt("lives");
        for (int i = 0; i < heartui.Length; i++)
        {
            int temp = PlayerPrefs.GetInt("lives");
            if(i < temp)
            {
                heartui[i].gameObject.SetActive(true);
            }
            else
            {
                heartui[i].gameObject.SetActive(false);
            }
        }
        regeneratelives();
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
        //Debug.LogError("the player has" + list.Length+" materials attached to itself");
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
    void regeneratelives()
    {
        int temp = PlayerPrefs.GetInt("lives");
        if(temp <5)
        {
            string stored = PlayerPrefs.GetString("savedtimer");
            DateTime startTime = DateTime.Parse(stored);
            DateTime endTime = System.DateTime.Now;
            System.TimeSpan timeDifference = endTime - startTime;
            Debug.Log("TimeDifference is :"+ timeDifference.Minutes);
            if(timeDifference.Minutes <30)
            {
                //start tiem timer with the value
            }
            else
            {
                timer_ui.SetActive(true);
                int divided = timeDifference.Minutes/30;
                int newlives = temp + divided;
                if(divided >=5)
                {
                    divided = 5;
                }
                else
                {
                    int justfornow = timeDifference.Minutes;
                    int remainder = justfornow-(30*divided);
                }
            }
        }
    }
    IEnumerator starttimer(int x,int y)
    {
        int min = x;
        int sec = y;
        if((sec >0) && min >0)
        {
            sec--;
        }
        else if(sec == 0 && min >0)
        {
            sec =59;
            min --;
        }
        else if(sec ==0 && min ==0)
        {
            StopCoroutine(starttimer(0,0));
        }
        string showtimer = (min +"M "+ sec+"s").ToString();
        timer.text = showtimer;
        yield return new WaitForSecondsRealtime(1f);
        StartCoroutine(starttimer(min,sec));
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
    public void testing()
    {
        PlayerPrefs.SetInt("lives",5);
    }
}
