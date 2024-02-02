using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Xml.Serialization;

public class LevelManager : MonoBehaviour
{
    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int levelnumber = ExtractNumber(SceneManager.GetActiveScene().name);
        //string temp = SceneManager.GetActiveScene().name;
        Debug.Log("the name of the scene is"+levelnumber);
        SceneLoader loader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
        loader.LoadScene(levelnumber);
        //SceneManager.LoadScene(currentSceneIndex + 1);
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
        temp =- 1;
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
