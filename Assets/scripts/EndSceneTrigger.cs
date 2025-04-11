using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Collider2D player;
    const int EndScene = 2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != player) return;

        SceneManager.LoadScene(2);

    }
}
