using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Image healthBar;
    public Vector3 respawnPosition = new Vector3(0, 4, 0); 

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Respawn(); 
        }
    }

    void UpdateHealthBar()
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    private void Respawn()
    {
        currentHealth = maxHealth;
        UpdateHealthBar(); 
        transform.position = respawnPosition; 
        Debug.Log("�÷��̾ �������Ǿ����ϴ�.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DamageObject"))
        {
            TakeDamage(10f);
        }
    }
}
