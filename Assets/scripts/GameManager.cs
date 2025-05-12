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

        StartLevel();
    }
}
