using UnityEngine;

namespace MagicPigGames
{
    public class ProgressBar : MonoBehaviour
    {
        public RectTransform healthBar; // 血条的 RectTransform
        public RectTransform mask; // 蒙版的 RectTransform
        private float maxHealth; // 最大健康值
        public GameObject fish;
        private float currentHealthBarWidth; // 当前血条的宽度
        private float smoothness = 1.0f;


        // 订阅 Health 组件的 OnHealthChanged 事件，在 Start 方法中进行初始化
        private void Start()
        {


            if (fish != null)
            {
                Health health = fish.GetComponent<Health>();
                if (health != null)
                {
                    UpdateMaxHealth(health.currentHealth); // 初始设置最大健康值
                    UpdateHealthBar(health.currentHealth); // 初始设置血条

                    health.OnHealthChanged += UpdateHealthBar;
                    
                    currentHealthBarWidth = mask.localScale.x; // 获取蒙版的 X 轴缩放值
                }
            }
        }

        // 更新最大健康值
        private void UpdateMaxHealth(float currentHealth)
        {
            maxHealth = currentHealth;
        }

        // 当 Health 组件触发 OnHealthChanged 事件时被调用的方法，用于更新血条
        private void UpdateHealthBar(float currentHealth)
        {
            // 计算健康值百分比
            float healthPercent = currentHealth / maxHealth;

            // 计算目标血条长度（根据蒙版的 X 轴缩放值）
            float targetHealthBarWidth = currentHealthBarWidth * healthPercent;

            // 使用插值函数实现平滑过渡
            float lerpedWidth = Mathf.Lerp(healthBar.localScale.x, targetHealthBarWidth, Time.deltaTime * smoothness);

            // 更新血条的大小
            healthBar.localScale = new Vector3(lerpedWidth, healthBar.localScale.y, healthBar.localScale.z);
        }

        // 在 Update 方法中持续获取当前健康值，并更新血条
        private void Update()
        {
            if (fish != null)
            {
                Health health = fish.GetComponent<Health>();
                if (health != null)
                {
                    UpdateHealthBar(health.currentHealth);
                }
            }
        }
    }
}
