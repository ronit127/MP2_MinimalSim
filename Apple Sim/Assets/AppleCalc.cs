using UnityEngine;
using TMPro;
public class AppleCalc : MonoBehaviour
{
    public float apples;
    public float generationRate;

    public TextMeshProUGUI textDisplay;

    [Header("Achievement Trophies")]
    public GameObject[] trophies;
    public int[] thresholds;
    private int achievementLevel = 0;
    bool[] isActive;
    public TextMeshProUGUI achievementText;

    void Start()
    {
        isActive = new bool[trophies.Length];
    }

    void Update()
    {
        apples += generationRate * Time.deltaTime;
        textDisplay.text = "Apples: " + Mathf.FloorToInt(apples);

        CheckAchievement(Mathf.FloorToInt(apples));
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