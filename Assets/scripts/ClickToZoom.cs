using UnityEngine;

public class ClickToZoom : MonoBehaviour
{
    [SerializeField] CameraZoomIn cam;
    bool zoomedIn = false; //used to track if player zoomed in on object or not
    void OnMouseDown()
    {
        // Zooms in on object, blurs/ whitens/ blackens everything else
        if(zoomedIn)
        {
            zoomedIn = false;
            cam.ZoomToPlayer2();
        }
        else if(!zoomedIn)
        {
            zoomedIn = true;
            cam.ZoomToObject(transform.position.x, transform.position.y, transform.position.z);
        }
    }
}
