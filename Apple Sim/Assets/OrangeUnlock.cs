using UnityEngine;
using TMPro;

public class OrangeUnlock : MonoBehaviour
{
    public AppleCalc manager;

    public GameObject tutorialPanel;       // UI panel with tutorial text
    public TextMeshProUGUI buttonText;

    public Renderer gateRenderer;
    public Color unlockedColor = Color.green;

    public float unlockCost = 30f;

    private bool unlocked = false;

    public void OnUnlockClicked()
    {
        if (unlocked) return;

        if (manager.apples >= unlockCost)
        {
            manager.apples -= unlockCost;

            tutorialPanel.SetActive(true);  // show tutorial

            unlocked = true;

            if (gateRenderer != null)
                gateRenderer.material.color = unlockedColor;

            if (buttonText != null)
                buttonText.text = "Unlocked";
        }
    }
}