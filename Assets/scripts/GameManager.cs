using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Level[] levels;         
    [SerializeField] GameObject player;     
    [SerializeField] TimerSystem timerSystem;
    [SerializeField] moveDoor door;          
    private int currentLevelIndex = 0;        

    private bool gameDone = false;            

    [SerializeField] Collider2D playerCollider;
    [SerializeField] CameraZoomIn cameraZoomIn;

    public Collider2D LastLevelDoorway;

   
    private void Start()
    {
        StartLevel();
    }

    private void Update()
    {
        if (gameDone) return;
    }

    private void StartLevel()
    {
        if (currentLevelIndex >= levels.Length) return;

        levels[currentLevelIndex].StartLevel(player); 
        timerSystem.StartTimer(levels[currentLevelIndex].GetLevelTimer());  

        gameDone = false;
    }

    private void CompleteLevel()
    {
        gameDone = true;
        player.GetComponent<PlayerController>().enabled = false;
        door.GetComponent<Collider2D>().enabled = false;

        door.CloseDoor();

        StartCoroutine(WaitToProgress(2));
    }


    public void OnWin()
    {
        gameDone = true;
        OnDoorwayTriggered(this.GetComponent<Collider2D>());
        StartCoroutine(WaitToProgress(2));
        if (null == door) return;
        door.GetComponent<Collider2D>().enabled = false;
        player.GetComponent<PlayerController>().enabled = false;
        StartCoroutine(WaitForDoor(1));
    }

    private IEnumerator WaitToProgress(int time)
    {
        yield return new WaitForSeconds(time);

        NextLevel();
    }

    public void NextLevel()
    {
        currentLevelIndex++;

        if (currentLevelIndex == 4)
        {
            SceneManager.LoadScene(2); 
            return;
        }

        if (currentLevelIndex >= levels.Length) return;

        //To show player next level from the array of cameras
        
            cameraZoomIn.TransitionCamera();
        
        StartLevel();
    }

    public void OnDoorwayTriggered(Collider2D triggeredCollider)
    {
        if (triggeredCollider == LastLevelDoorway)
        {
            Debug.Log("Last level doorway was triggered");
            // Load end scene or do something special
            StartCoroutine(Wait(2));
        }
    }

    IEnumerator WaitForDoor (int delay)
    {
        yield return new WaitForSeconds(delay);
        door.CloseDoor();
        StartCoroutine(WaitForDoor(1));
    }
    IEnumerator Wait(int delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(2);
    }
}
