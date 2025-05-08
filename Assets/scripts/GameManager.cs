using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Code review : 
    // Game Manager manages the flow of the game from a macro perspective, therefore level progression.
    // All your levels are in the same scene, so it would be easy to reference them here in an array.
    // You could create a Level script that you place at the root of each level. The level script 
    // Would have simple functions such as start level, stop level, on level ended events, etc... 
    // It would manage the level flow during its lifecycle. The teleportPlayer function would also be done from 
    // the Level script as part of the start level flow. Each level knows its player spawn point.
    // The level script would also have a reference to a level timer (one per level)

    // Level class : manages level flow (whatever happens when the level starts, ends, win / lose conditions, timer...)
    // Timer class : manages time constraints on a level
    // Game Manager : manages game flow (such as going to the next level on completion, etc...)
    
    public int Timer = 33; 
    [SerializeField] Transform greenObject; 
    [SerializeField] EndSceneTrigger trigger;
    [SerializeField] GameObject player;
    public int CurrentLevel = 1; //Progresses as each level is completed. Max is 3 for now
    [SerializeField] int LastLevel = 4;

    // Code review : instead of having one timer variable per level all stacked in the game manager, you should have one instance 
    // of a timer per level (or encapulsate it in a Level script that manages the level's flow, as explained above). 
    // Your game manager would one cache the current timer, start it when the level starts and listen to it
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

