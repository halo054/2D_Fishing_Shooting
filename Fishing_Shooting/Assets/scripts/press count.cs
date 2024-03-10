using UnityEngine;
using UnityEngine.UI;

public class presscount : MonoBehaviour
{
    public Sprite newSprite; // 要切换到的新Sprite
    private Sprite originalSprite; // 原始Sprite
    private Image imageComponent; // UI上的Image组件
    private GameObject fish; // 参考 fish 游戏对象

    void Start()
    {
        // 获取UI上的Image组件
        imageComponent = GetComponent<Image>();
        // 保存原始Sprite
        originalSprite = imageComponent.sprite;

        
    }

    void Update()
    {
        // 检测空格键是否按下
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 切换到新的Sprite
            imageComponent.sprite = newSprite;
        }

        // 检测空格键是否释放
        if (Input.GetKeyUp(KeyCode.Space))
        {
            // 恢复原始Sprite
            imageComponent.sprite = originalSprite;
        }
    }
}