using System;
using System.IO;
using System.Net;
using UnityEngine;
using System.IO.Compression;

public class ZipDownloader : MonoBehaviour
{
    private string zipUrl = "http://13.201.128.65/huntandroid/Android.zip"; // URL of the zip file to download
    [SerializeField] SplashScreen splash;
    private string zipFileName = "Android.zip"; // Name of the zip file
    private string zipFilePath; // Full path of the downloaded zip file

    private void Start()
    {
        // Start the download process
        DownloadZipFile();
    }

    private void DownloadZipFile()
    {
        // Combine the persistent data path with the zip file name to get the full file path
        zipFilePath = Path.Combine(Application.persistentDataPath, zipFileName);

        // Create a new WebClient instance to download the zip file
        WebClient webClient = new WebClient();

        // Subscribe to the DownloadFileCompleted event to handle completion
        webClient.DownloadFileCompleted += DownloadCompletedCallback;

        try
        {
            // Start downloading the zip file asynchronously
            webClient.DownloadFileAsync(new Uri(zipUrl), zipFilePath);
        }
        catch (Exception e)
        {
            Debug.LogError("Error downloading zip file: " + e.Message);
        }
    }

    private void DownloadCompletedCallback(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
    {
        // Check if the download completed successfully
        if (e.Error == null)
        {
            Debug.Log("Zip file downloaded successfully.");

            // Unzip the downloaded file
            UnzipFile();

            // Delete the downloaded zip file
            DeleteDownloadedZipFile();
        }
        else
        {
            Debug.LogError("Error downloading zip file: " + e.Error.Message);
        }
    }

    private void UnzipFile()
    {
        // Check if the zip file exists
        if (File.Exists(zipFilePath))
        {
            // Unzip the file to the persistent data path
            ZipFile.ExtractToDirectory(zipFilePath, Application.persistentDataPath);
            Debug.Log("Zip file unzipped successfully.");
            StartCoroutine(splash.FadeInAndOut());
        }
        else
        {
            Debug.LogError("Zip file not found at path: " + zipFilePath);
        }
    }

    private void DeleteDownloadedZipFile()
    {
        // Check if the zip file exists
        if (File.Exists(zipFilePath))
        {
            // Delete the zip file
            File.Delete(zipFilePath);
            Debug.Log("Downloaded zip file deleted successfully.");
        }
        else
        {
            Debug.LogError("Zip file not found at path: " + zipFilePath);
        }
    }
}
