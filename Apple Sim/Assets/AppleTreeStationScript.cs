using UnityEngine;
using TMPro;

public class AppleTreeStation : MonoBehaviour
{
    public AppleCalc manager;
    public TextMeshProUGUI statusText;
    public TextMeshProUGUI currentProductionText;
    public MeshFilter treeVisual;
    public Mesh[] treeLevels;

    public float price = 10f;
    public float productionPowerChange = 1f;
    private float currProductionPower = 1f;
    private bool isOwned = false;
    private int upgradeCount = 0;

    public void Start()
    {
        treeVisual.gameObject.SetActive(false);
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
                price = 20f;
            }
            else
            {
                treeVisual.mesh = treeLevels[upgradeCount];
                manager.generationRate += productionPowerChange;
                currProductionPower += productionPowerChange;
                upgradeCount += 1;
                price *= 1.5f;
            }
            statusText.text = isOwned ? "Upgrade: " + (int)price + " apples" : "Buy: " + (int)price + " apples";
            currentProductionText.text = isOwned ? (int) currProductionPower + " apples per second" : "";

            if (upgradeCount == treeLevels.Length - 1)
            {
                statusText.text = "Max Level!";
            }
        }
    }
}