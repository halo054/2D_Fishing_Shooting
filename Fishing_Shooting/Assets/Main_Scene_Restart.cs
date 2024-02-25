using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Scene_Restart : MonoBehaviour
{
    public GameObject button;
    void Start() { button.SetActive(false); }

    public void choosing_level(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
