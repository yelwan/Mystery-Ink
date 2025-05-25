using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class InventoryUi : MonoBehaviour
{

    [SerializeField] private Sprite redSprite;
    [SerializeField] private Sprite blueSprite;
    [SerializeField] private Sprite yellowSprite;

    [SerializeField] private CollectSpraycan redCan;
    [SerializeField] private CollectSpraycan blueCan;
    [SerializeField] private CollectSpraycan redCan2;
    [SerializeField] private CollectSpraycan yellowCan;

    [SerializeField] InventorySystem inventorySystem = null;
    [SerializeField] SprayController sprayController = null;
    private VisualElement labelElement;
    private VisualElement healthBar;
    public GameObject myGameObject = null;
    private VisualElement Instructions;

    private void Awake()
    {
        sprayController.SetAmounts += OnAmountUpdated;
        inventorySystem.OnStarted += onStarted;
        inventorySystem.OnEnded += onEnded;

        UIDocument root = GetComponent<UIDocument>();
        labelElement = root.rootVisualElement.Q<VisualElement>("CharacterPortrait");
        healthBar = root.rootVisualElement.Q<VisualElement>("HealthBar");

        labelElement.transform.scale = new Vector3(5f, 5f, 5f);


        Instructions = root.rootVisualElement.Q<VisualElement>("instructionsUI");

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

        // Unsubscribe from previous sprayController
        if (sprayController != null)
        {
            sprayController.SetAmounts -= OnAmountUpdated;
        }

        sprayController = i_item.GetComponentInChildren<SprayController>();

        if (sprayController != null)
        {
            sprayController.SetAmounts += OnAmountUpdated;
        }

        CollectSpraycan can = i_item.GetComponent<CollectSpraycan>();
        sprayController.UpdateCurrentSprayCan(can);

        if (redCan.isEquipped || redCan2.isEquipped)
        {
            labelElement.style.backgroundImage = new StyleBackground(redSprite);
        }
        else if (blueCan.isEquipped)
        {
            labelElement.style.backgroundImage = new StyleBackground(blueSprite);
        }
        else if (yellowCan.isEquipped)
        {
            labelElement.style.backgroundImage = new StyleBackground(yellowSprite);
        }
    }

    private void onEnded(IInventoryObserver i_Inventory, GameObject i_item)
    {
        // Unsubscribe from the current spray controller
        if (sprayController != null)
        {
            sprayController.SetAmounts -= OnAmountUpdated;
            sprayController = null;
        }

        myGameObject = null;
        labelElement.style.backgroundImage = null;
    }


    private void OnAmountUpdated(IISprayAmount sender, float amount)
    {
        SprayController controller = sender as SprayController;

        if (controller != sprayController) return;

        CollectSpraycan can = controller.GetComponentInParent<CollectSpraycan>();
        if (can == null || !can.isEquipped) return;

        can.amount = amount;

        float progress = ++amount / 5f;
        healthBar.style.width = Length.Percent((100 * progress) );
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

    private void Start()
    {
        float delaySeconds = 12f;
        float fadeDuration = 2f;
        if (Instructions != null)
        {
            StartCoroutine(FadeOutAfterDelay(Instructions, delaySeconds, fadeDuration));
        }
        else
        {
            Debug.Log("Nothing to display!!");
        }
    }
    IEnumerator FadeOutAfterDelay(VisualElement element, float delay, float duration)
    {
        yield return new WaitForSeconds(delay);

        float elapsed = 0f;
        float startOpacity = element.resolvedStyle.opacity;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float newOpacity = Mathf.Lerp(startOpacity, 0f, elapsed / duration);
            element.style.opacity = newOpacity;
            yield return null;
        }

        element.style.opacity = 0f;
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
        CollectSpraycan can = i_SprayAmount as CollectSpraycan;
    if (can == null) return;

    float clampedAmount = Mathf.Clamp(i_Amount, 0f, 5f);
    can.amount = clampedAmount;

    float progress = clampedAmount / 5f;
    healthBar.style.width = Length.Percent((100 * progress) + 1);

    }
}
*/