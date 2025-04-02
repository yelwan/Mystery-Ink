using System.Collections;
using UnityEngine;

public class moveDoor : MonoBehaviour
{
    public Transform door; // Assign the door GameObject in the Inspector
    public Transform mask;
    public float slideDistance = 2f; // Distance to slide right
    public float speed = 2f; // Speed of the movement

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isOpen = false;

    void Start()
    {
        if (door != null)
        {
            closedPosition = door.position;
            openPosition = closedPosition + new Vector3(slideDistance, 0, 0);
        }
    }

    public void OpenDoor()
    {
        if (!isOpen && door != null)
        {
            StartCoroutine(MoveDoor(door.position, openPosition));
            isOpen = true;
        }
    }

    private IEnumerator MoveDoor(Vector3 startPos, Vector3 targetPos)
    {
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            door.position = Vector3.Lerp(startPos, targetPos, elapsedTime);
            elapsedTime += Time.deltaTime * speed;
            yield return null;
        }
        door.position = targetPos;
        mask.position = targetPos;
    }
}
