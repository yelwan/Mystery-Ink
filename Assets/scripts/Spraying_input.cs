using UnityEngine;

public class Spraying_input : MonoBehaviour
{
    private ParticleSystem spray;
    public bool collected = false;
    public CollectSpraycan spraycan;
    public InputManager inputManager; // Reference to your InputManager script

    void Start()
    {
        spray = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        collected = spraycan.collected;

        if (collected)
        {
            if (Input.GetKey(KeyCode.E))
            {
                if (!spray.isPlaying)
                {
                    spray.Play();
                }

                // Get move direction from InputManager
                Vector2 moveDir = inputManager.moveDirection;

                // Only update rotation if there's movement
                if (moveDir != Vector2.zero)
                {
                    float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
                    float sign = angle / -angle;
                    spray.transform.rotation = Quaternion.Euler(-angle, 90, 90);
                }
            }
            else
            {
                spray.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }
        }
    }
}
