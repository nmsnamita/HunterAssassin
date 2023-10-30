using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemButton : MonoBehaviour
{
    [Header("Player Prefab")]
    [SerializeField] GameObject playerPrefab;

    [Header("Speed Powerup")]
    [SerializeField] float speedMultiplier;
    MainMenuUIManager mainMenuUIManager;

    [Header("Misc")]
    public int gemValue;

    private const string Speed1 = "Speed1.1";

    private void Awake()
    {

    }

    void Start()
    {
        mainMenuUIManager = FindObjectOfType<MainMenuUIManager>();
    }

    void Update()
    {
        if (mainMenuUIManager.gemCount <= gemValue && PlayerPrefs.GetFloat(Speed1, speedMultiplier) == speedMultiplier)
        {
            gameObject.GetComponent<Button>().interactable = false;
        }
    }

    public void SpeedUp()
    {
        playerPrefab.GetComponent<PlayerMovement>().playerMoveSpeed *= speedMultiplier;
        gameObject.GetComponent<Button>().interactable = false;
        PlayerPrefs.SetFloat(Speed1, speedMultiplier);
        PlayerPrefs.Save();
    }
}
