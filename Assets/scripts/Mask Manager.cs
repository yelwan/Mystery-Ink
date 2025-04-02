using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MaskManager : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] SpriteRenderer spriteRenderer2;

    public delegate void  ActivateInteraction();
    public ActivateInteraction activateInteraction;

    public string color = "blue";
    public CollectSpraycan can;
    public moveDoor door;
   void Awake()
    {
        activateInteraction += Interact;
    }
    void Start()
    {
        spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        spriteRenderer2.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
    }

    void Interact()
    {
        if (can.colorName == color)
        {
            spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
            spriteRenderer2.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
            StartCoroutine(DelayedOpenDoor());
        }
    }

    private IEnumerator DelayedOpenDoor()
    {
        yield return new WaitForSeconds(2f); // Wait for 2 seconds
        door.OpenDoor();
    }
}
