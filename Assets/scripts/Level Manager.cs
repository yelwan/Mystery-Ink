using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    //[SerializeField] Transform playerSpawnPoint;

    //Need to move wide view camera to see the new level, not player. This is to test which view
    //or experience are better!

    Coroutine countdownCoroutine;
    [SerializeField] int levelTimer = 60;
   
    public void StartLevel(GameObject player)
    {
        TeleportPlayer(player);
    }

    private void TeleportPlayer(GameObject player)
    {
        //player.transform.position = playerSpawnPoint.position;
        StartCoroutine(WaitAndEnablePlayer(3, player.GetComponent<PlayerController>()));
    }


    IEnumerator WaitAndEnablePlayer(int timer, PlayerController i_player)
    {
        yield return new WaitForSeconds(timer);
        i_player.enabled = true;
    }

   

    public int GetLevelTimer() => levelTimer;
}
