using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class InventoryUi : MonoBehaviour
{
    [SerializeField] InventorySystem inventorySystem = null;
    [SerializeField] SprayController sprayController = null;
    private VisualElement labelElement;
    private VisualElement healthBar;
    public GameObject myGameObject = null;
    private void Awake()
    {
        sprayController.SetAmounts += OnAmountUpdated;
        inventorySystem.OnStarted += onStarted;
        inventorySystem.OnEnded += onEnded;
        UIDocument root = GetComponent<UIDocument>();
        labelElement = root.rootVisualElement.Q<VisualElement>("CharacterPortrait");
        healthBar = root.rootVisualElement.Q<VisualElement>("HealthBar");
        labelElement.transform.scale = new Vector3(5f, 5f, 5f);
    }

    void OnDestroy()
    {
        inventorySystem.OnStarted -= onStarted;
        inventorySystem.OnEnded -= onEnded;
        inventorySystem = null;
        sprayController.SetAmounts -= OnAmountUpdated;
        sprayController = null;
    }

    private void onStarted(IInventoryObserver i_Inventory, GameObject i_item)
    {

            myGameObject = i_item;
            CollectSpraycan can = i_item.GetComponent<CollectSpraycan>();
            sprayController.UpdateCurrentSprayCan(can);
        SpriteRenderer spriteRenderer = i_item.GetComponent<SpriteRenderer>();
        Sprite sprite = spriteRenderer.sprite;
        labelElement.style.backgroundImage = new StyleBackground(sprite.texture);
    }
    private void onEnded(IInventoryObserver i_Inventory, GameObject i_item)
    {
        myGameObject = null;
        labelElement.style.backgroundImage = null;
    }
    
    private void OnAmountUpdated(IISprayAmount i_SparyAmount, float i_Amount)
    {
        float clampedAmount = Mathf.Clamp(i_Amount, 0f, 5f); 
        myGameObject.GetComponent<CollectSpraycan>().amount = clampedAmount;

        float progress = clampedAmount / 5f;
        healthBar.style.width = Length.Percent((100 * progress)+1);

    }
}
