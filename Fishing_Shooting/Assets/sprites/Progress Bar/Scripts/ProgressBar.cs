using UnityEngine;

namespace MagicPigGames
{
    public class ProgressBar : MonoBehaviour
    {
        public RectTransform healthBar; // 血条的 RectTransform
        public RectTransform mask; // 蒙版的 RectTransform

        // 当前血条的长度
        private float currentHealthBarWidth;

        // 订阅 Health 组件的 OnHealthChanged 事件，并在事件触发时更新血条显示
        private void Start()
        {
            Health health = GameObject.FindWithTag("Player").GetComponent<Health>();
            health.OnHealthChanged += UpdateHealthBar;

            // 获取初始血条长度
            currentHealthBarWidth = healthBar.sizeDelta.x;
        }

        // 当 Health 组件触发 OnHealthChanged 事件时被调用的方法
        private void UpdateHealthBar(float healthPercent)
        {
            // 计算当前血条长度
            float newHealthBarWidth = currentHealthBarWidth * healthPercent;

            // 更新血条的显示
            healthBar.sizeDelta = new Vector2(newHealthBarWidth, healthBar.sizeDelta.y);

            // 更新蒙版的大小，只显示血条内的部分，将边框外的部分隐藏起来
            mask.sizeDelta = new Vector2(newHealthBarWidth, mask.sizeDelta.y);
        }
    }
}