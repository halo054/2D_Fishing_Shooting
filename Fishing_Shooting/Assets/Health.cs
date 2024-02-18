using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    private bool isInvincible = false; // 用于标记物体是否处于无敌状态
    public float invincibilityDuration = 0.5f; // 无敌持续时间
    public string damageSourceTag = "laser"; // 碰撞对象的标签

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        // 在Update中进行碰撞检测
        CheckCollision();
    }

    private void CheckCollision()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag(damageSourceTag) && !isInvincible) // 检查碰撞的物体是否是指定的标签
            {
                Debug.Log("hit"); // 输出到控制台
                TakeDamage(1);
                StartCoroutine(InvincibilityFrame()); // 启动协程，开始无敌帧
            }
        }
    }

    private void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // 摧毁该物体
        Destroy(gameObject);
    }

    private IEnumerator InvincibilityFrame()
    {
        isInvincible = true; // 设置为无敌状态

        // 等待无敌时间结束
        yield return new WaitForSeconds(invincibilityDuration);

        isInvincible = false; // 取消无敌状态
    }
}
