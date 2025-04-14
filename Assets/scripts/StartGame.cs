using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] int time = 2;
    void Start()
    {
        StartCoroutine(OpenLevel1(time));
    }

    IEnumerator OpenLevel1(int time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(1); // Now it waits before loading
    }
}
