
using Unity.Cinemachine;
using UnityEngine;
using System.Collections;

public class CameraZoomIn : MonoBehaviour
{
    //[SerializeField] CinemachineCamera wideViewCam;

    [SerializeField] CinemachineCamera [] Level_Cams;
    //no objects, so commented out object cam, and commented out Player cam to test which view is the
    //better experience for a maze game.

    [SerializeField] CinemachineCamera playerCam;
    //[SerializeField] CinemachineCamera objectCam;

    [SerializeField] float delayBeforeZoom = 2f;
    [SerializeField] GameObject player;
    Coroutine coroutine = null;
    Transform transformP;

    private int LevelIndex = 0;

    // Code review : 
    // For switching between vCams, a cleaner way is to simply call
    // cinemachineCamera.Prioritize();
    // That way, you will not need to manage priority values.

    void Start()
    {
        transformP = player.GetComponent<Transform>();
        //ZoomToPlayer2();

        Level_Cams[1].Priority = 5;
        playerCam.Priority = 1;

        //wideViewCam.Prioritize();

        coroutine = StartCoroutine(ZoomInCoroutine(delayBeforeZoom));

        // ZoomToPlayer(); //Instead of ZoomToPlayer2(), for now
    }

    void ZoomToPlayer()
    {
        //Level_Cams[LevelIndex].Priority = 1;
        //playerCam.Priority = 5;
        //playerCam.Prioritize();
    }


    IEnumerator ZoomInCoroutine(float delay)
    {
       yield return new WaitForSeconds(delay);

        //playerCam.Prioritize();
        for (int i = 0; i < Level_Cams.Length; i++)
        {
            Level_Cams[i].Priority = 1;
        }

        playerCam.Priority = 5;
        //ZoomToPlayer();
    }

    public void ZoomToObject(Transform targetTransform)
    {
        //objectCam.Follow = targetTransform;
        //objectCam.LookAt = targetTransform;
        /*playerCam.Priority = 15;
        objectCam.Priority = 20;*/
        //objectCam.Prioritize();
    }

    public void ZoomToPlayer2()
    {
        //playerCam.Follow = transformP;
        //playerCam.LookAt = transformP;

        /*playerCam.Priority= 20;
        objectCam.Priority= 15;*/
        //playerCam.Prioritize();
        //wideViewCam.Prioritize();
    }

    //Function to transition between cameras
    public void TransitionCamera()
    {
        StartCoroutine(Transition_Delay(2));
    }

    IEnumerator Transition_Delay(int delay)
    {
        yield return new WaitForSeconds(delay);
        LevelIndex++;
        playerCam.Priority = 1;
        Level_Cams[LevelIndex].Priority = 5;

        yield return new WaitForSeconds(delay+3);
        playerCam.Priority = 5;
        Level_Cams[LevelIndex].Priority = 1;
    }
}