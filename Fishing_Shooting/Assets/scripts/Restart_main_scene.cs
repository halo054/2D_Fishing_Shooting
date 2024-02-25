using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Restart_main_scene : MonoBehaviour
{
    public GameObject Restart_Button;

    void Start() { Restart_Button.SetActive(false); }
    public void choosing_level(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
