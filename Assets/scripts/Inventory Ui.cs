using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class InventoryUi : MonoBehaviour
{
    [SerializeField] InventorySystem inventorySystem = null;
    [SerializeField] Label countdownLabel;
    private VisualElement labelElement;

    private void Awake()
    {
        inventorySystem.OnStarted += onStarted;
        inventorySystem.OnEnded += onEnded;
        UIDocument root = GetComponent<UIDocument>();
        labelElement = root.rootVisualElement.Q<VisualElement>("CharacterPortrait");
        labelElement.transform.scale = new Vector3(5f, 5f, 5f);
    }

    void OnDestroy()
    {
        inventorySystem.OnStarted -= onStarted;
        inventorySystem.OnEnded -= onEnded;
        inventorySystem = null;
    }

    private void onStarted(IInventoryObserver i_Inventory, GameObject i_item)
    {
        SpriteRenderer spriteRenderer = i_item.GetComponent<SpriteRenderer>();
        Sprite sprite = spriteRenderer.sprite;
        labelElement.style.backgroundImage = new StyleBackground(sprite.texture);
    }
    private void onEnded(IInventoryObserver i_Inventory, GameObject i_item)
    {
        labelElement.style.backgroundImage = null;
    }
    
}
