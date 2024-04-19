using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class RemoteBundleSceneLoader : MonoBehaviour
{
    public string remoteUrl = "http://13.233.184.138/huntandroid/Android/"; // Remote URL of the Addressable scene bundle
    public string localFilePath = "Assets/AddressableLocal";
    [SerializeField] SplashScreen splash;
    [SerializeField] Slider slide;
    [SerializeField] TextMeshProUGUI download_txt;
    [SerializeField] Text testing;
    string folderName = "MyFolder";
    string folderPath;

    
    void Awake()
    {
        //Addressables.RuntimePath = Application.persistentDataPath;
    }

    void Start()
    {
        StartCoroutine(DownloadBundle());
        folderPath = Path.Combine(Application.persistentDataPath, folderName);

        // Check if the folder does not exist
        if (!Directory.Exists(folderPath))
        {
            // Create the folder
            Directory.CreateDirectory(folderPath);
            Debug.Log("Folder created at: " + folderPath);
        }
        Debug.Log("The path is "+  localFilePath);
    }

    IEnumerator DownloadBundle()
    {
        int i =0;
        using (UnityWebRequest webRequest = UnityWebRequest.Get(remoteUrl))
        {
            yield return webRequest.SendWebRequest();
            ulong totalBytes = webRequest.downloadedBytes; 
            Debug.Log("the toal size to download would be "+ totalBytes/1024);
            // while (!webRequest.isDone)
            // {
            //     float progress = webRequest.downloadProgress;
            //     slide.value = progress;
            //     float temp = progress * 100f;
            //     string temps = "Downloading : "+temp.ToString("F2")+"%";
            //     download_txt.text = temps; 
            //     Debug.Log("Download Progress: " + (progress * 100).ToString("F2") + "%");
            //     i++;
            //     testing.text = i.ToString();

            // // Optionally, yield to the next frame to avoid blocking the main thread
            //     yield return null;
            // }

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to download bundle: " + webRequest.error);
                Application.Quit();
            }
            else
            {
                //float downloadProgress = webRequest.downloadProgress;
                Debug.Log("Downlaod complete");
                SaveBundleToLocalFile(webRequest.downloadHandler.data);
            }
        }
    }

    void SaveBundleToLocalFile(byte[] data)
    {
        File.WriteAllBytes(folderPath, data);

        Debug.Log("Bundle downloaded and saved to: " + localFilePath);

        // Once the bundle is downloaded, load scenes from the bundle
        
        StartCoroutine(splash.FadeInAndOut());
        //LoadScenesFromBundle();
    }

    
    // IEnumerator UpdateProgressBar(AsyncOperationHandle<SceneInstance> handle)
    // {
    //     while (!handle.IsDone)
    //     {
    //         // Calculate the download progress percentage
    //         float progress = handle.PercentComplete;

    //         // Update the progress bar UI element
    //         progressBar.SetValue(progress);

    //         yield return null; // Wait for the next frame
    //     }

    //     // Download is complete, start the splash screen fade in/out
    //     StartCoroutine(splash.FadeInAndOut());
    // }
}
