using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CollectSpraycan : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Collider2D Can;
    public string color = "Blue";
    public float collectDistance = 3.0f;
    public GameObject player;
    public bool collected = false;

    Color colorColor = Color.blue; //assigning colors

    void Start()
    {
        Can = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if Sugar collides with the candy box
        if (collision.gameObject == player)
        {
            //transform.position = player.transform.position;
            //Add to inventory, make it selectable/ equip-able
        }
    }
}
