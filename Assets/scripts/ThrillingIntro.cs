using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public  class ThrillingIntro : MonoBehaviour
{
    [SerializeField] InputManager inputManager;
    [SerializeField] moveDoor door;
    [SerializeField]  TextMeshProUGUI titleText;         // "Mystery Ink"
    [SerializeField]  TextMeshProUGUI chapterText;       // "Chapter 1"
    [SerializeField]  TextMeshProUGUI taglineText;       // "Where am I..?"
    [SerializeField]  TextMeshProUGUI redirectingText;   // "Redirecting..."

    [SerializeField]  float fadeDuration = 1f;           // Fade duration for each text
    [SerializeField]  float betweenDelay = 1.2f;         // Delay between each fade
    [SerializeField]  float flickerStartDelay = 1.5f;    // Delay before flicker starts

    // Audio Sources (assign in inspector)
    [SerializeField]  AudioSource flickerSFX;            // Short flickering sound
    [SerializeField]  AudioSource ambientLoop;           // Wind or mystery ambient loop
    [SerializeField] MaskManager maskManager;
    private void Awake()
    {
        if(door != null)
        door.GetComponent<Collider2D>().enabled = false;
        if(inputManager != null)
        inputManager.enabled = false;
        if(door != null)
        door.CloseDoorStart();
        if(inputManager != null)
        inputManager.enabled = true;
        transform.SetParent(null);
    }
    void Start()
    {
        if (redirectingText != null) redirectingText.enabled = false; // Hide at start
        StartCoroutine(PlayIntroSequence());
    }

    IEnumerator PlayIntroSequence()
    {
        yield return new WaitForSeconds(3);
        // Play ambient background
        if (ambientLoop != null)
        {
            ambientLoop.loop = true;
            ambientLoop.Play(); // Looping ambient sound (wind, hum, etc.)
        }

         //Fade in: Title, Chapter, Tagline
       //yield return StartCoroutine(FadeTextIn(titleText));
       //yield return new WaitForSeconds(betweenDelay);
      // yield return StartCoroutine(FadeTextIn(chapterText));
     //  yield return new WaitForSeconds(betweenDelay);
    //   yield return StartCoroutine(FadeTextIn(taglineText));
      // yield return new WaitForSeconds(flickerStartDelay);

        // Start flickering "Redirecting..."
      // yield return StartCoroutine(FlickerRedirecting(redirectingText));

        // Transition to next scene after short delay
        yield return new WaitForSeconds(1.5f);
        
    }

    IEnumerator FadeTextIn(TextMeshProUGUI text)
    {
        float timer = 0f;
        Color originalColor = text.color;
        originalColor.a = 0f;
        text.color = originalColor;

        while (timer < fadeDuration)
        {
            float alpha = timer / fadeDuration;
            text.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            timer += Time.deltaTime;
            yield return null;
        }

        text.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);
    }

    IEnumerator FlickerRedirecting(TextMeshProUGUI text)
    {
        text.enabled = true;

        for (int i = 0; i < 6; i++)
        {
            text.enabled = !text.enabled;

            // Play flicker SFX when it turns on
            if (text.enabled && flickerSFX != null)
                flickerSFX.Play();

            yield return new WaitForSeconds(Random.Range(0.02f, 0.15f));
        }

        text.enabled = true; // Stay visible after flicker

        yield return new WaitForSeconds(2);

        text.enabled = false; 
    }
}
