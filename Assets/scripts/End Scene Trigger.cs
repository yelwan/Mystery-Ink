using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneTriggers : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Collider2D player;
    const int EndScene = 2;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != player) return;
         
        SceneManager.LoadScene(EndScene);

    }
}