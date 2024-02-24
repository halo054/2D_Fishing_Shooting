using UnityEngine;
using UnityEngine.SceneManagement;

public class change_background_music : MonoBehaviour
{
    public static change_background_music instance;

    public AudioClip initialMusic;
    public AudioClip secondMusic;

    private AudioSource audioSource;
    private bool _fish_on_flag;

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
        if (SceneManager.GetActiveScene().name == "2.24 Yuan's version")
        {
            // 获取另一个物体上的 FishController 脚本，并获取其中的 _fish_on_flag 变量
            fish_moving_WASD_uprightdownleft  fishController = FindObjectOfType<fish_moving_WASD_uprightdownleft >();
            if (fishController != null)
            {
                _fish_on_flag = fishController._fish_on_flag;
                // 每帧检查 _fish_on_flag 的值，根据其值选择要播放的音乐
                
            }
           
            if (_fish_on_flag)
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