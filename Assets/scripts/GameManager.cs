using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int Timer = 5;

    // Update is called once per frame
    void Start()
    {
        StartCoroutine("Countdown", Timer);
    }

    IEnumerator Countdown(int timer)
    {
        yield return new WaitForSeconds(timer);
        Debug.Log("This is the timer finished!");
        SceneManager.LoadScene(3);
    }
}
