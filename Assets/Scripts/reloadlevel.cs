using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class reloadlevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void reload()
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
}
