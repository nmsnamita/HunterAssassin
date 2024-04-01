using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Xml.Serialization;
//using UnityEngine.UIElements;

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
        try
        {
            showad();
        }
        catch (System.Exception e)
        {
            Debug.Log("this is the error"+e);
            throw;
        }
        //BlackImage();
        //loader.LoadScene(levelnumber);
        //SceneManager.LoadScene(currentSceneIndex + 1);
    }
    void BlackImage()
    {
        // Get screen width and height
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;

        // Create a new black Texture2D with screen width and height
        Texture2D blackTexture = new Texture2D(screenWidth, screenHeight);

        // Fill the texture with black color
        Color[] pixels = new Color[screenWidth * screenHeight];
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = Color.black;
        }
        blackTexture.SetPixels(pixels);
        blackTexture.Apply();

        // Create a new GameObject with a Renderer component to display the black image
        GameObject blackImageGO = new GameObject("BlackImage");
        SpriteRenderer spriteRenderer = blackImageGO.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = Sprite.Create(blackTexture, new Rect(0, 0, screenWidth, screenHeight), new Vector2(0.5f, 0.5f));
        blackImageGO.transform.position = new Vector3(screenWidth / 2f, screenHeight / 2f, 0f);
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
