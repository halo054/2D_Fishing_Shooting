using UnityEngine;

public class Check_normal : MonoBehaviour
{
    void Update()
    {
        // 检测名为“health manager”的物体是否存在
        GameObject healthManager = GameObject.Find("health manager");

        if (healthManager != null)
        {
            // 如果存在，则获取其HealthManager脚本
            HealthManager healthManagerScript = healthManager.GetComponent<HealthManager>();

            if (healthManagerScript != null)
            {
                // 获取Win变量的值
                bool winValue = healthManagerScript.Win;

                // 如果Win的值为True，则隐藏当前物体
                if (winValue)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
