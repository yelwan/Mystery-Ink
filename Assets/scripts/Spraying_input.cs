using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GameColor
{
    Red,
    Green,
    Blue,
    Yellow,
    White,
    Black,
    Unknown
}

public class SprayController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField]  CollectSpraycan _spraycan;
    [SerializeField] InputManager _inputManager;
    [SerializeField]  PlayerController _playerInputManager;
    [SerializeField]  MaskManager _maskManager;
    [SerializeField]  ParticleSystem _sprayParticles;
    [SerializeField] List<ParticleCollisionEvent> collisionEvents;
    [Header("Settings")]

    private float sprayTimeAccumulator = 0f;
    private const float timePerUnit = 3f; // 3 seconds of spray = 1 unit


    private bool _isSpraying = false;

    private void Awake()
    {
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    private void Update()
    {
        if (!CanSpray()) return;

        HandleSprayInput();

        if (_isSpraying)
        {
            // Only accumulate if spray amount > 0
            if (_spraycan.amount > 0)
            {
                sprayTimeAccumulator += Time.deltaTime;

                if (sprayTimeAccumulator >= timePerUnit)
                {
                    sprayTimeAccumulator -= timePerUnit;
                    _spraycan.amount--;
                    Debug.Log("Amount is " + _spraycan.amount);
                    if (_spraycan.amount <= 0)
                    {
                        StopSpraying();
                    }
                }
            }
            else
            {
                StopSpraying();
            }
        }
    }


    private bool CanSpray()
    {
        return _spraycan != null && _spraycan.collected;
    }

    private void HandleSprayInput()
    {
        if (_inputManager.GetEPressed())
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
        if (_playerInputManager.GetMoveDirection() == Vector2.zero) return;

        Vector2 moveDir = _playerInputManager.GetMoveDirection();
        float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
        _sprayParticles.transform.rotation = Quaternion.Euler(-angle, 90, 90);
    }
    private void OnParticleCollision(GameObject other)
    {
        if (null == other.GetComponent<MaskManager>() || GetGameColor(other.GetComponent<SpriteRenderer>().color) != GetGameColor(_sprayParticles.main.startColor.color)) return;

        _maskManager.activateInteraction();
    }
    private static GameColor GetGameColor(Color color)
    {
        Color32 c = color;
        if (c.Equals((Color32)Color.red)) return GameColor.Red;
        if (c.Equals((Color32)Color.green)) return GameColor.Green;
        if (c.Equals((Color32)Color.blue)) return GameColor.Blue;
        if (c.Equals((Color32)Color.yellow)) return GameColor.Yellow;
        if (c.Equals((Color32)Color.white)) return GameColor.White;
        if (c.Equals((Color32)Color.black)) return GameColor.Black;

        return GameColor.Unknown;
    }

}