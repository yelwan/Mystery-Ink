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
