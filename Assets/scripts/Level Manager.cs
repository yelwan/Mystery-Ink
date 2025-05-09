using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] Transform playerSpawnPoint;
    Coroutine countdownCoroutine;
    [SerializeField] int levelTimer = 60;
   


    public void StartLevel(GameObject player)
    {
        TeleportPlayer(player);
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

   

    public int GetLevelTimer() => levelTimer;
}
