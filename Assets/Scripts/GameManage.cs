using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class GameManage : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] TextMeshProUGUI enemyCounter;
    int totalCount;
    int totalRemaining;
    public bool levelCleared;

    [Header("Gems")]
    [SerializeField] TextMeshProUGUI gemCounter;
    int gemsCollected;

    [Header("Level Completed")]
    [SerializeField] GameObject levelCompletedPanel;
    [SerializeField] TMP_Text levelNumber;
    [SerializeField] TMP_Text levelCompletedGems;
    [SerializeField] CanvasGroup levelCompleteCanvasGroup;
    [SerializeField] float levelCompleteDelay = 3f;

    [Header("Level Failed")]
    [SerializeField] GameObject levelFailedPanel;

    PlayerHealth player;
    AudioSource audioSource;
    PlayerWallet wallet;

    void Start()
    {
        EnemyMovement[] totalEnemies = FindObjectsOfType<EnemyMovement>();
        wallet = FindObjectOfType<PlayerWallet>();

        totalCount = totalEnemies.Length;
        levelCleared = false;

        levelCompletedPanel.SetActive(false);
        levelCompleteCanvasGroup.alpha = 0;

        player = FindObjectOfType<PlayerHealth>();

        audioSource = GetComponent<AudioSource>();

        levelFailedPanel.SetActive(false);
    }

    void Update()
    {
        UpdateUI();
        if (levelCleared)
        {
            UnlockNewLevel();
            Invoke("DisplayLevelComplete", levelCompleteDelay);
        }

        if (player.hitPoints == 0)
        {
            Invoke("GameOverDelay", 2f);
        }
    }

    void UpdateUI()
    {
        EnemyCounter();
        GemCounter();
    }

    void GameOverDelay()
    {
        levelFailedPanel.SetActive(true);
    }

    private void EnemyCounter()
    {
        EnemyMovement[] totalEnemies = FindObjectsOfType<EnemyMovement>();
        totalRemaining = totalEnemies.Length;
        if (totalRemaining == 0)
        {
            levelCleared = true;
        }
        enemyCounter.text = totalRemaining.ToString() + "/" + totalCount.ToString();
    }

    private void GemCounter()
    {
        gemCounter.text = wallet.GetCurrentGems().ToString();
    }

    void DisplayLevelComplete()
    {
        if (levelCompleteCanvasGroup.alpha == 0)
        {
            audioSource.Play();
        }
        if (levelCompleteCanvasGroup.alpha < 1)
        {
            levelCompletedPanel.SetActive(true);
            levelCompleteCanvasGroup.alpha += Time.deltaTime;
        }
        levelNumber.text = SceneManager.GetActiveScene().name.ToString();
        levelCompletedGems.text = wallet.GetCurrentGems().ToString();
    }

    void UnlockNewLevel()
    {

        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }

    public void enemycount(GameObject killed,Vector3 killpos)
    {
        EnemyMovement[] totalEnemies = FindObjectsOfType<EnemyMovement>();
        List<GameObject> temp = new List<GameObject>();
        GameObject[] trial = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < trial.Length; i++)
        {
            if(trial[i]!=killed)
            {
                temp.Add(trial[i]);
            }
            //Debug.Log("the name is " + totalEnemies.GameObject.name);
        }
        Debug.Log("the count of enemy" +temp.Count);
        
        foreach (GameObject item in temp)
        {
            float temp_distance = Vector3.Distance(item.transform.position,killpos);
            if(temp_distance <6f)
            {
                Debug.LogError("the distance of "+item.name+" is: "+temp_distance);
                item.GetComponent<EnemyMovement>().StayIdle();
                StartCoroutine(item.GetComponent<EnemyMovement>().checkthenoise(killpos));
                //item.GetComponent<EnemyMovement>().StartCoroutine(checkthenoise(killpos));
                
            }
            
        }
    }
    
}
