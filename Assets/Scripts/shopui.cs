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

    // Start is called before the first frame update
    void Start()
    {
        owned.Add(PlayerPrefs.GetInt("PlayerType"));
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
    public void changeplayer(int indexno)
    {
        PlayerPrefs.SetInt("PlayerType",indexno);
        if(owned.Count>0)
        {
            for (int i = 0; i < owned.Count; i++)
            {
                if(owned[i] == indexno)
                {
                    break;
                }
            }
            owned.Add(indexno);
        }
        else
        {
            owned.Add(indexno);

        }

        updateui();
    }

    // Update is called once per frame
    void Update()
    {
        
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
