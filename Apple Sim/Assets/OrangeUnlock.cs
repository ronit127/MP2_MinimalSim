using UnityEngine;
using TMPro;

public class OrangeUnlock : MonoBehaviour
{
    public AppleCalc manager;

    public GameObject tutorialPanel;       // UI panel with tutorial text
    public TextMeshProUGUI buttonText;

    public Renderer gateRenderer;
    public Color unlockedColor = Color.green;

    private bool unlocked = false;

    public void OnUnlockClicked()
    {
        if (unlocked) return;

        if (manager.apples >= manager.unlockCost)
        {
            manager.apples -= manager.unlockCost;

            tutorialPanel.SetActive(true);  // show tutorial

            unlocked = true;

            if (gateRenderer != null)
                gateRenderer.material.color = unlockedColor;

            if (buttonText != null)
                buttonText.text = "Unlocked";
        }
    }
}