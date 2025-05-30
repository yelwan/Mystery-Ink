using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneTrigger : MonoBehaviour
{
    [SerializeField] Collider2D player;
    const int EndScene = 2;
    public moveDoor door;
    public bool GameDone = false;
    [SerializeField] GameManager gameManager; 
    // Code review : GameManager is referencing an end scene trigger. End scene trigger is referencing the game manager.
    // Your code is full of circular dependencies like this. Remove them by applying the observer pattern. 
    // One 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != player) return;

        GameDone = true;

        // disabling player controller and door collider so player can enter the doorway
        StartCoroutine(WaitToProgress(2));

        if (null == door) return;

        door.GetComponent<Collider2D>().enabled = false;
        player.GetComponent<PlayerController>().enabled = false;

        //Door closing in front of Player
        StartCoroutine(WaitForDoor(1));
        door.CloseDoor();
        StartCoroutine(WaitForDoor(2));
        
    }

    IEnumerator WaitForDoor(int time)
    {
        yield return new WaitForSeconds(time);
    }

    IEnumerator WaitToProgress(int timer)
    {
        yield return new WaitForSeconds(timer);
        gameManager.NextLevel();
    }
}
