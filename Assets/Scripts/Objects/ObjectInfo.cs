using UnityEngine;

public class ObjectInfo : MonoBehaviour
{
    [TextArea]
    public string[] descriptions = new string[]
    {
        //설명
    };

    public string GetDescription(int index)
    {
        if (index < 0 || index >= descriptions.Length)
            return "설명이 없습니다.";
        return descriptions[index];
    }
}
