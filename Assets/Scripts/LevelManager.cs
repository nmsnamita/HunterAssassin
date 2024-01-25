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
        foreach (char c in input)
        {
            // Check if the character is a digit
            if (char.IsDigit(c))
            {
                // Parse the digit and return the result
                return int.Parse(c.ToString());
            }
        }

        // If no digit is found, return a default value (you can modify this based on your requirement)
        return -1;
    }
    

    public void LoadDialogue()
    {
        SceneManager.LoadScene("Dialogue");
    }

    public void RestartGame()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int levelnumber = ExtractNumber(SceneManager.GetActiveScene().name);
        SceneLoader loader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
        loader.LoadScene(levelnumber-1);
        //SceneManager.LoadScene(currentSceneIndex);
        Time.timeScale = 1f;
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
