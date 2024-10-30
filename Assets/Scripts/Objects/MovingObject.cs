using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public Vector3 startPoint;  
    public Vector3 endPoint;
    public float speed;   
    private float journeyLength; 
    private float startTime;     

    void Start()
    {
        journeyLength = Vector3.Distance(startPoint, endPoint);
        startTime = Time.time;
    }

    void Update()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fractionOfJourney = distCovered / journeyLength;

        transform.position = Vector3.Lerp(startPoint, endPoint, Mathf.PingPong(fractionOfJourney, 1));
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.position = new Vector3(transform.position.x, collision.transform.position.y, transform.position.z);
        }
    }
}
