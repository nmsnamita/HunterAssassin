using System.Collections;
using System.Collections.Generic;
using TMPro;

//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class shopui : MonoBehaviour
{
    public GameObject[] shopbuttons;
    Color basecolor = new Color(0,255,254);
    public GameData saveddata;
    public TMP_Text price;
    public GameObject buy_button;

    // Start is called before the first frame update
    void Start()
    {
        //owned.Add(PlayerPrefs.GetInt("PlayerType"));
        owned = saveddata.retrieved;
        updateui();
        // int temp = PlayerPrefs.GetInt("PlayerType");
        // for (int i = 0; i < shopbuttons.Length; i++)
        // {
        //     if(i == temp)
        //     {
        //         shopbuttons[i].GetComponent<Image>().color = Color.green;
        //         Debug.Log("NAME IS "+shopbuttons[i].GetComponent<Transform>().GetChild(0).name);
        //         shopbuttons[i].GetComponent<Transform>().GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "SELECTED";
        //         //shopbuttons[i].GetComponent<Image>().color = Color.green;
        //     }
        // }
    }
    [SerializeField]List<int> owned = new List<int>();
    int selected;
    bool purchased;
    public void changeplayer(int indexno)
    {
        //PlayerPrefs.SetInt("PlayerType",indexno);
        selected = indexno;
        if(owned.Count>0)
        {
            for (int i = 0; i < owned.Count; i++)
            {
                if(owned[i] == indexno)
                {
                    purchased = true;
                    PlayerPrefs.SetInt("PlayerType",indexno);
                    GameObject.Find("MainMenuDataManager").GetComponent<MainMenuUIManager>().changingplayerskin();
                    //MainMenuUIManager.changingplayerskin();
                    GameData.savingdata(owned);
                    buy_button.SetActive(false);
                    price.text = "OWNED";//gameObject.SetActive(false);
                    break;
                }
            }
            
            //owned.Add(indexno);
        }
        else
        {
            //owned.Add(indexno);

        }
        changingprice(indexno);
        purchased = false;
        optionalui(indexno);
        updateui();
    }
    public void changingprice(int value)
    {
        int newvalue =0;
        switch (value)
        {
            
            case 0 : newvalue =0;
                break;
            case 1 : newvalue =500;
                break; 
            case 2 : newvalue =1000;
                break; 
            case 3 : newvalue =2000;
                break; 
            case 4 : newvalue =4000;
                break; 
            case 5 : newvalue =8000;
                break; 
            case 6 : newvalue =15000;
                break; 
            case 7 : newvalue =20000;
                break;  
        }
        if(purchased)
        {
            price.text = "OWNED";
        }
        else
        {
            buy_button.SetActive(true);
            price.text = newvalue.ToString();
            if(value < PlayerPrefs.GetInt("GemCount"))
            {
                price.color = Color.white;
            }
            else
            {
                price.color = Color.red;
            }
        }
    }
    public void addtolist()
    {
        if(price.color == Color.white)
        {
            int temp = PlayerPrefs.GetInt("GemCount");
            int secondvalue = temp - int.Parse(price.text);
            PlayerPrefs.SetInt("GemCount",secondvalue);
            owned.Add(selected);
            PlayerPrefs.SetInt("PlayerType",selected);
            GameData.savingdata(owned);
            updateui();
            //saveddata.savingdata(owned);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void optionalui(int index)
    {
        Debug.Log("Gem COunt:"+ PlayerPrefs.GetInt("GemCount")+ " Owned:"+owned.Count);
        for (int i = 0; i < shopbuttons.Length; i++)
        {
            if(i== index)
            {
                shopbuttons[i].GetComponent<Image>().color = Color.yellow;
            }
            else
            {
                shopbuttons[i].GetComponent<Image>().color = basecolor;
            }
        }
    }

    void updateui()
    {
        int temp = PlayerPrefs.GetInt("PlayerType");
        
        for (int i = 0; i < shopbuttons.Length; i++)
        {
            if(i == temp)
            {
                shopbuttons[i].GetComponent<Image>().color = Color.green;
                Debug.Log("NAME IS "+shopbuttons[i].GetComponent<Transform>().GetChild(0).name);
                shopbuttons[i].GetComponent<Transform>().GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "SELECTED";
                //shopbuttons[i].GetComponent<Image>().color = Color.green;
            }

            else
            {
                for (int j = 0; j < owned.Count; j++)
                {
                    if(i == owned[j])
                    {
                        shopbuttons[i].GetComponent<Image>().color = basecolor;
                        shopbuttons[i].GetComponent<Transform>().GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "OWNED";
                    }
                }
            }


            

        }
        // foreach (int item in owned)
        // {
        //     if(item !=temp)
        //     {
                
        //     }
        // }

    }
}
