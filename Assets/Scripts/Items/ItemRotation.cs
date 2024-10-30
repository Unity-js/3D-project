using UnityEngine;

public class ItemRotation : MonoBehaviour
{
    public float rotationSpeed = 50f;
    private Material itemMaterial; 

    private void Start()
    {
        itemMaterial = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        float emission = Mathf.PingPong(Time.time, 1);
    }
}
