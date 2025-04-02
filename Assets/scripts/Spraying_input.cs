using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SprayController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private CollectSpraycan _spraycan;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private MaskManager _maskManager;
    [SerializeField] private ParticleSystem _sprayParticles;
    [SerializeField] List<ParticleCollisionEvent> collisionEvents;
    [Header("Settings")]
    [SerializeField] private KeyCode _sprayKey = KeyCode.E;

 
    private bool _isSpraying = false;

    private void Awake()
    {
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    private void Update()
    {
        if (!CanSpray()) return;

        HandleSprayInput();
    }

    private bool CanSpray()
    {
        return _spraycan != null && _spraycan.collected;
    }

    private void HandleSprayInput()
    {
        if (Input.GetKey(_sprayKey))
        {
            StartSpraying();
            UpdateSprayDirection();
        }
        else
        {
            StopSpraying();
        }
    }

    private void StartSpraying()
    {
        if (_isSpraying) return;

        _sprayParticles.Play();
        _isSpraying = true;
    }

    private void StopSpraying()
    {
        if (!_isSpraying) return;

        _sprayParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        _isSpraying = false;
    }

    private void UpdateSprayDirection()
    {
        if (_inputManager.moveDirection == Vector2.zero) return;

        Vector2 moveDir = _inputManager.moveDirection;
        float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
        _sprayParticles.transform.rotation = Quaternion.Euler(-angle, 90, 90);
    }
    

    private void OnParticleCollision(GameObject other)
    {
        if (null == other.GetComponent<MaskManager>() || other.GetComponent<SpriteRenderer>().color != _sprayParticles.main.startColor.color) return;
        _maskManager.activateInteraction();
    }

}