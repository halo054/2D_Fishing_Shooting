using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class choose_level_scene_manager : MonoBehaviour
{
    public void choosing_level(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
