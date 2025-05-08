using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int Timer = 33;
    [SerializeField] Transform greenObject; 
    [SerializeField] EndSceneTrigger trigger;
    [SerializeField] GameObject player;
    public int CurrentLevel = 1; //Progresses as each level is completed. Max is 3 for now
    [SerializeField] int LastLevel = 4;

    public int TimerL2 = 83; //+3 for the door to open and close
    public int TimerL3 = 103;
    
    [SerializeField] TimerSystem timerSystem;
    Coroutine coroutineCountdown;
    void Start()
    {
        coroutineCountdown = StartCoroutine("Countdown", Timer);
    }

    IEnumerator Countdown(int timer)
    {
        yield return new WaitForSeconds(timer);
        Debug.Log("This is the timer finished!");
        if (!trigger.GameDone) { SceneManager.LoadScene(3); } //Must start a new game.

        if(CurrentLevel < LastLevel)
        {
            trigger.GameDone = false;
        }
    }

    public void ProgressLevel()
    {
        CurrentLevel++;

        if (CurrentLevel == 2)
        {
            TeleportPlayer(greenObject.position.x, greenObject.position.y);
            StopCoroutine(coroutineCountdown);
            StopCoroutine(timerSystem.countdownCoroutine);
            StopAllCoroutines();
            StartCoroutine(Countdown(TimerL2));
        }

        else if (CurrentLevel == 3)
        {
            TeleportPlayer(-0.3, -57.5); //Do nothing for now. Teleport player to level 3
            StopCoroutine(coroutineCountdown);
            Debug.Log("This is 1");
            timerSystem.StopCoroutine(timerSystem.countdownCoroutine);
            Debug.Log("This is 2");
            StopAllCoroutines();
            Debug.Log("This is 3");
            StartCoroutine(Countdown(TimerL3));
            Debug.Log("This is 4");
        }

        else if (CurrentLevel == 4)
        {
            //Load end scene
            SceneManager.LoadScene(2);
        }
    }

    void TeleportPlayer(double x, double y)
    {
        Vector2 new_position = new Vector2((float)x, (float)y);
        player.transform.position = new_position;

        StartCoroutine(Wait(3));
        player.GetComponent<PlayerController>().enabled = true;
    }

    IEnumerator Wait(int timer)
    {
        yield return new WaitForSeconds(timer);
    }
}

