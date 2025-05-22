using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class TimerUI : MonoBehaviour
{
    [SerializeField] TimerSystem timer = null;
    [SerializeField] Label countdownLabel;
    private Label labelElement;
    private VisualElement Instructions;

    private void Awake()
    {
        timer.OnStarted += onStarted;
        timer.OnEnded += onEnded;
        timer.OnTick += onTick;
        timer.OnStopped += onStopped;
        var root = GetComponent<UIDocument>().rootVisualElement;
        labelElement = root.Q<Label>("Countdown");
        labelElement.style.opacity = 0f;

    }

    void OnDestroy()
    {
        timer.OnStarted -= onStarted;
        timer.OnEnded -= onEnded;
        timer.OnTick -= onTick;
        timer.OnStopped -= onStopped;
    }

    void onStarted(ITimer i_timer)
    {
        StartCoroutine(FadeInLabel());     
    }

    void onEnded (ITimer i_timer)
    {
        StartCoroutine(FadeOutLabel());
    }

    void onStopped(ITimer i_timer)
    {
        StopAllCoroutines();
    }

    void onTick (ITimer i_timer, int value)
    {
        labelElement.text = value.ToString();
    }

    public IEnumerator FadeInLabel()
    {
        yield return new WaitForSeconds(3f);  

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

    private IEnumerator FadeOutLabel()
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
