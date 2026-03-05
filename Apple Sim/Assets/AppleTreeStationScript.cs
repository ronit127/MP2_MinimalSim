using UnityEngine;
using TMPro;

public class AppleTreeStation : MonoBehaviour
{
    public AppleCalc manager;
    public TextMeshProUGUI statusText;
    public TextMeshProUGUI currentProductionText;
    public GameObject clickerButton;
    public MeshFilter treeVisual;
    public Mesh[] treeLevels;

    public float price = 10f;
    public float productionPowerChange = 1f;
    private float currProductionPower = 1f;
    private bool isOwned = false;
    private int upgradeCount = 0;
    public int clickerRate = 1;

    private float lastClickTime = -999f;
    private float clickCooldown = 0.1f;

    public void Start()
    {
        treeVisual.gameObject.SetActive(false);
        clickerButton.SetActive(false);
    }

    public void OnStationClicked()
    {
        if (manager.apples >= price && upgradeCount < treeLevels.Length)
        {
            manager.apples -= price;

            if (!isOwned)
            {
                isOwned = true;
                treeVisual.gameObject.SetActive(true);
                clickerButton.SetActive(true);

                if (treeLevels != null && treeLevels.Length > 0)
                {
                    treeVisual.mesh = treeLevels[0];
                }

                manager.generationRate += productionPowerChange;
                currProductionPower += productionPowerChange;
                upgradeCount += 1;
                price = 20f;
            }
            else
            {
                if (upgradeCount < treeLevels.Length)
                {
                    treeVisual.mesh = treeLevels[upgradeCount];
                    manager.generationRate += productionPowerChange;
                    currProductionPower += productionPowerChange;
                    upgradeCount += 1;
                    price *= 1.5f;
                }
            }

            currentProductionText.text = isOwned ? (int) currProductionPower + " apples per second" : "";

            if (upgradeCount >= treeLevels.Length)
            {
                statusText.text = "Max Level!";
            }
            else
            {
                statusText.text = isOwned ? "Upgrade: " + (int)price + " apples" : "Buy: " + (int)price + " apples";
            }
        }
    }
    
    public void OnClickerClicked()
    {
        if (!isOwned) return;

        if (Time.time - lastClickTime < clickCooldown) return;
        lastClickTime = Time.time;

        manager.apples += clickerRate;
    }

    public void ApplyPowerUp(float multiplier)
    {
        if (!isOwned) return;

        currProductionPower *= multiplier;
        currentProductionText.text = (int)currProductionPower + " apples per second";
    }
}