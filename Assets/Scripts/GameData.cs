using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameData : MonoBehaviour
{
    // Start is called before the first frame update
    private string SaveFilePath
    {
        get { return Application.persistentDataPath + "/playerdata.nms"; }
    }
    void Start()
    {
        if(File.Exists(SaveFilePath))
        {
            Debug.Log("Fetching data");
        }
        else
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void initialdata()
    {
        PlayerPrefs.GetInt("GemCount",0);
        PlayerPrefs.GetInt("PlayerType",0);
        PlayerPrefs.GetInt("UnlockedLevel", 1);
        PlayerData data = new PlayerData(0,1,0);

        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(SaveFilePath,FileMode.Create);
        
        bf.Serialize(stream,data);
    }
    public static void savingdata()
    {
        PlayerData data = new PlayerData(PlayerPrefs.GetInt("PlayerType"),PlayerPrefs.GetInt("UnlockedLevel"),PlayerPrefs.GetInt("GemCount"));
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/playerdata.nms",FileMode.Create);

        bf.Serialize(stream,data);

        //FileStream stream = new FileStream(SaveFilePath,FileMode.Create);
    }
    public void readingfrombinary()
    {
        if(File.Exists(SaveFilePath))
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream stream = new FileStream(SaveFilePath,FileMode.Open);
                PlayerData data = bf.Deserialize(stream) as PlayerData;
                stream.Close();

                PlayerPrefs.SetInt("GemCount",data.gems_id);
                PlayerPrefs.SetInt("PlayerType",data.Player_Character);
                PlayerPrefs.SetInt("UnlockedLevel",data.levels_Completed);

            }
            catch (System.Exception e)
            {
                Debug.Log("error in fetching the data " + e);
            }
        }
    }


}
[System.Serializable]
public class PlayerData
{
    public int Player_Character;
    public int levels_Completed;
    public int gems_id;

    public PlayerData (int player_id,int levls,int gems)
    {
        Player_Character = player_id;
        levels_Completed = levls;
        gems_id = gems;
    }
    
}
