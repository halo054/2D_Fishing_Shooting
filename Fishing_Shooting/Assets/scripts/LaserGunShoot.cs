using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGunShoot : MonoBehaviour
{
    // 激光枪的Transform组件
    public Transform gunTransform;

    // 激光预制件
    public GameObject laserPrefab;

    // 发射激光的速度
    public float laserSpeed = 10f;

    void Update()
    {
        // 检测鼠标点击
        if (Input.GetMouseButtonDown(0))
        {
            // 检查是否能发射激光
            if (CanFireLaser())
            {
                // 发射激光
                FireLaser();
            }
        }
    }

    void FireLaser()
    {
        // 实例化激光预制件
        GameObject laser = Instantiate(laserPrefab, gunTransform.position, gunTransform.rotation);

        // 获取激光的刚体组件
        Rigidbody2D rb = laser.GetComponent<Rigidbody2D>();

        // 设置激光的速度
        rb.velocity = gunTransform.right * laserSpeed;

        // 销毁激光预制件，防止占用过多内存
        Destroy(laser, 2f); // 2秒后销毁，可以根据实际需要调整时间
    }

    bool CanFireLaser()
    {
        // 获取场景中带有 "laser" 标签的物体数量
        GameObject[] lasers = GameObject.FindGameObjectsWithTag("laser");
        int activeLasersCount = 0;

        // 统计激活的激光数量
        foreach (GameObject laser in lasers)
        {
            if (laser.activeSelf)
            {
                activeLasersCount++;
            }
        }

        // 如果激活的激光数量小于2，则允许发射新的激光
        return activeLasersCount < 2;
    }
}