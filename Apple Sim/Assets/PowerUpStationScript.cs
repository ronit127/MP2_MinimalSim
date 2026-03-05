using UnityEngine;
using TMPro;

public class PowerUpStationScript : MonoBehaviour
{
    public AppleCalc manager;
    public TextMeshProUGUI productionRateText;
    public TextMeshProUGUI buttonText;

    public float price = 100f;
    public float productionMultiplier = 1.2f;
    public int maxPurchases = 5;
    private int purchases = 0;

    private float lastClickTime = -999f;
    private float clickCooldown = 0.1f;

    public void Start()
    {
        buttonText.text = "Cost " + price;
    }

    public void OnStationClicked()
    {
        if (Time.time - lastClickTime < clickCooldown) return;
        lastClickTime = Time.time;
    
        if (purchases >= maxPurchases) return;

        if (manager.apples >= price)
        {
            manager.apples -= price;
            manager.generationRate *= productionMultiplier;
            productionRateText.text = "Production x " + productionMultiplier;

            AppleTreeStation[] appleTrees = FindObjectsOfType<AppleTreeStation>();
            foreach (AppleTreeStation appleTree in appleTrees)
            {
                appleTree.ApplyPowerUp(productionMultiplier);
            }

            // OrangeTreeStation[] orangeTrees = FindObjectsOfType<OrangeTreeStation>();
            // foreach (OrangeTreeStation orangeTree in orangeTrees)
            // {
            //     orangeTree.ApplyPowerUp(productionMultiplier);
            // }

            productionMultiplier *= 2f; 
            price *= 2f;
            purchases += 1;

            buttonText.text = (purchases >= maxPurchases) ? "Sold Out!" : ("Cost " + price);
        }
    }
}
