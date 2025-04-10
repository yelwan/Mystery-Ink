
using Unity.Cinemachine;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraZoomIn : MonoBehaviour
{
    [SerializeField] CinemachineCamera wideViewCam;
    [SerializeField] CinemachineCamera playerCam;
    [SerializeField] float delayBeforeZoom = 2f;
    [SerializeField] GameObject player;
    Coroutine coroutine = null;


    void Start()
    {
        Transform transformP = player.GetComponent<Transform>();
        playerCam.Follow = transformP;
        playerCam.LookAt = transformP;

  

        wideViewCam.Priority = 20;
        playerCam.Priority = 10;

        coroutine = StartCoroutine(ZoomInCoroutine(delayBeforeZoom));
 
        playerCam.Follow = transformP;
        playerCam.LookAt = transformP;
    }

    void ZoomToPlayer()
    {
        wideViewCam.Priority = 10;
        playerCam.Priority = 20;
    }

    IEnumerator ZoomInCoroutine(float delay)
    {
       yield return new WaitForSeconds(delay);
       ZoomToPlayer();
    }
}