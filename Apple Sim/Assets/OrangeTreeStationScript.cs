using UnityEngine;
using TMPro;

public class OrangeTreeStationScript : MonoBehaviour
{
    public AppleCalc manager;
    public TextMeshProUGUI statusText;
    public TextMeshProUGUI currentProductionText;
    public GameObject clickerButton;
    public MeshFilter treeVisual;
    public Mesh[] treeLevels;

    public float price = 10f;
    public float productionPowerChange = 1f;
    private float currProductionPower = 0f;
    private bool isOwned = false;
    private int upgradeCount = 0;
    public int clickerRate = 1;

    private float lastClickTime = -999f;
    private float clickCooldown = 0.1f;

    public GameObject fruitPrefab;
    public Transform fruitSpawnPoint;
    private float fruitSpawnTimer = 0f;
    private const float MinSpawnInterval = 0.05f;

    public void Start()
    {
        treeVisual.gameObject.SetActive(false);
        clickerButton.SetActive(false);
        statusText.text = "Buy: " + (int)price + " oranges";
        currentProductionText.text = "";
    }

    void Update()
    {
        if (!isOwned || currProductionPower <= 0f || fruitPrefab == null) return;

        fruitSpawnTimer += Time.deltaTime;
        float spawnInterval = Mathf.Max(MinSpawnInterval, 1f / currProductionPower);

        while (fruitSpawnTimer >= spawnInterval)
        {
            fruitSpawnTimer -= spawnInterval;
            SpawnFruit();
        }
    }

    void SpawnFruit()
    {
        Vector3 origin = fruitSpawnPoint != null ? fruitSpawnPoint.position : transform.position + Vector3.up * 2f;
        Vector3 offset = new Vector3(Random.Range(-0.4f, 0.4f), 0f, Random.Range(-0.4f, 0.4f));
        Instantiate(fruitPrefab, origin + offset, Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f)));
    }

    public void OnStationClicked()
    {
        if (manager.oranges >= price && upgradeCount < treeLevels.Length)
        {
            manager.oranges -= price;

            if (!isOwned)
            {
                isOwned = true;
                treeVisual.gameObject.SetActive(true);
                clickerButton.SetActive(true);

                if (treeLevels != null && treeLevels.Length > 0)
                    treeVisual.mesh = treeLevels[0];

                manager.orangeGenerationRate += productionPowerChange;
                currProductionPower += productionPowerChange;
                upgradeCount++;
                price = Mathf.Round(price * 2f);
            }
            else
            {
                if (upgradeCount < treeLevels.Length)
                {
                    treeVisual.mesh = treeLevels[upgradeCount];
                    manager.orangeGenerationRate += productionPowerChange;
                    currProductionPower += productionPowerChange;
                    upgradeCount++;
                    price = Mathf.Round(price * 1.5f);
                }
            }

            currentProductionText.text = currProductionPower.ToString("F1") + " oranges/s";

            if (upgradeCount >= treeLevels.Length)
                statusText.text = "Max Level!";
            else
                statusText.text = "Upgrade: " + (int)price + " oranges";
        }
    }

    public void OnClickerClicked()
    {
        if (!isOwned) return;
        if (Time.time - lastClickTime < clickCooldown) return;
        lastClickTime = Time.time;
        manager.oranges += clickerRate;
    }

    public void ApplyPowerUp(float multiplier)
    {
        if (!isOwned) return;
        currProductionPower *= multiplier;
        currentProductionText.text = currProductionPower.ToString("F1") + " oranges/s";
    }
}
