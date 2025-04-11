using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TimerSystem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] int Timer;
    [SerializeField] GameManager Manager;
    private Label countdownLabel;
    private VisualElement labelElement;


    private void Awake()
    {
        Timer = Manager.Timer;
    }

    private void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        countdownLabel = root.Q<Label>("Countdown");
        labelElement = countdownLabel;

        // Set initial opacity to 0 (fully transparent)
        labelElement.style.opacity = 0f;

        StartCoroutine(FadeInLabel());
        StartCoroutine(Countdown(Timer));
    }

    IEnumerator FadeInLabel()
    {
        yield return new WaitForSeconds(3f); // wait before starting fade

        float duration = 1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsed / duration);
            labelElement.style.opacity = alpha;
            elapsed += Time.deltaTime;
            yield return null;
        }

        labelElement.style.opacity = 1f; // make sure it's fully visible
    }

    IEnumerator Countdown(int timer)
    {
        while (Timer > 0)
        {
            countdownLabel.text = Timer.ToString();
            yield return new WaitForSeconds(1f);
            Timer--;
        }
        countdownLabel.text = "Time's up!";
    }

}
