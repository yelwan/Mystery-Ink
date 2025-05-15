using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class TimerUI : MonoBehaviour
{
    [SerializeField] TimerSystem timer = null;
    [SerializeField] Label countdownLabel;  
    private VisualElement labelElement;

    private void Awake()
    {
        timer.OnStarted += onStarted;

        var root = GetComponent<UIDocument>().rootVisualElement;
        countdownLabel = root.Q<Label>("Countdown");  
        labelElement = countdownLabel;
        labelElement.style.opacity = 0f;  
    }

    void OnDestroy()
    {
        timer.OnStarted -= onStarted;
        
    }

    void onStarted(ITimer i_timer)
    {

        
    }

    public void UpdateTimerUI(int value)
    {
        countdownLabel.text = value.ToString();
    }

    public void TimerEnded()
    {
        StartCoroutine(FadeOutLabel()); 
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
