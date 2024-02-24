using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class start_scene_manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ExitGameOnClick()
    {
        // 退出游戏
        Application.Quit();
    }
    public void choosing_level(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
