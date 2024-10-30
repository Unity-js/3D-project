using UnityEngine;

public class ItemUsage : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    public void UseItem(ItemData itemData)
    {
        if (itemData.jumpBoost > 0) player.ApplyJumpBoost(2f, itemData.effectDuration);
       
        if (itemData.speedBoost > 0) player.ApplySpeedBoost(2f, itemData.effectDuration);
    }

}
