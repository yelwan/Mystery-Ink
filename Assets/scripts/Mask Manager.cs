using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MaskManager : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] GameObject player;
    void Start()
    {
        spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
    }

    void Update()
    {
      
    
    }
 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != player.gameObject) return;
        spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;

    }
    
    
}
