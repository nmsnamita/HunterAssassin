using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    [SerializeField] Button[] buttons;
    public GameObject levelButtons;
    public Sprite unselected;

    private void OnEnable()
    {
        ButtonsToArray();
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        Debug.Log("Levels reached"+ unlockedLevel);
        for (int i = 0; i < unlockedLevel; i++)
        {
            buttons[i].interactable = true;
           // Debug.Log("Success");
        }
        // for (int i = 0; i < buttons.Length; i++)
        // {
        //     buttons[i].interactable = false;
        // }
        // for (int i = 0; i < unlockedLevel; i++)
        // {
        //     buttons[i].interactable = true;
        // }
    }
    private void Update() {
        // if (Input.GetKeyDown(KeyCode.M))
        // {
        //     PlayerPrefs.SetInt("UnlockedLevel",100);
        // }
    }

    public void OpenLevel(int levelId)
    {
        string levelName = "Level " + levelId;
        SceneManager.LoadScene(levelName);
    }

    void ButtonsToArray()
    {
        int childCount = levelButtons.transform.childCount;
        buttons = new Button[childCount];
        for (int i = 0; i < childCount; i++)
        {
            buttons[i] = levelButtons.transform.GetChild(i).gameObject.GetComponent<Button>();
        }
    }
}
