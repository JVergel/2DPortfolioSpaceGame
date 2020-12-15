using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CShake : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private CinemachineBasicMultiChannelPerlin BaseShake;

    private void Awake()
    {
        BaseShake = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    public IEnumerator kShake()
    {
        BaseShake.m_AmplitudeGain = 0.05f;
        //StartShake(intesity);
        yield return new WaitForSeconds(0.9f);
        BaseShake.m_AmplitudeGain = 0;
    }
        public void Shake()
    {
        StartCoroutine(kShake());

    }


}
