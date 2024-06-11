using UnityEngine;
using UnityEngine.UI;

public class Tiros : MonoBehaviour
{
    public int currentTiros = 5;
    public int maxTiros = 20;
    public int reserveTiros = 0;
    public Text tiroText;

    private void Start()
    {
        UpdateUI();
    }

    public void AddTiros(int amount)
    {
        reserveTiros += amount;
        UpdateUI();
    }

    public void Reload()
    {
        int tirosNeeded = maxTiros - currentTiros;
        if (reserveTiros >= tirosNeeded)
        {
            currentTiros = maxTiros;
            reserveTiros -= tirosNeeded;
        }
        else
        {
            currentTiros += reserveTiros;
            reserveTiros = 0;
        }
        UpdateUI();
    }

    public bool CanShoot()
    {
        return currentTiros > 0;
    }

    public void Shoot()
    {
        if (currentTiros > 0)
        {
            currentTiros--;
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        if (tiroText != null)
        {
            tiroText.text = $"{currentTiros}/{reserveTiros}";
        }
    }
}
