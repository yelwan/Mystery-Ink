using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Spikes : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] InventorySystem inventorySystem;
    [SerializeField] SprayController sprayController;
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject != player) return;
        Debug.Log("Entered");
        GameObject currentSprayCan = inventorySystem.GetEquippedSprayCan();
        if (currentSprayCan == null) return;

        CollectSpraycan spraycan = currentSprayCan.GetComponent<CollectSpraycan>();
        if (spraycan == null) return;

        spraycan.amount = Mathf.Max(0, spraycan.amount - 1f);

        inventorySystem.CurrentSprayController.SetAmount(spraycan.amount);
        Debug.Log("Done");
    }
}
