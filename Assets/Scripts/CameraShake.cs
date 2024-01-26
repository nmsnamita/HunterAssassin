using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    // Start is called before the first frame update
    private CinemachineVirtualCamera virtualCamera;
    private float ShakeIntensity = 5f;
    private float ShakeTime =0.2f;

    private float timer;
    //private CinemachineBasicMultiChannelPerlin _cbmcp;
    void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }
    private void Start() {
        StopShake();
    }
    public void ShakeCamera()
    {
       CinemachineBasicMultiChannelPerlin _cbmcp = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
       _cbmcp.m_AmplitudeGain= ShakeIntensity;

       timer = ShakeTime;

       Invoke("StopShake",0.5f);
    }
    void StopShake()
    {
        CinemachineBasicMultiChannelPerlin _cbmcp = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
       _cbmcp.m_AmplitudeGain= 0;

       timer = 0;
    }

    

}
