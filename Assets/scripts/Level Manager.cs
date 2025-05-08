using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] Transform playerSpawnPoint;
    Coroutine coroutineCountdown;

    void Start()
    {
        coroutineCountdown = StartCoroutine("Countdown", Timer);
    }


    public void StartLevel(GameObject player)
    {
        TeleportPlayer(player);
        StartCountdown();
    }




    private void TeleportPlayer(GameObject player)
    {
        player.transform.position = playerSpawnPoint.position;
        StartCoroutine(Wait(3));
        player.GetComponent<PlayerController>().enabled = true;
   
    }


    IEnumerator Wait(int timer)
    {
        yield return new WaitForSeconds(timer);
    }

    private void StartCountdown()
    {
        if (countdownCoroutine != null)
            StopCoroutine(countdownCoroutine);

        countdownCoroutine = StartCoroutine(CountdownCoroutine());
    }

  
}
