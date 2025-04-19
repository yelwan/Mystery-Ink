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
    [SerializeField] EndSceneTrigger trigger;


    private Coroutine countdownCoroutine;
    private bool hasFadedOut = false;
    private bool countdownActive = false;


    private void Awake()
    {
        Timer = Manager.Timer;
    }

    private void Start()
    {
        TimerUIActivation();
    }

    public void TimerUIActivation()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        countdownLabel = root.Q<Label>("Countdown");
        labelElement = countdownLabel;

        labelElement.style.opacity = 0f;

        StartCoroutine(FadeInLabel());
        countdownCoroutine = StartCoroutine(Countdown(Timer));
    }

    private void Update()
    {
        if (trigger.GameDone && !hasFadedOut)
        {
            hasFadedOut = true;

            if (countdownActive && countdownCoroutine != null)
            {
                StopCoroutine(countdownCoroutine);
            }

            StartCoroutine(FadeOutLabel());
        }


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

        labelElement.style.opacity = 1f; 
    }
    IEnumerator Countdown(int timer)
    {
        countdownActive = true;

        while (Timer > 0)
        {
            countdownLabel.text = Timer.ToString();
            yield return new WaitForSeconds(1f);
            Timer--;

            if (trigger.GameDone)
            {
                yield break;
            }
        }

        countdownActive = false;

        yield return new WaitForSeconds(2f);
        if (!hasFadedOut)
        {
            hasFadedOut = true;
            StartCoroutine(FadeOutLabel());
        }
    }

    IEnumerator FadeOutLabel() //Fade out at the end of each level to start a new timer later on
    {
        float duration = 1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            labelElement.style.opacity = alpha;
            elapsed += Time.deltaTime;
            yield return null;
        }

        labelElement.style.opacity = 0f;
    }

}
