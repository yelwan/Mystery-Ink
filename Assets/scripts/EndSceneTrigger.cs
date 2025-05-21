using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneTrigger : MonoBehaviour
{
    [SerializeField] Collider2D player;
    private int collisions_player = 0;
    [SerializeField] GameManager gameManager;
    [SerializeField] bool done = false;
    // Code review : GameManager is referencing an end scene trigger. End scene trigger is referencing the game manager.
    // Your code is full of circular dependencies like this. Remove them by applying the observer pattern. 
    // One 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != player || done == true) return;
        done = true;
        gameManager.OnWin();
    }
}
