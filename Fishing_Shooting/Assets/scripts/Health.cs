using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    public delegate void HealthChangedDelegate(float healthPercent);
    public event HealthChangedDelegate OnHealthChanged;
    private HealthManager scene_manager;

    public int maxHealth = 3;
    public int currentHealth;
    private bool isInvincible = false;
    public float invincibilityDuration = 0.5f;
    public string damageSourceTag = "laser";
    public GameObject manager;

    public string catchTag = "catch detector";
    // 触发器Collider2D
        public Collider2D triggerCollider;
    
        // 普通Collider2D
        public Collider2D normalCollider;

    private void Start()
    {
        // 初始化时，关闭Collider2D
        normalCollider.enabled = false;
        
        currentHealth = maxHealth;
        UpdateHealthBar();
        manager = GameObject.FindGameObjectWithTag(catchTag);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        normalCollider.enabled = false;
        if (other.CompareTag(damageSourceTag) && !isInvincible)
        {
            Debug.Log("hit");
            TakeDamage(1);
            StartCoroutine(InvincibilityFrame());
        }
        if (other.CompareTag(catchTag) && !isInvincible)
        {
            scene_manager = manager.GetComponent<HealthManager>(); // 更新 fishHealth 引用
            currentHealth = 0;
            scene_manager.Win = true;
        }
        if (other.CompareTag(damageSourceTag) == false)
        {
            normalCollider.enabled = true;
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