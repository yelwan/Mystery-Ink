using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MaskManager : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    public delegate void  ActivateInteraction();
    public ActivateInteraction activateInteraction;
   void Awake()
    {
        activateInteraction += Interact;
    }
    void Start()
    {
        spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
    }

    void Interact ()
    {
        spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
    }
 

  
    
    
}
