using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MaskManager : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
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
    }

    void Interact()
    {
   
            spriteRenderer.enabled = true;
            StartCoroutine(DelayedOpenDoor());
        
    }

    private IEnumerator DelayedOpenDoor()
    {
        yield return new WaitForSeconds(2f);
        door?.OpenDoor();
    }
}
