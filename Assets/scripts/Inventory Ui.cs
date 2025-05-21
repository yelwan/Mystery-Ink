using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class InventoryUi : MonoBehaviour
{

    [SerializeField] private Sprite redSprite;
    [SerializeField] private Sprite blueSprite;
    [SerializeField] private Sprite yellowSprite;

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

        //Sprite selectedSprite = GetSpriteFromInk(inventorySystem.GetEquippedSprayCan().GetComponent<CollectSpraycan>().colorName.ToString());
        //Debug.Log($"Selected sprite for color {inventorySystem.GetEquippedSprayCan().GetComponent<CollectSpraycan>().colorName}: {selectedSprite}");

        /*if (selectedSprite != null)
        {
            //labelElement.style.backgroundColor = GetColorFromInk(inventorySystem.inkColor.ToString());
            labelElement.style.backgroundImage = new StyleBackground(selectedSprite.texture);
        }
        else
        {
            Debug.LogWarning("Selected sprite is null!");
        }*/
    }

    private void onEnded(IInventoryObserver i_Inventory, GameObject i_item)
    {
        myGameObject = null;
        labelElement.style.backgroundImage = null;
        labelElement.style.backgroundColor = Color.clear;
    }

    private void OnAmountUpdated(IISprayAmount i_SparyAmount, float i_Amount)
    {
        float clampedAmount = Mathf.Clamp(i_Amount, 0f, 5f);
        myGameObject.GetComponent<CollectSpraycan>().amount = clampedAmount;

        float progress = clampedAmount / 5f;
        healthBar.style.width = Length.Percent(100 * progress);
    }

    private Sprite GetSpriteFromInk(string inkColor)
    {
        switch (inkColor.ToLower())
        {
            case "red":
                return redSprite;
            case "blue":
                return blueSprite;
            case "yellow":
                return yellowSprite;
            default:
                return null;
        }
    }


    private Color GetColorFromInk(string inkColor)
    {
        switch (inkColor.ToLower())
        {
            case "red":
                return Color.red;
            case "blue":
                return Color.blue;
            case "yellow":
                return Color.yellow;
            default:
                return Color.white; // fallback color
        }
    }
}


/*
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
        healthBar.style.width = Length.Percent((100 * progress));

    }
}
*/