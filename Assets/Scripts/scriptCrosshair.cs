using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class scriptCrosshair : MonoBehaviour
{
    public RawImage crosshairImage;
    public Text tiros;

    void Start()
    {
        crosshairImage.color = new Color(crosshairImage.color.r, crosshairImage.color.g, crosshairImage.color.b, 0f);
        tiros.color = new Color(tiros.color.r, tiros.color.g, tiros.color.b, 0f);
        StartCoroutine(FadeInCrosshairAndTiros(4.6f, 1f));
    }

    private IEnumerator FadeInCrosshairAndTiros(float delay, float duration)
    {
        yield return new WaitForSeconds(delay);

        float elapsedTime = 0f;
        Color crosshairColor = crosshairImage.color;
        Color tirosColor = tiros.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / duration);
            crosshairImage.color = new Color(crosshairColor.r, crosshairColor.g, crosshairColor.b, alpha);
            tiros.color = new Color(tirosColor.r, tirosColor.g, tirosColor.b, alpha);
            yield return null;
        }

        crosshairImage.color = new Color(crosshairColor.r, crosshairColor.g, crosshairColor.b, 1f);
        tiros.color = new Color(tirosColor.r, tirosColor.g, tirosColor.b, 1f);
    }
}
