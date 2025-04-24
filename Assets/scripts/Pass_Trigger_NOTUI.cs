using System.Security.Authentication.ExtendedProtection;
using UnityEngine;

public class Pass_Trigger_NOTUI : MonoBehaviour
{
    [SerializeField] Collider2D player;
    public bool triggered = false;
    public bool open = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        triggered = true;
        open = true;
    }

    private void OnMouseDown()
    {
        open = !open;
    }
}
