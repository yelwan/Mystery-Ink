using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public List<GameObject> collectedSprayCans = new List<GameObject>();
    private int equippedIndex = -1; 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CycleSprayCan();
        }
    }

    public void CollectSprayCan(CollectSpraycan sprayCan)
    {
        if (!collectedSprayCans.Contains(sprayCan.gameObject))
        {
            collectedSprayCans.Add(sprayCan.gameObject);
            sprayCan.collected = true; 

            if (equippedIndex == -1)
            {
                EquipSprayCan(0);
            }
            else
            {
                sprayCan.gameObject.SetActive(false);
            }
        }
    }

    private void CycleSprayCan()
    {
        if (collectedSprayCans.Count == 0) return;

        equippedIndex = (equippedIndex + 1) % collectedSprayCans.Count;
        EquipSprayCan(equippedIndex);
    }

    private void EquipSprayCan(int index)
    {
        equippedIndex = index;

        for (int i = 0; i < collectedSprayCans.Count; i++)
        {
            collectedSprayCans[i].SetActive(i == equippedIndex);
        }
    }
}
