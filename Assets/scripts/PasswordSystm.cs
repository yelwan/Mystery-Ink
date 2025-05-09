using UnityEngine;
using UnityEngine.UIElements;

public class PasswordSystem : MonoBehaviour
{
    private TextField pass;
    private VisualElement labelElement;

    public bool displayed = false;
    [SerializeField] Collider2D collider_Pass;
    [SerializeField] Pass_Trigger_NOTUI trigger;

    [SerializeField] Collider2D door;
    [SerializeField] Collider2D pillar;


    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        pass = root.Q<TextField>("TextField");
        labelElement = pass;
        labelElement.style.opacity = 0f;
    }

    void Update()
    {

        if (trigger.triggered && !displayed)
        {
            displayed = true;
            labelElement.style.opacity = 1f;
        }

      
            foreach (char c in Input.inputString)
            {
                if (c == '\b')
                {
                    if (pass.value.Length > 0)
                        pass.value = pass.value.Substring(0, pass.value.Length - 1);
                }
                else if (c == '\n' || c == '\r')
                {
                    Debug.Log("Enter Pressed: " + pass.value);
                }
                else
                {
                    pass.value += c;
                }
            }
        

        if (!trigger.open)
        {
            labelElement.style.opacity = 0f;
        }

        if (trigger.open)
        {
            labelElement.style.opacity = 1f;
        }

        if (pass.value == "cat")
        {
            door.enabled = false;
            pillar.enabled = false;
        }
    }
}
