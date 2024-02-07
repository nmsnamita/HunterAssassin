using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Slider loadingBar;

    //////////reference of below taken  from sceneloading script of royal luck
    private AsyncOperationHandle SceneHandle;
    [SerializeField]
    private AssetReference AddressableScene;
    [SerializeField]
    private Slider LoadingSlider;
    [SerializeField]

    //change from here to make if addressable scene changes goes improper
    public AssetReference[] demoscenes;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        loadingScreen.SetActive(false);
    }
    private void startscene(AssetReference record)//this reference is also taken from the royal luck project
    {
        // GameObject spawn =Instantiate(loadingScreen);
        // GameObject ui = GameObject.Find("Game Canvas");
        // spawn.transform.SetParent(ui.transform);
        //Debug.LogError("starting the loading scene");
        SceneHandle = Addressables.DownloadDependenciesAsync(record);
        
        loadingScreen.SetActive(true);
        StartCoroutine(loadingpercentage());
        SceneHandle.Completed += OnSceneLoaded;
    }

    private void OnSceneLoaded(AsyncOperationHandle obj)///this scene is also taken from the rpyal luck project
    {
        if(obj.Status == AsyncOperationStatus.Succeeded)
        {  
            Debug.Log("Success");
            Addressables.LoadSceneAsync(AddressableScene, UnityEngine.SceneManagement.LoadSceneMode.Single, true);
        }
    }
    void selectfrombunch(int levelindex)
    {
        for (int i = 0; i < demoscenes.Length; i++)
        {
            //demoscenes[
        }
    }

    public void LoadScene(int levelIndex)
    {
        startscene(demoscenes[levelIndex]);
        AddressableScene = demoscenes[levelIndex];

        //StartCoroutine(LoadSceneAsynchronously(levelIndex));
    }

    IEnumerator LoadSceneAsynchronously(int levelIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            loadingBar.value = operation.progress;
            yield return null;
        }
    }
    IEnumerator loadingpercentage()
    {
        loadingBar.value = SceneHandle.GetDownloadStatus().Percent;
        //Debug.Log("////////////////////////////"+ SceneHandle.GetDownloadStatus().Percent);
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(loadingpercentage());
    }

}
