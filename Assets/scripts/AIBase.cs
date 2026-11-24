using System;
using Unity.VisualScripting;
using UnityEngine;

struct Chromosome
{
    int fitnessScore;
};
public class AIBase : MonoBehaviour
{

    [SerializeField] private float distanceToPlayer;
    [SerializeField] private Vector3 playerPosition;
    [SerializeField] private bool canBeSeen;
    [SerializeField] private float distanceToEndGoal;
    [SerializeField] private float pDotA;

    public void SetDistanceToPlayer(Vector3 PlayerPosition)
    {
        this.playerPosition = PlayerPosition;
       
        distanceToPlayer = Vector3.Distance(transform.position, PlayerPosition);
        pDotA = Vector3.Dot(PlayerPosition, transform.position);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Evaluate()
    {

    }

    void Simulate()
    {

    }

    void Select()
    {

    }
}
