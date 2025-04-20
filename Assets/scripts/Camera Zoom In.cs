
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
    Transform transformP;


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

    public void ZoomToObject(float x, float y, float z)
    {
        Vector3 newPos = new Vector3(x, y, z); 
        playerCam.Follow.transform.position = newPos;
        playerCam.LookAt.transform.position = newPos;
    }

    public void ZoomToPlayer2()
    {
        playerCam.Follow = transformP;
        playerCam.LookAt = transformP;
    }
}