using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using TMPro;
using JetBrains.Annotations;




public class SplashScreen : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float timeToFade;
    [SerializeField] float afterFadeDelay = 0.1f;
    //[SerializeField] AddressableAssetGroup maingroup;
    [SerializeField] AssetReference[] scenes; 
    [SerializeField] Slider loading;
    [SerializeField] TextMeshProUGUI loading_txt;
    [SerializeField] Text valuetxt;
    [SerializeField] Text scenecomplete;
    [SerializeField] TextMeshProUGUI completedscene;
    //[SerializeField] private AssetReferenceGroup  groupName;

    public bool fadeIn = false;
    public bool fadeout = false;
    int currentSceneIndex;

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //PrintGroupName();
        //DownloadScenesInGroup(groupName);
        //StartCoroutine(FadeInAndOut());

        //PlayerPrefs.SetInt("lives",3);
        //StartCoroutine(DownloadDependencies());
        scenecomplete.text = "false";
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

    public IEnumerator FadeInAndOut()
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
    // void DownloadScenesInGroup(string groupName)
    // {
    //     AsyncOperationHandle downloadHandle = Addressables.DownloadDependenciesAsync();

    //     downloadHandle.Completed += OnDownloadCompleted;
    // }
    void OnDownloadCompleted(AsyncOperationHandle handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("All scenes in Addressable group downloaded successfully.");
        }
        else
        {
            Debug.LogError("Failed to download scenes in Addressable group: " + handle.OperationException);
        }
    }
    int count =0;
    IEnumerator DownloadDependencies()
    {
        //StartCoroutine(tryonce());
        // doone();
        // yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < scenes.Length; i++)
        {
            AsyncOperationHandle handle = Addressables.DownloadDependenciesAsync(scenes[i]);
            
            // Wait for the download operation to complete
            yield return new WaitForSeconds(0.1f);
            loading.value = handle.GetDownloadStatus().Percent;
            float temp = handle.GetDownloadStatus().Percent*100;
            loading_txt.text = "Downloading: " + temp+"%";
            Debug.Log("just for j=github push");
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
    private AsyncOperationHandle SceneHandle;
    void doone()
    {
        SceneHandle = Addressables.DownloadDependenciesAsync(scenes[0]);
        StartCoroutine(loadingpercentage());
        SceneHandle.Completed += OnSceneLoaded;

    }
    private void OnSceneLoaded(AsyncOperationHandle obj)///this scene is also taken from the rpyal luck project
    {
        scenecomplete.text = "True";
        StartCoroutine(FadeInAndOut());
        if(obj.Status == AsyncOperationStatus.Succeeded)
        {  
            Debug.Log("Success");
            //SceneManager.LoadScene(currentSceneIndex + 1);
            
            //Addressables.LoadSceneAsync(AddressableScene, UnityEngine.SceneManagement.LoadSceneMode.Single, true);
        }
    }
    IEnumerator loadingpercentage()
    {
        
        //yield return handle;
        float temp = (SceneHandle.GetDownloadStatus().Percent)*100f;
        Debug.Log("the value is "+ temp);
        valuetxt.text = temp.ToString();
        loading.value = SceneHandle.GetDownloadStatus().Percent;;
        string temps = temp.ToString("F2");
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(loadingpercentage());
        loading_txt.text = "Downloading: " + temps+"%";
    }

    void doneafew(AsyncOperationHandle obj)
    {
        if(obj.Status == AsyncOperationStatus.Succeeded)
        {  
            //loading.value  = count;
            Debug.Log("Count value"+ count);
            float percentage = (count/360f)*100;
            //int temp = (int)percentage;
            //float temp = (count/360) * 100;
            completedscene.text = ("Completed("+count+"/360)").ToString();
            string temp = percentage.ToString("F2");
            //loading_txt.text = "Downloading: " + temp+"%";//("Downloading : " +temp+"%").ToString();
            //Debug.Log("Success" + percentage);
            if(count >=360)
            {
                loading_txt.text = "Download Complete";
                StartCoroutine(FadeInAndOut());

            }
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
