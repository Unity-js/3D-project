using UnityEngine;

public class LaunchPlatform : MonoBehaviour
{
    public float launchForce = 10f; // �߻� ��
    public Vector3 launchDirection = Vector3.up; // �߻� ����

    private bool playerOnPlatform = false; // �÷��̾ �÷��� ���� �ִ��� ����

    private void Update()
    {
        if (playerOnPlatform && Input.GetKeyDown(KeyCode.E)) // E Ű�� ������ �� �߻�
        {
            LaunchPlayer();
        }
    }

    private void LaunchPlayer()
    {
        // �÷��� ���� �÷��̾� ã��
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
            playerOnPlatform = true; // �÷��̾ �÷����� �ö��� ��
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnPlatform = false; // �÷��̾ �÷������� ������ ��
        }
    }
}
