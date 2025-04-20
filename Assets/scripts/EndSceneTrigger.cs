using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Collider2D player;
    const int EndScene = 2;
    public moveDoor door;
    public bool GameDone = false;
    [SerializeField] GameManager gameManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != player) return;

        GameDone = true;

        // disabling player controller and door collider so player can enter the doorway
        door.GetComponent<Collider2D>().enabled = false;
        player.GetComponent<PlayerController>().enabled = false;

        //Door closing in front of Player
        StartCoroutine(WaitForDoor(1));
        door.CloseDoor();
        StartCoroutine(WaitForDoor(2));
        StartCoroutine(WaitToProgress(2));
    }

    IEnumerator WaitForDoor(int time)
    {
        yield return new WaitForSeconds(time);
    }

    IEnumerator WaitToProgress(int timer)
    {
        yield return new WaitForSeconds(timer);
        gameManager.ProgressLevel();
    }
}
