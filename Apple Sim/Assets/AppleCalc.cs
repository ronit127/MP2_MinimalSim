using UnityEngine;
using TMPro;

public class AppleCalc : MonoBehaviour
{
    public float apples;
    public float generationRate;

    public float oranges;
    public float orangeGenerationRate;

    public TextMeshProUGUI textDisplay;
    public TextMeshProUGUI orangeTextDisplay;

    public GameObject unlockButton;
    public float unlockCost = 30f;

    [Header("Achievement Trophies")]
    public GameObject[] trophies;
    public int[] thresholds;
    private int achievementLevel = 0;
    bool[] isActive;
    public TextMeshProUGUI achievementText;

    void Start()
    {
        int trophyCount = trophies != null ? trophies.Length : 0;
        int thresholdCount = thresholds != null ? thresholds.Length : 0;
        int arrLen = Mathf.Min(trophyCount, thresholdCount);
        isActive = new bool[arrLen];
        if (unlockButton != null)
            unlockButton.SetActive(false);
    }

    void Update()
    {
        apples += generationRate * Time.deltaTime;
        textDisplay.text = "Apples: " + Mathf.FloorToInt(apples);

        oranges += orangeGenerationRate * Time.deltaTime;
        orangeTextDisplay.text = "Oranges: " + Mathf.FloorToInt(oranges);

        if (unlockButton != null)
            unlockButton.SetActive(apples >= unlockCost);

        CheckAchievement(Mathf.FloorToInt(apples + oranges));
    }

    void CheckAchievement(int fruit)
    {
        if (trophies == null || thresholds == null) return;
        int maxCount = Mathf.Min(trophies.Length, thresholds.Length);
        if (achievementLevel >= maxCount) return;

        if (!isActive[achievementLevel] && fruit >= thresholds[achievementLevel])
        {
            isActive[achievementLevel] = true;
            if (trophies[achievementLevel] != null)
                trophies[achievementLevel].SetActive(true);
            if (achievementText != null)
                achievementText.text = "Achievement Unlocked!\n" + thresholds[achievementLevel] + " Fruit Collected";
            achievementLevel++;
        }
    }
}
