using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public List<GameObject> collectedSprayCans = new List<GameObject>(); // Stores collected spray cans
    private int equippedIndex = -1; // Tracks which spray can is equipped (-1 means none)

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CycleSprayCan();
        }
    }

    public void CollectSprayCan(CollectSpraycan sprayCan)
    {
        if (!sprayCan.collected)
        {
            sprayCan.collected = true;
            collectedSprayCans.Add(sprayCan.gameObject);

            // Auto-equip the first collected spray can
            if (equippedIndex == -1)
            {
                EquipSprayCan(0);
                sprayCan.gameObject.SetActive(true);
                sprayCan.isEquipped = true;
            }
        }
    }

    private void CycleSprayCan()
    {
        if (collectedSprayCans.Count == 0) return; // No cans collected

        equippedIndex = (equippedIndex + 1) % collectedSprayCans.Count; // Loop through cans
        EquipSprayCan(equippedIndex);
    }

    private void EquipSprayCan(int index)
    {
        equippedIndex = index;
        Debug.Log("Equipped Spray Can: " + collectedSprayCans[equippedIndex]);
      
        for (int i = 0; i < collectedSprayCans.ToArray().Length; i++) {
            collectedSprayCans[i].SetActive(false);
        }

        collectedSprayCans[equippedIndex].SetActive(true);
    }
}
