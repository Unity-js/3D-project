using UnityEngine;
using TMPro;

public class PlayerLook : MonoBehaviour
{
    public float raycastDistance = 7f; 
    public TextMeshProUGUI infoText;
    void Update()
    {
        if (infoText == null)
        {
            Debug.LogWarning("infoText가 할당되지 않았습니다");
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject.GetComponent<Interactable>() != null)
            {
                ObjectInfo objectInfo = hitObject.GetComponent<ObjectInfo>();
                if (objectInfo != null)
                {
                    int index = GetObjectIndex(hitObject);
                    infoText.text = objectInfo.GetDescription(index);
                }
            }
        }
        else
        {
            infoText.text = "";
        }
    }


    private int GetObjectIndex(GameObject hitObject)
    {
        switch (hitObject.name)
        {
            case "Object1":
                return 0;
            case "Object2":
                return 1;
            case "Object3":
                return 2;
            default:
                return 0;
        }
    }
}
