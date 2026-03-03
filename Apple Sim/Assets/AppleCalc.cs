using UnityEngine;
using TMPro;
public class AppleCalc : MonoBehaviour
{
    public float apples;
    public float generationRate;

    public TextMeshProUGUI textDisplay;

    void Update()
    {
        apples += generationRate * Time.deltaTime;
        textDisplay.text = "Apples: " + Mathf.FloorToInt(apples);
    }
}