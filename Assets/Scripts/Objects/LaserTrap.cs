using UnityEngine;
using UnityEngine.UI;

public class LaserTrap : MonoBehaviour
{
    public Transform laserStart; 
    public Transform laserEnd;   
    public float laserWidth = 0.1f; 
    public float damage = 10f; 
    public float damageInterval = 1f;

    private float lastDamageTime; 

    private void Start()
    {
        lastDamageTime = Time.time; 
    }

    private void Update()
    {
        DetectPlayer();
    }

    private void DetectPlayer()
    {
        Vector3 direction = laserEnd.position - laserStart.position;
        RaycastHit hit;

        if (Physics.Raycast(laserStart.position, direction.normalized, out hit, direction.magnitude))
        {
            if (hit.collider.CompareTag("Player"))
            {
                if (Time.time >= lastDamageTime + damageInterval)
                {
                    PlayerHealth playerHealth = hit.collider.GetComponent<PlayerHealth>();
                    if (playerHealth != null)
                    {
                        playerHealth.TakeDamage(damage);
                        lastDamageTime = Time.time;
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 direction = laserEnd.position - laserStart.position;

        // 테스트 시각화용
        Gizmos.color = Color.red;
        Gizmos.DrawLine(laserStart.position, laserEnd.position);
        Gizmos.DrawWireCube((laserStart.position + laserEnd.position) / 2, new Vector3(laserWidth, laserWidth, direction.magnitude));
    }
}
