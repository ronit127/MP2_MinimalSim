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
        isActive = new bool[trophies.Length];
        unlockButton.SetActive(false);
    }

    void Update()
    {
        // Apples
        apples += generationRate * Time.deltaTime;
        textDisplay.text = "Apples: " + Mathf.FloorToInt(apples);

        // Oranges
        oranges += orangeGenerationRate * Time.deltaTime;
        orangeTextDisplay.text = "Oranges: " + Mathf.FloorToInt(oranges);

        if (apples >= unlockCost)
        {
            unlockButton.SetActive(true);
        }

        CheckAchievement(Mathf.FloorToInt(apples + oranges));
    }

    void CheckAchievement(int fruit)
    {
        if (achievementLevel >= trophies.Length) return;
        
        if (!isActive[achievementLevel] && fruit >= thresholds[achievementLevel])
        {
            isActive[achievementLevel] = true;
            trophies[achievementLevel].gameObject.SetActive(true);
            achievementText.text = "Achievement Unlocked!\n" + thresholds[achievementLevel] + " Fruit Collected";
            achievementLevel += 1;
        }
    }
}