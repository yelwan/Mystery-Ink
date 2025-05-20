using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Spikes : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] CollectSpraycan _spraycan;
    [SerializeField] SprayController sprayController;
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject != player) return;

        _spraycan.amount--;
        sprayController.SetAmount(_spraycan.amount);
    }
}
