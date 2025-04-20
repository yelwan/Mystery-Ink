using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TimerSystem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] int Timer;
    [SerializeField] int TimerL2;
    [SerializeField] int TimerL3;
    [SerializeField] GameManager Manager;
    private Label countdownLabel;
    private VisualElement labelElement;
    [SerializeField] EndSceneTrigger trigger;


    public Coroutine countdownCoroutine;
    private bool hasFadedOut = false;
    private bool countdownActive = false;
    private bool level2TimerStarted = false;
    private int currentTimerValue; // Add this with your other variables

    private void Awake()
    {
        Timer = Manager.Timer;
        TimerL2 = Manager.TimerL2;
        TimerL3 = Manager.TimerL3;
    }

    private void Start()
    {
        TimerUIActivation(Timer);
    }

    public void TimerUIActivation(int TimeNUM)
    {
        // Reset state variables
        hasFadedOut = false;
        countdownActive = false;

        var root = GetComponent<UIDocument>().rootVisualElement;
        countdownLabel = root.Q<Label>("Countdown");
        labelElement = countdownLabel;

        labelElement.style.opacity = 0f;

        StartCoroutine(FadeInLabel());
        Debug.Log("Faded in Label");
        countdownCoroutine = StartCoroutine(Countdown(TimeNUM));
    }

    private void Update()
    {
        if (trigger.GameDone && !hasFadedOut)
        {
            hasFadedOut = true;

/*            if (countdownActive && countdownCoroutine != null)
            {
                StopCoroutine(countdownCoroutine);
            }*/

            StartCoroutine(FadeOutLabel());
        }

        if (Manager.CurrentLevel == 2 && !level2TimerStarted)
        {
            level2TimerStarted = true;
            trigger.GameDone = false;
            TimerUIActivation(TimerL2);
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

    public IEnumerator Countdown(int timer)
    {
        currentTimerValue = timer; // Initialize the current timer value
        countdownActive = true;

        while (currentTimerValue > 0)
        {
            countdownLabel.text = currentTimerValue.ToString();
            yield return new WaitForSeconds(1f);
            currentTimerValue--;
            Debug.Log(currentTimerValue.ToString());
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
