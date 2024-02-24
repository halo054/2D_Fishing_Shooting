using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    public delegate void HealthChangedDelegate(float healthPercent);
    public event HealthChangedDelegate OnHealthChanged;

    public int maxHealth = 3;
    public int currentHealth;
    private bool isInvincible = false;
    public float invincibilityDuration = 0.5f;
    public string damageSourceTag = "laser";

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(damageSourceTag) && !isInvincible)
        {
            Debug.Log("hit");
            TakeDamage(1);
            StartCoroutine(InvincibilityFrame());
        }
    }

    private void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        
        UpdateHealthBar();
    }

    private IEnumerator InvincibilityFrame()
    {
        isInvincible = true;

        yield return new WaitForSeconds(invincibilityDuration);

        isInvincible = false;
    }

    private void UpdateHealthBar()
    {
        float healthPercent = (float)currentHealth / maxHealth;
        if (OnHealthChanged != null)
            OnHealthChanged(healthPercent); // 触发委托，传递健康值的百分比
        
    }
}