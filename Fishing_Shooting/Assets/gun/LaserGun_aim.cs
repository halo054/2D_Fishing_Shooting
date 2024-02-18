using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun_aim : MonoBehaviour
{
    // 激光枪的Transform组件
    public Transform gunTransform;

    // 限制激光枪旋转的最小和最大角度
    public float minAngle = -20f;
    public float maxAngle = 20f;

    void Update()
    {
        // 获取鼠标在屏幕上的位置
        Vector3 mousePosition = Input.mousePosition;
        // 将屏幕上的位置转换为世界空间中的射线
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        // 计算从激光枪位置指向目标位置的向量
        Vector3 direction = targetPosition - gunTransform.position;
        // 计算旋转角度
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // 将旋转角度限制在指定范围内
        angle = Mathf.Clamp(angle, minAngle, maxAngle);
        // 应用旋转
        gunTransform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
