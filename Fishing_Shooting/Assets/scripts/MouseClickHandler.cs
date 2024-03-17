using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseClickHandler : MonoBehaviour
{
    public GameObject objectToHide1;
    public GameObject objectToHide2;
    public GameObject objectToHide3;
    public GameObject objectToHide4;
    public GameObject objectToHide5;
    public string sceneToLoad = "";

    private int clickCount = 1;
    public GameObject objectToShowHide;
    private bool isObjectVisible = true;

    private void Start()
    {
        InvokeRepeating("ToggleVisibility", 0.5f, 1.0f);
    }

    private void ToggleVisibility()
    {
        isObjectVisible = !isObjectVisible;
        objectToShowHide.SetActive(isObjectVisible);
    }

    void Update()
    {
        // 检测鼠标左键是否按下
        if (Input.GetMouseButtonDown(0))
        {
            // 隐藏物体
            if (clickCount == 1)
            {
                objectToHide1.SetActive(false);
                clickCount++;
                return;
            }
            if (clickCount == 2)
                        {
                            objectToHide2.SetActive(false);
                            clickCount++;
                            return;
                        }
            if (clickCount == 3)
                        {
                            objectToHide3.SetActive(false);
                            clickCount++;
                            return;
                        }
            if (clickCount == 4)
                        {
                            objectToHide4.SetActive(false);
                            clickCount++;
                            return;
                        }
            // 切换场景
            else
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }
}
