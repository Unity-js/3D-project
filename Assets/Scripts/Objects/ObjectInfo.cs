using UnityEngine;

public class ObjectInfo : MonoBehaviour
{
    [TextArea]
    public string[] descriptions = new string[]
    {
        //����
    };

    public string GetDescription(int index)
    {
        if (index < 0 || index >= descriptions.Length)
            return "������ �����ϴ�.";
        return descriptions[index];
    }
}
