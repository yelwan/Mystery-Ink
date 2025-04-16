using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class EndSceneTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Collider2D player;
    const int EndScene = 2;
    public moveDoor door;
/*    private Label countdownLabel;
    private VisualElement labelElement;*/

    private void Start()
    {
        //var root = GetComponent<UIDocument>().rootVisualElement;
       /* countdownLabel = root.Q<Label>("Countdown");
        labelElement = countdownLabel;*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != player) return;

        //Time label fades away
        //labelElement.style.opacity = 1f;

        //StartCoroutine(FadeOutLabel());

        // disabling player controller and door collider so player can enter the doorway
        door.GetComponent<Collider2D>().enabled = false;
        player.GetComponent<PlayerController>().enabled = false;

        //Door closing in front of Player
        StartCoroutine(WaitForDoor(1));
        door.CloseDoor();
        StartCoroutine(WaitForDoor(2));
    }

    IEnumerator WaitForDoor(int time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(2);
    }

    IEnumerator FadeOutLabel()
    {
        yield return new WaitForSeconds(1f); // wait before starting fade

        float duration = 1f;
        float elapsed = 0f;


        while (elapsed < duration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsed / duration);
            //labelElement.style.opacity = alpha;
            elapsed -= Time.deltaTime;
            yield return null;
        }

        //labelElement.style.opacity = 0f;
    }
}
