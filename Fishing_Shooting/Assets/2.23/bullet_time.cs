using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_time : MonoBehaviour
{
    public float maxHeight = 0.0f; // 设定的高度
    public float slowdownCooldown = 10f; // 触发减速的冷却时间

    private Rigidbody2D rb;
    private float originalFixedDeltaTime; // 原始固定时间步长
    private bool isSlowdown = false; // 是否处于减速状态
    private bool isCooldown = false; // 是否处于减速冷却状态
    private float slowdownTimer = 0f; // 减速计时器
    private float cooldownTimer = 0f; // 冷却计时器

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // 获取物体的 Rigidbody2D 组件
        originalFixedDeltaTime = Time.fixedDeltaTime; // 保存原始固定时间步长
    }

    void Update()
    {
        if (transform.position.y > maxHeight && !isSlowdown && !isCooldown) // 如果物体高度超过设定的高度且不在减速状态且不在冷却状态
        {
            Time.timeScale = 0.1f; // 减速时间流速为原来的1/10
            isSlowdown = true; // 进入减速状态
            slowdownTimer = slowdownCooldown; // 设置减速计时
        }
        else if (transform.position.y <= maxHeight && isSlowdown) // 如果物体高度低于或等于设定的高度且处于减速状态
        {
            Time.timeScale = 1f; // 恢复时间流速
            isSlowdown = false; // 结束减速状态
        }

        if (isSlowdown) // 如果处于减速状态
        {
            slowdownTimer -= Time.deltaTime; // 更新减速计时器
            if (slowdownTimer <= 0f) // 如果减速时间结束
            {
                isSlowdown = false; // 结束减速状态
                isCooldown = true; // 进入冷却状态
                cooldownTimer = slowdownCooldown; // 设置冷却计时
            }
        }

        if (isCooldown) // 如果处于冷却状态
        {
            cooldownTimer -= Time.deltaTime; // 更新冷却计时器
            if (cooldownTimer <= 0f) // 如果冷却时间结束
            {
                isCooldown = false; // 结束冷却状态
            }
        }
    }
}