using UnityEngine;

public class LaunchPlatform : MonoBehaviour
{
    public float launchForce = 10f; // 발사 힘
    public Vector3 launchDirection = Vector3.up; // 발사 방향

    private bool playerOnPlatform = false; // 플레이어가 플랫폼 위에 있는지 여부

    private void Update()
    {
        if (playerOnPlatform && Input.GetKeyDown(KeyCode.E)) // E 키를 눌렀을 때 발사
        {
            LaunchPlayer();
        }
    }

    private void LaunchPlayer()
    {
        // 플랫폼 위의 플레이어 찾기
        Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(1f, 1f, 1f), Quaternion.identity);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                Rigidbody playerRigidbody = collider.GetComponent<Rigidbody>();
                if (playerRigidbody != null)
                {
                    playerRigidbody.AddForce(launchDirection.normalized * launchForce, ForceMode.Impulse);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnPlatform = true; // 플레이어가 플랫폼에 올라갔을 때
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnPlatform = false; // 플레이어가 플랫폼에서 나갔을 때
        }
    }
}
