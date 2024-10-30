using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item")]
public class ItemData : ScriptableObject
{
    public string itemName;       // ������ �̸�
    [TextArea]
    public string description;     // ������ ����
    public float effectDuration;   // ȿ�� ���� �ð�
    public float jumpBoost;        // ������ ����
    public float speedBoost;       // �̵� �ӵ� ����
}
