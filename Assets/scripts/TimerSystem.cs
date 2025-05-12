using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerSystem : MonoBehaviour
{
    [SerializeField] TimerUI timerUI; 

    private int currentTimerValue;

    public void StartTimer(int timer)
    {
        currentTimerValue = timer;
        StartCoroutine(Countdown());
        StartCoroutine(timerUI.FadeInLabel());
    }

    public void StopTimer()
    {
        StopAllCoroutines();
    }

    private IEnumerator Countdown()
    {
        while (currentTimerValue > 0)
        {
            timerUI.UpdateTimerUI(currentTimerValue);  
            yield return new WaitForSeconds(1f);  
            currentTimerValue--;
        }

        timerUI.TimerEnded();
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(2);
    }
}
