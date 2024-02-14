using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;



public class SplashScreen : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float timeToFade;
    [SerializeField] float afterFadeDelay = 1f;

    public bool fadeIn = false;
    public bool fadeout = false;
    int currentSceneIndex;

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //PlayerPrefs.SetInt("lives",3);
        StartCoroutine(FadeInAndOut());
        StartCoroutine(downloadassets());
    }

    private void Update()
    {
        if (fadeIn == true)
        {
            if (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += timeToFade * Time.deltaTime;
                if (canvasGroup.alpha >= 1)
                {
                    fadeIn = false;
                }
            }
        }
        if (fadeout == true)
        {
            if (canvasGroup.alpha >= 0)
            {
                canvasGroup.alpha -= timeToFade * Time.deltaTime;
                if (canvasGroup.alpha == 0)
                {
                    fadeout = false;
                }
            }
        }
    }

    IEnumerator FadeInAndOut()
    {
        FadeOut();
        yield return new WaitForSeconds(afterFadeDelay);
        FadeIn();
        yield return new WaitForSeconds(afterFadeDelay);
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    IEnumerator downloadassets()
    {
        AsyncOperationHandle downloadHandle = Addressables.DownloadDependenciesAsync("Hunterassassian");
        yield return downloadHandle;
        if (downloadHandle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("Scenes downloaded successfully.");
        }
        else
        {
            Debug.LogError("Failed to download scenes: " + downloadHandle.Status);
        }

    }
    void PrintGroupName()
    {
        // Get the Addressable asset settings
        AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;

        if (settings != null && settings.groups.Count > 0)
        {
            // Get the name of the first (and only) group
            string groupName = settings.groups[0].Name;
            
            // Print the group name
            Debug.Log("Addressable Group Name: " + groupName);
        }
        else
        {
            Debug.LogError("No Addressable groups found.");
        }
    }

    public void FadeIn()
    {
        fadeIn = true;
    }

    public void FadeOut()
    {
        fadeout = true;
    }

}
