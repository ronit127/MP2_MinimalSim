using UnityEngine;
using TMPro;

public class PowerUpStationScript : MonoBehaviour
{
    public AppleCalc manager;
    public TextMeshProUGUI productionRateText;
    public TextMeshProUGUI buttonText;

    public float price = 100f;
    public float productionPowerMultiplier = 2f;
    public int maxPurchases = 5;
    private int purchases = 0;

    public void Start()
    {
        buttonText.text = "Cost " + price;
    }

    public void OnStationClicked()
    {
        if (manager.apples >= price)
        {
            if (purchases >= maxPurchases)
            {
                buttonText.text = "Sold Out!";
                return;
            }

            manager.apples -= price;
            manager.generationRate *= productionPowerMultiplier;
            productionRateText.text = "Production x " + productionPowerMultiplier;
            productionPowerMultiplier *= 2; 
            price *= 2;
            purchases += 1;

            buttonText.text = "Cost " + price;
        }
    }
}
