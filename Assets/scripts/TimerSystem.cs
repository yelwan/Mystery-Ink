using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface ITimer
{
    float RemainingTime { get; }

    void StartTimer(float i_time);

    void Stop();

    void End();

    bool IsRunning { get; }
}


public class TimerSystem : MonoBehaviour, ITimer
{

    private int currentTimerValue;
    public Action<ITimer> OnStarted = null;
    public Action<ITimer> OnEnded = null;
    public Action<ITimer> OnStopped = null;
    public Action<ITimer, int> OnTick = null;

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
    }

    // Code review : you better call this ! 
    // Release coroutine references
    public void Stop()
    {
        // Call OnStopped
        OnStopped?.Invoke(this);
    }

    public void End()
    {
        OnEnded?.Invoke(this);
    }
    public bool IsRunning => currentTimerValue > 0; // return if coroutine reference is null;

    public float RemainingTime =>  RemainingTime; // return timer remaining time

    private IEnumerator Countdown()
    {
        while (currentTimerValue > 0)
        {

            // Call OnTick
            OnTick?.Invoke(this, currentTimerValue);
            yield return new WaitForSeconds(1f);
            currentTimerValue--;
        }
        End();
        
        yield return new WaitForSeconds(1f);


    }
}
