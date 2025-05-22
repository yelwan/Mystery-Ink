using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public interface IInventoryObserver
{
    public void OnItemEquipped(GameObject item);
    public void OnItemUnequipped(GameObject item);
}

public class InventorySystem : MonoBehaviour, IInventoryObserver
{
    public SprayController CurrentSprayController { get; private set; }
    public List<GameObject> collectedSprayCans = new List<GameObject>();
    private int equippedIndex = -1;
    public bool AllFinished = false;
    [SerializeField] int TotalNumOfCans = 2;
    public Action<IInventoryObserver, GameObject> OnStarted = null;
    public Action<IInventoryObserver, GameObject> OnEnded = null;
    [SerializeField] AudioSource collectCan;
    public string inkColor;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && collectedSprayCans.Count > 1)
        {
            CycleSprayCan();
        }

        /*if (collectedSprayCans.Count > 0)
        {
            AllFinished = IsAllFinished();
            if (AllFinished)
            {
                SceneManager.LoadScene(3);
            }
        }*/
    }

    public void OnItemEquipped(GameObject item)
    {
        OnStarted?.Invoke(this, item);
    }

    public GameObject GetEquippedSprayCan()
    {
        //inkColor = collectedSprayCans[equippedIndex].GetComponent<CollectSpraycan>().colorName;
        return collectedSprayCans[equippedIndex];
    }
    public void OnItemUnequipped(GameObject item)
    {
        OnEnded?.Invoke(this, item);
    }
    public void CollectSprayCan(CollectSpraycan sprayCan)
    {
        if (!collectedSprayCans.Contains(sprayCan.gameObject))
        {
            collectedSprayCans.Add(sprayCan.gameObject);
            sprayCan.collected = true;
            OnItemEquipped(sprayCan.gameObject);
            if (equippedIndex == -1)
            {
                EquipSprayCan(0);
                collectCan.Play();
            }
            else
            {
                sprayCan.gameObject.SetActive(false);
                collectCan.Play();
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
            /////////////////////////////////////////////////////////////////////////////////
            if(i == equippedIndex)
                collectedSprayCans[i].GetComponent<CollectSpraycan>().isEquipped = true;
            else
                collectedSprayCans[i].GetComponent<CollectSpraycan>().isEquipped = false;
            /////////////////////////////////////////////////////////////////////////////////

        }

        CurrentSprayController = collectedSprayCans[equippedIndex].GetComponentInChildren<SprayController>();
        OnItemEquipped(collectedSprayCans[equippedIndex].gameObject);
    }

    private bool IsAllFinished()
    {
        if(collectedSprayCans.Count == TotalNumOfCans)
        {
            for(int i = 0; i<collectedSprayCans.Count; i++)
            {
                if (collectedSprayCans[i].GetComponent<CollectSpraycan>().amount > 0)
                {
                    return false;
                }
            }
            return false;
        }
        return false;
    }
}
