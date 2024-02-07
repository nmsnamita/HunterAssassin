using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UIElements;

public class GameData : MonoBehaviour
{
    public List<int> retrieved = new List<int>();
    // Start is called before the first frame update
    private string SaveFilePath
    {
        get { return Application.persistentDataPath + "/playerdata.nms"; }
    }
    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        if(File.Exists(SaveFilePath))
        {
            Debug.Log("Fetching data");
            readingfrombinary();
        }
        else
        {
            initialdata();
            
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
        List<int> temp = new List<int>();
        temp.Add(0);
        PlayerData data = new PlayerData(0,1,0,temp);

        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(SaveFilePath,FileMode.Create);
        
        bf.Serialize(stream,data);
    }
    public static void savingdata(List<int> owned)
    {
        PlayerData data = new PlayerData(PlayerPrefs.GetInt("PlayerType"),PlayerPrefs.GetInt("UnlockedLevel"),PlayerPrefs.GetInt("GemCount"),owned);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/playerdata.nms",FileMode.Create);

        bf.Serialize(stream,data);
        stream.Close();

        //FileStream stream = new FileStream(SaveFilePath,FileMode.Create);
    }
    // public static void leveldata()
    // {
    //     PlayerData data = new PlayerData(PlayerPrefs.GetInt("PlayerType"),PlayerPrefs.GetInt("UnlockedLevel"),PlayerPrefs.GetInt("GemCount"),owned);
    //     BinaryFormatter bf = new BinaryFormatter();
    //     FileStream stream = new FileStream(Application.persistentDataPath + "/playerdata.nms",FileMode.Create);

    //     bf.Serialize(stream,data);
    //     stream.Close();

    //     //FileStream stream = new FileStream(SaveFilePath,FileMode.Create);
    // }
    public void leveldata()
    {
        if(File.Exists(SaveFilePath))
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream stream = new FileStream(SaveFilePath,FileMode.Open);
                PlayerData data = bf.Deserialize(stream) as PlayerData;
                stream.Close();

                retrieved = data.have;
            }
            catch (System.Exception e)
            {
                Debug.Log("error in fetching the data " + e);
            }
        }
        PlayerData nd = new PlayerData(PlayerPrefs.GetInt("PlayerType"),PlayerPrefs.GetInt("UnlockedLevel"),PlayerPrefs.GetInt("GemCount"),retrieved);
        BinaryFormatter af = new BinaryFormatter();
        FileStream beam = new FileStream(Application.persistentDataPath + "/playerdata.nms",FileMode.Create);

        af.Serialize(beam,nd);
        beam.Close();

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
                retrieved = data.have;
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
    public List<int> have;

    public PlayerData (int player_id,int levls,int gems,List<int> owned)
    {
        Player_Character = player_id;
        levels_Completed = levls;
        gems_id = gems;
        have = owned;
    }
    
}
