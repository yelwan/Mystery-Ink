
using Unity.Cinemachine;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Mono.Cecil.Cil;

public class CameraZoomIn : MonoBehaviour
{
    [SerializeField] CinemachineCamera wideViewCam;
    [SerializeField] CinemachineCamera playerCam;
    [SerializeField] CinemachineCamera objectCam;
    [SerializeField] float delayBeforeZoom = 2f;
    [SerializeField] GameObject player;
    Coroutine coroutine = null;
    Transform transformP;

    // Code review : 
    // For switching between vCams, a cleaner way is to simply call
    // cinemachineCamera.Prioritize();
    // That way, you will not need to manage priority values.

    void Start()
    {

        transformP = player.GetComponent<Transform>();
        ZoomToPlayer2();

        wideViewCam.Priority = 20;
        playerCam.Priority = 10;

        coroutine = StartCoroutine(ZoomInCoroutine(delayBeforeZoom));
 
        ZoomToPlayer2();
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

    public void ZoomToObject(Transform targetTransform)
    {
        objectCam.Follow = targetTransform;
        objectCam.LookAt = targetTransform;
        playerCam.Priority = 15;
        objectCam.Priority = 20;
    }

    public void ZoomToPlayer2()
    {
        playerCam.Follow = transformP;
        playerCam.LookAt = transformP;

        playerCam.Priority= 20;
        objectCam.Priority= 15;
    }
}