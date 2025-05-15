using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface ITimer
{
    float RemainingTime { get; }

    void StartTimer(float i_time);

    void Stop();

    bool IsRunning { get; }
}


public class TimerSystem : MonoBehaviour, ITimer
{
    [SerializeField] TimerUI timerUI;

    private int currentTimerValue;

    public Action<ITimer> OnStarted = null;
    public Action<ITimer> OnEnded = null;
    public Action<ITimer> OnTick = null;

    void OnDestroy()
    {
        OnStarted = null;
        OnEnded = null;
        OnTick = null;
    }

    // Code review : cache timer coroutine reference (this tells us if timr is running already)
    public void StartTimer(float i_timer)
    {
        currentTimerValue = (int)i_timer;
        StartCoroutine(Countdown());

        OnStarted?.Invoke(this);

        StartCoroutine(timerUI.FadeInLabel());
    }

    // Code review : you better call this ! 
    // Release coroutine references
    public void Stop()
    {
        // Call OnStopped
        StopAllCoroutines();
    }

    public bool IsRunning => false; // return if coroutine reference is null;

    public float RemainingTime => 0f; // return timer remaining time

    private IEnumerator Countdown()
    {
        while (currentTimerValue > 0)
        {
            timerUI.UpdateTimerUI(currentTimerValue);

            // Call OnTick
            yield return new WaitForSeconds(1f);
            currentTimerValue--;
        }

        timerUI.TimerEnded();
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(3);
    }
}
