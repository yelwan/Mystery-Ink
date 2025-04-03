using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CollectSpraycan : MonoBehaviour
{
    private Collider2D Can;
    public float collectDistance = 1.0f;
    public GameObject player;
    public bool collected = false;

    public string colorName = "blue";

    public bool isEquipped = false;

    public float amount = 5f;

    private static int numberOfCans = 0; 

    private InventorySystem inventorySystem;

    void Start()
    {
        Can = GetComponent<BoxCollider2D>();

        // Find the player's InventorySystem
        inventorySystem = player.GetComponent<InventorySystem>();
    }

    void Update()
    {
        if (!collected && Vector2.Distance(transform.position, player.transform.position) <= collectDistance)
        {
            Collect();
        }

        if (collected)
        {
            Vector2 offset = new Vector2(0.9f, 1.0f); // Adjust these values for desired offset
            transform.position = (Vector2)player.transform.position + offset;
        }
    }

    private void Collect()
    {
        if (collected) return; // Prevent duplicate collection

        collected = true;
        numberOfCans++; // 🔹 Increment before disabling

        if (inventorySystem != null)
        {
            inventorySystem.CollectSprayCan(this); // Add to inventory
        }

    }
}
