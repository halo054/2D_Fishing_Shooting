using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LaserGunShoot : MonoBehaviour
{
    public Transform gunTransform;
    public GameObject laserPrefab;
    public float laserSpeed = 10f;
    public TextMeshProUGUI textComponent_bullet;
    public bool isshoot = false;
    private bool canShoot = true;

    void Update()
    {
        // 更新弹药数量显示
        UpdateBulletDisplay();

        // 检测鼠标点击并且可以开枪
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            // 发射激光
            FireLaser();

            // 设置开枪状态为不可开枪
            canShoot = false;

            // 开启协程，等待两秒后重新设置为可开枪状态
            StartCoroutine(ResetCanShoot());
        }
    }

    void UpdateBulletDisplay()
    {
        // 根据开枪状态更新文本显示
        textComponent_bullet.text = canShoot ? "Bullet: 1/1" : "Bullet: 0/1";
    }

    IEnumerator ResetCanShoot()
    {
        // 等待两秒
        yield return new WaitForSeconds(2f);
        // 将 isshoot 设置为 false
        isshoot = false;

        // 设置开枪状态为可开枪
        canShoot = true;
    }

    void FireLaser()
    {
        // 实例化激光预制件
        GameObject laser = Instantiate(laserPrefab, gunTransform.position, gunTransform.rotation);

        // 获取激光的刚体组件
        Rigidbody2D rb = laser.GetComponent<Rigidbody2D>();

        // 设置激光的速度
        rb.velocity = gunTransform.right * laserSpeed;
        
        isshoot = true;

        // 销毁激光预制件，防止占用过多内存
        Destroy(laser, 2f); // 2秒后销毁，可以根据实际需要调整时间
    }
}