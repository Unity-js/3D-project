using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Text durationText; 
    public ItemData itemData; 

    void Start()
    {
        if (itemData != null)
        {
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        durationText.text = "Effect Duration: " + itemData.effectDuration.ToString("F1") + "s"; 
    }
}
