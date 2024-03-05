using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;




public class SplashScreen : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float timeToFade;
    [SerializeField] float afterFadeDelay = 1f;
    //[SerializeField] AddressableAssetGroup maingroup;
    [SerializeField] AssetReference[] scenes; 
    [SerializeField] Slider loading;

    public bool fadeIn = false;
    public bool fadeout = false;
    int currentSceneIndex;

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //PrintGroupName();

        //PlayerPrefs.SetInt("lives",3);
        StartCoroutine(FadeInAndOut());
        StartCoroutine(DownloadDependencies());
        //StartCoroutine(downloadassets());
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

    // void downloadassets()
    // {
    //     int status;
        
    //     for (int i = 0; i < scenes.Length; i++)
    //     {
    //         AsyncOperationHandle handle = Addressables.DownloadDependenciesAsync(scenes[i]);
    //         {
    //             if(handle.Status == AsyncOperationStatus.Succeeded)
    //             {
    //                 Debug.Log("its all done");

    //             }
    //             else
    //             {
    //                 Debug.LogError(handle.Status);
    //             }
    //             handle.Completed += doneafew;
    //         }
    //     }
        
        

    // }
    int count =0;
    IEnumerator DownloadDependencies()
    {
        for (int i = 0; i < scenes.Length; i++)
        {
            AsyncOperationHandle handle = Addressables.DownloadDependenciesAsync(scenes[i]);
            
            // Wait for the download operation to complete
            yield return handle;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                //Debug.Log("Dependencies downloaded successfully for scene: " + scenes[i]);
                count++;
            }
            else
            {
               // Debug.LogError("Failed to download dependencies for scene: " + scenes[i]);
            }

            // Register a callback for when the download operation completes
            handle.Completed += doneafew;

        }
    }

    void doneafew(AsyncOperationHandle obj)
    {
        if(obj.Status == AsyncOperationStatus.Succeeded)
        {  
            loading.value  = count;
            //Debug.Log("Success" + count);
            //Addressables.LoadSceneAsync(AddressableScene, UnityEngine.SceneManagement.LoadSceneMode.Single, true);
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
