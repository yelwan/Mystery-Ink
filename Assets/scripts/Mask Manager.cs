using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MaskManager : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    public delegate void  ActivateInteraction();
    public ActivateInteraction activateInteraction;

    public moveDoor door = null;

    [SerializeField] SpriteRenderer letter = null;
   void Awake()
    {
        activateInteraction += Interact; 
        // Code review : listening to events internally inside a class is an anti-pattern.
        // What you are doing here : 
        // You have a public event and you are adding a private function to it as a listener, all in the same class - then you
        // call the event manually from SprayingInput --> very convoluted.
        // What you should do instead : 
        // Delete that event and make the Interact function public and call it directly from SprayingInput
    }
    void Start()
    {
        spriteRenderer.enabled = false;
        if (letter != null) letter.enabled = false;
    }

    void Interact()
    {
            spriteRenderer.enabled = true;
        if (letter != null) letter.enabled = true;

        if (door != null)
            StartCoroutine(DelayedOpenDoor());
    }

    private IEnumerator DelayedOpenDoor()
    {
        yield return new WaitForSeconds(2f);
        door.OpenDoor();
    }
}
