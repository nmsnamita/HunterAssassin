using UnityEngine;
using UnityEngine.UI;

public class ImageBlinkEffect : MonoBehaviour
{
    public Color startColor = Color.red;
    public Color endColor = Color.black;

    [Range(1,10)]
    public float speed = 1;

    Image  impComp;

    void Awake(){

        impComp = GetComponent<Image>();
    }

     void Update(){

        impComp.color = Color.Lerp(startColor,endColor, Mathf.PingPong(Time.time*speed,1));
    }

}
