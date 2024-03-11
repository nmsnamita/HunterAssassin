using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Xml.Serialization;
using UnityEngine.UIElements;

public class LevelManager : MonoBehaviour
{
    int nxtlvl;
    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int levelnumber = ExtractNumber(SceneManager.GetActiveScene().name);
        nxtlvl = levelnumber;
        //string temp = SceneManager.GetActiveScene().name;
        Debug.Log("the name of the scene is"+levelnumber);
        SceneLoader loader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
        int temo = PlayerPrefs.GetInt("UnlockedLevel");
        if (levelnumber+1 >temo)
        {
            PlayerPrefs.SetInt("UnlockedLevel",levelnumber+1);
            Debug.Log("playerprefs" + temo + " levelnumber"+levelnumber+1);
            GameObject.FindGameObjectWithTag("data").GetComponent<GameData>().leveldata();
            //GameData.leveldata();
        }
        showad();
        //loader.LoadScene(levelnumber);
        //SceneManager.LoadScene(currentSceneIndex + 1);
    }
    public void gotonext()
    {
        SceneLoader loader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
        loader.LoadScene(nxtlvl);
    }


    int ExtractNumber(string input)
    {
        // Loop through each character in the input string
        string temp ="";
        foreach (char c in input)
        {
            // Check if the character is a digit
            if (char.IsDigit(c))
            {
                // Parse the digit and return the result
                temp+=c;
                //return int.Parse(c.ToString());
            }
        }

        // If no digit is found, return a default value (you can modify this based on your requirement)
        return int.Parse(temp);
    }
    public void showad()
    {
        RewardedAdsButton adsobj = GameObject.FindGameObjectWithTag("Ads").GetComponent<RewardedAdsButton>();
        adsobj.ShowAd();
    }
    

    public void LoadDialogue()
    {
        SceneManager.LoadScene("Dialogue");
    }

    public void RestartGame()
    {
        if(PlayerPrefs.GetInt("lives") <=0)
        {
            return;
        }
        int temp = PlayerPrefs.GetInt("lives");
        temp-- ;
        PlayerPrefs.SetInt("lives",temp);
        gettinglives();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int levelnumber = ExtractNumber(SceneManager.GetActiveScene().name);
        SceneLoader loader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
        loader.LoadScene(levelnumber-1);
        //SceneManager.LoadScene(currentSceneIndex);
        Time.timeScale = 1f;
    }

    public void gettinglives()
    {
        System.DateTime timesaved = System.DateTime.Now;
        string stored = timesaved .ToString();
        PlayerPrefs.SetString("savedtimer",stored);
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
