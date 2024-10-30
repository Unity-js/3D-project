using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item")]
public class ItemData : ScriptableObject
{
    public string itemName;       // 아이템 이름
    [TextArea]
    public string description;     // 아이템 설명
    public float effectDuration;   // 효과 지속 시간
    public float jumpBoost;        // 점프력 증가
    public float speedBoost;       // 이동 속도 증가
}
