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
    private float cumulativeMultiplier = 1f;

    private AppleTreeStation[] cachedAppleTrees;
    private OrangeTreeStationScript[] cachedOrangeTrees;

    private float lastClickTime = -999f;
    private float clickCooldown = 0.1f;

    public void Start()
    {
        buttonText.text = "Boost: " + (int)price + " apples";
        productionRateText.text = "Boost x1.0";
        cachedAppleTrees = FindObjectsOfType<AppleTreeStation>();
        cachedOrangeTrees = FindObjectsOfType<OrangeTreeStationScript>();
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
            manager.orangeGenerationRate *= productionMultiplier;
            cumulativeMultiplier *= productionMultiplier;

            foreach (AppleTreeStation t in cachedAppleTrees)
                t.ApplyPowerUp(productionMultiplier);

            foreach (OrangeTreeStationScript t in cachedOrangeTrees)
                t.ApplyPowerUp(productionMultiplier);

            productionRateText.text = "Boost x" + cumulativeMultiplier.ToString("F2");

            productionMultiplier += 0.1f;
            price = Mathf.Round(price * 2f);
            purchases++;

            buttonText.text = purchases >= maxPurchases ? "Sold Out!" : "Boost: " + (int)price + " apples";
        }
    }
}
