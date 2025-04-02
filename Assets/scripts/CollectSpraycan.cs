using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CollectSpraycan : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Collider2D Can;
    public float collectDistance = 2.0f;
    public GameObject player;
    public bool collected = false;

    public string colorName = "blue";

    public bool isEquipped = false;

    public float amount = 5f;

    private int numberOfCans = 0;

  
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

        if(collected)
        {
            Vector2 offset = new Vector2(0.9f, 1.0f); // Adjust these values for desired offset
            gameObject.transform.position = (Vector2)player.transform.position + offset;
        }
    }

    private void Collect()
    {
        collected = true;
        if (inventorySystem != null)
        {
            inventorySystem.CollectSprayCan(this); // Add to inventory
        }

        if (numberOfCans != 0)
        {
            gameObject.SetActive(false); // Hide the spray can after collection
            numberOfCans++;
        }
        else
        {
            numberOfCans++;
        }
    }
}
