using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MaskManager : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] SpriteRenderer doorWay;
    public delegate void  ActivateInteraction();
    public ActivateInteraction activateInteraction;

    public moveDoor door;
   void Awake()
    {
        activateInteraction += Interact;
    }
    void Start()
    {
        spriteRenderer.enabled = false;
        doorWay.enabled = false;
    }

    void Interact()
    {
   
            spriteRenderer.enabled = true;
            doorWay.enabled = true;
            StartCoroutine(DelayedOpenDoor());
        
    }

    private IEnumerator DelayedOpenDoor()
    {
        yield return new WaitForSeconds(2f); // Wait for 2 seconds
        door?.OpenDoor();
    }
}
