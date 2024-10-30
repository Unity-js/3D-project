using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemData[] itemDataArray;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                

                ItemUsage itemUsage = player.GetComponent<ItemUsage>();
                if (itemUsage != null)
                {
                    foreach (ItemData itemData in itemDataArray)
                    {
                        
                        itemUsage.UseItem(itemData); 
                    }
                }
               
                Destroy(gameObject);
            }
        }
    }
}
