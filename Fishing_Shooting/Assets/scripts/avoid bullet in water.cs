using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class avoidbulletinwater : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 检查碰撞的物体是否带有 "laser" 标签
        if (other.CompareTag("laser"))
        {
            // 摧毁碰撞的物体
            Destroy(other.gameObject);
        }
    }
}
