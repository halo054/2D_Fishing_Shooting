using UnityEngine;
using UnityEngine.SceneManagement;

public class change_background_music : MonoBehaviour
{
    public static change_background_music instance;

    public AudioClip initialMusic;
    public AudioClip secondMusic;

    private AudioSource audioSource;
    private bool _set_hook_flag;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = initialMusic;
            audioSource.loop = true;
            audioSource.Play();
           
        }
        else
        {
            Destroy(gameObject);
        }
        audioSource.clip = initialMusic;
        if (audioSource.isPlaying == false)
        {
            audioSource.Play();
        }
        
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name != "start scene" && SceneManager.GetActiveScene().name != "choose_level")
        {
            // 获取另一个物体上的 FishController 脚本，并获取其中的 _set_hook_flag 变量
            fish_moving_WASD fishController = FindObjectOfType<fish_moving_WASD >();
            if (fishController != null)
            {
                _set_hook_flag = fishController._set_hook_flag;
                // 每帧检查 _set_hook_flag 的值，根据其值选择要播放的音乐

            }

            if (_set_hook_flag)
            {
                if (audioSource.clip != secondMusic)
                {
                    audioSource.clip = secondMusic;
                    audioSource.Play();
                }
            }
            else
            {
                if (audioSource.clip != initialMusic)
                {
                    audioSource.clip = initialMusic;
                    audioSource.Play();
                }
            }
        }
        else
        {
            audioSource.clip = initialMusic;
            if (audioSource.isPlaying == false)
            {
                audioSource.Play();
            }
        }
    }
}