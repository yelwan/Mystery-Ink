using System.Collections;
using UnityEngine;

public class TriggerDoor : MonoBehaviour
{
    /*
     * Detects if player reached "Endpoint", opening door, using function from PlayerController,
     * moving player into the door, then closing the door.
     */

    [SerializeField] moveDoor door;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            door.OpenDoor();
            Debug.Log("Player entered the trigger!");
            StartCoroutine(Wait(3));
        }
    }

    IEnumerator Wait(int delay)
    {
        yield return new WaitForSeconds(delay);
    }
}
