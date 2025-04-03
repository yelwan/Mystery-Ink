using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SprayParticles : MonoBehaviour
{
    public ParticleSystem _sprayParticles;
    [SerializeField] GameObject splat;
    [SerializeField] GameObject player;
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent> ();
    public Vector2 hit;
    void Start()
    {
        hit = Vector2.zero;
    }
    void OnParticleCollision(GameObject other)
    {
        if (other == player) return;
        int numCollisionEvents = _sprayParticles.GetCollisionEvents(other,collisionEvents);
        for (int i = 0; i < numCollisionEvents; i++)
        {
            Vector2 hit = collisionEvents[i].intersection;
            PoolingSystem.instance.CreateProjectile(hit);
        }
    }

   

}
