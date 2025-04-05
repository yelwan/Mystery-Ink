using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SprayParticles : MonoBehaviour
{
    [SerializeField] PoolingSystem _poolingSystem;
    [SerializeField] ParticleSystem _sprayParticles;
    [SerializeField] GameObject splat;
    [SerializeField] GameObject player;
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent> ();
  
    void OnParticleCollision(GameObject other)
    {
        if (other == player) return;
        int numCollisionEvents = _sprayParticles.GetCollisionEvents(other,collisionEvents);
        for (int i = 0; i < numCollisionEvents; i++)
        {
            Vector2 hit = collisionEvents[i].intersection;
            _poolingSystem.CreatedProjectilePublic(hit);
        }
    }

   

}
