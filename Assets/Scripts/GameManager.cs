using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text messageText;
    public Image fadeImage;
    public RawImage crosshairImage;
    public float fadeDuration = 1f;
    public bool endgame = false;
    private bool hasShownSeePlayerText = false;

    private int totalAmmoCount;

    private void Start()
    {
        Color fadeColor = fadeImage.color;
        fadeColor.a = 0f;
        fadeImage.color = fadeColor;

        totalAmmoCount = GameObject.FindGameObjectsWithTag("GunAmmo").Length;
        messageText.enabled = false;
    }

    public void OnAmmoCollected()
    {
        totalAmmoCount--;

        if (totalAmmoCount <= 0)
        {
            StartCoroutine(EndGameSequenceByAmmo());
        }
    }

    public void OnPlayerAttacked()
    {
        if (!endgame)
        {
            StartCoroutine(EndGameSequenceByAttack());
        }
    }

    public void OnFirstSeePlayer()
    {
        if (!hasShownSeePlayerText)
        {
            StartCoroutine(ShowSeePlayerText());
            hasShownSeePlayerText = true;
        }
    }

    private IEnumerator ShowSeePlayerText()
    {
        messageText.text = "Those creatures again...";
        messageText.enabled = true;

        yield return new WaitForSeconds(1.5f);

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Clamp01(1 - (elapsedTime / fadeDuration));
            messageText.color = new Color(messageText.color.r, messageText.color.g, messageText.color.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        messageText.enabled = false;
        messageText.color = new Color(messageText.color.r, messageText.color.g, messageText.color.b, 1);
    }

    private IEnumerator EndGameSequenceByAmmo()
    {
        endgame = true;
        messageText.text = "I might have enough ammo to survive...";
        messageText.enabled = true;

        yield return new WaitForSeconds(1f);

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            crosshairImage.color = new Color(crosshairImage.color.r, crosshairImage.color.g, crosshairImage.color.b, 1 - alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, 1f);
        crosshairImage.color = new Color(crosshairImage.color.r, crosshairImage.color.g, crosshairImage.color.b, 0f);

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator EndGameSequenceByAttack()
    {
        endgame = true;

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            crosshairImage.color = new Color(crosshairImage.color.r, crosshairImage.color.g, crosshairImage.color.b, 1 - alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, 1f);
        crosshairImage.color = new Color(crosshairImage.color.r, crosshairImage.color.g, crosshairImage.color.b, 0f);

        messageText.text = "I never thought I would die like that...";
        messageText.enabled = true;

        yield return new WaitForSeconds(2.5f);

        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Clamp01(1 - (elapsedTime / fadeDuration));
            messageText.color = new Color(messageText.color.r, messageText.color.g, messageText.color.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        messageText.enabled = false;

        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
