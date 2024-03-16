using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    private Health fishHealth; // 引用 Health 脚本
    public GameObject fish; // 参考 fish 游戏对象
    public bool Win;
    private static HealthManager instance;
    public string manager_name = "Health Manager easy";

    private void Start()
    {
        Win = false;

        // 查找所有 HealthManager 对象
        HealthManager[] healthManagers = FindObjectsOfType<HealthManager>();

        // 计算场景中 "Health Manager easy" 对象的数量
        int count = 0;
        foreach (HealthManager manager in healthManagers)
        {
            if (manager.gameObject.name == manager_name)
            {
                count++;
            }
        }

        // 如果 "Health Manager easy" 对象数量超过两个，销毁当前实例
        if (count > 2)
        {
            Destroy(gameObject);
            return;
        }

       
        instance = this;
        DontDestroyOnLoad(gameObject);
        

        SceneManager.sceneLoaded += OnSceneLoaded; // 添加场景加载完成后的回调
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 重新查找场景中的鱼对象并更新引用
        fish = GameObject.FindGameObjectWithTag("Player");

        if (fish != null)
        {
            fishHealth = fish.GetComponent<Health>(); // 更新 fishHealth 引用
        }
    }

    private void HandleHealthZero()
    {
        // 切换到 choose_level_scene
        SceneManager.LoadScene("choose_level");
    }

    // 在 Update 方法中检查健康值
    private void Update()
    {
        // 检查 fish 是否为 null
        if (fish == null)
        {
            return;
        }

        // 从 fish 游戏对象获取 Health 组件
        fishHealth = fish.GetComponent<Health>();

        // 检查 fish 的健康值是否小于等于 0
        if (fishHealth.currentHealth <= 0)
        {
            Win = true;
            // 如果是，触发健康值归零事件
            HandleHealthZero();
        }
    }
}