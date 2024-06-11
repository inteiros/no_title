using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour
{
    public Image blackScreen;
    public Text messageText;
    public Text gameName;

    void Start()
    {
        StartCoroutine(ShowGameNameAndFadeIn());
    }

    private IEnumerator ShowGameNameAndFadeIn()
    {
        blackScreen.color = new Color(0, 0, 0, 1);
        messageText.color = new Color(messageText.color.r, messageText.color.g, messageText.color.b, 0);
        gameName.color = new Color(gameName.color.r, gameName.color.g, gameName.color.b, 0);

        gameName.text = "no_title";
        yield return StartCoroutine(FadeTextIn(gameName, 1f));

        yield return new WaitForSeconds(2f);

        yield return StartCoroutine(FadeTextOut(gameName, 1f));

        messageText.text = "I`m almost out of ammo...";
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        blackScreen.color = new Color(0, 0, 0, 1);
        messageText.color = new Color(messageText.color.r, messageText.color.g, messageText.color.b, 1);

        yield return new WaitForSeconds(0.6f);

        float duration = 1f;
        float currentTime = 0f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            blackScreen.color = new Color(0, 0, 0, 1 - (currentTime / duration));
            yield return null;
        }

        blackScreen.color = new Color(0, 0, 0, 0);

        yield return new WaitForSeconds(0.5f);
        messageText.text = "I'll need some more if I want to survive this hell.";

        yield return new WaitForSeconds(1.5f);

        currentTime = 0f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            messageText.color = new Color(messageText.color.r, messageText.color.g, messageText.color.b, 1 - (currentTime / duration));
            yield return null;
        }

        messageText.color = new Color(messageText.color.r, messageText.color.g, messageText.color.b, 0);

        gameObject.SetActive(false);
    }

    private IEnumerator FadeTextIn(Text text, float duration)
    {
        float currentTime = 0f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            text.color = new Color(text.color.r, text.color.g, text.color.b, currentTime / duration);
            yield return null;
        }
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
    }

    private IEnumerator FadeTextOut(Text text, float duration)
    {
        float currentTime = 0f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            text.color = new Color(text.color.r, text.color.g, text.color.b, 1 - (currentTime / duration));
            yield return null;
        }
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
    }
}
