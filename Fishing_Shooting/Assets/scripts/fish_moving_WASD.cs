using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class fish_moving_WASD : MonoBehaviour
{
    private float _speed = 50f;
    public GameObject Fish;
    public GameObject Anchor;
    public bool _fish_on_flag;
    public  bool _set_hook_flag;
    private float _fish_check_time = 0.0f;
    private float _current_fish_check_time = 0.0f;
    public GameObject Restart_Button;
    public TextMeshProUGUI textComponent; // 在Unity编辑器中将Canvas上的Text组件拖拽到这个变量中
    public TextMeshProUGUI countComponent;
    public TextMeshProUGUI word_hint;
    public FixedJoint2D joint;
    public HingeJoint2D Hinge;
    public float decrease_speed = 0.005f;
    public GameObject space_key;
    public float lose_condition = 15f;
    public float up_condition = 20f;
    public float down_condition = 0f;
    public float decrease_speed_condition = -25f;
    private float fishing_rod_damage = 0f;
    public float max_damage = 120f;
    public float fish_escaping_speed = 1f;
    public TextMeshProUGUI instructions;

    private float _press_space_timer;
    private int _press_space_counter;
    public bool _press_space_flag = false;
    public float fish_pulling_force = 5f; 

    // Start is called before the first frame update
    void Start()
    {
        Hinge = Anchor.GetComponent<HingeJoint2D>();
        _fish_on_flag = false;
        _fish_check_time = 2 + Time.time + Random.Range(2f, 3f);
        
        
    }



    // Update is called once per frame
    void Update()
    {
        if (Restart_Button.activeSelf == false)
        {
            if (_set_hook_flag == true)
            {
                instructions.text = "";
                if (_speed >= 60f)
                {
                    word_hint.text = "You feel like the fish is escaping! Control the fish!";
                }

                if (_speed < 60)
                {
                    if (Hinge.jointAngle <= decrease_speed_condition && _speed > 5f)
                    {
                        word_hint.text = "You feel like the fish is losing strength!";
                    }
                    else
                    {
                        word_hint.text = "You feel like the fish is trying to escape!";
                    }
                }

                if (_speed <= up_condition && _speed >= down_condition)
                {
                    word_hint.text = "It's the right time to pull!!! (Press F)";
                }

                if (Controlling_Rod.force.magnitude + 2f >= lose_condition)
                {
                    word_hint.text = "Too much force! Take it easy!";
                }
            }
            else
            {
                //if (_fish_on_flag == true && _set_hook_flag == false)
                //{
                    instructions.text = "Press SPACE to set the hook when fish is on.\nUse A and D to control the rod when hook is set.\nUse MOUSE to aim and shoot the fish.";
               // }

            }
        }
        else
        {
            word_hint.text = "The fish has escaped...QAQ";
        }
        // 如果fish_moving_WASD组件中的_press_space_flag为true，则显示space_key对象
        if (_press_space_flag == true)
        {
            space_key.SetActive(true);
        }
        else
        {
            space_key.SetActive(false);
        }

            if (Time.time > _fish_check_time && _fish_on_flag == false){
                _current_fish_check_time = _fish_check_time;
                _fish_check_time = _fish_check_time + Random.Range(0.5f, 3f);
                if (Random.Range(-1f, 1f) >= 0)
                {
                    Fish.GetComponent<Rigidbody2D>().AddForce((-Vector3.up + Vector3.right) * _speed * fish_pulling_force);
                    _fish_on_flag = true;
                }

            }

            if( Time.time <= _current_fish_check_time+0.5f && Input.GetKeyDown(KeyCode.Space)){
                _set_hook_flag = true;
            }
            else if(Time.time > _current_fish_check_time + 0.5f && _set_hook_flag == false){
                _fish_on_flag = false;
            }


        if (_set_hook_flag == true)
        {
            // 更新Text组件的文本内容为你的变量的值
            textComponent.text = "Speed: " + _speed.ToString("F2");
            Fish.GetComponent<Rigidbody2D>().AddForce((-Vector3.up + Vector3.right) * _speed * 0.05f);
            if ( Time.time > _fish_check_time)
            {
                _fish_check_time = _fish_check_time + Random.Range(0.5f, 3f);
                Fish.GetComponent<Rigidbody2D>().AddForce((-Vector3.up + Vector3.right) * _speed * 10);
                _speed += fish_escaping_speed;
                //Debug.Log(_speed);
            }

        }

        if (_press_space_flag == false)
        {
            countComponent.text = "";
        }

        

         // Press F to decide to pull fish
         if (Input.GetKeyDown(KeyCode.F) && _set_hook_flag == true && _speed <= up_condition && _speed >= down_condition)
         {
         _press_space_timer = Time.time + 2f;
         _press_space_counter = 0;
         _press_space_flag = true;
         
         }

        if ( _press_space_flag == true && Time.time < _press_space_timer)
        {
            countComponent.text = "Count: " + _press_space_counter.ToString("F0");
            word_hint.text = "Control your force!";
            if (Input.GetKeyDown(KeyCode.Space)) {
                _press_space_counter++;
                Debug.Log(_press_space_counter);
            }
        }
        // if fail to press space enough, add force back
        if (_press_space_flag == true && Time.time > _press_space_timer)
        {
            //add force to pull fish
            Fish.GetComponent<Rigidbody2D>().AddForce((Vector3.up) * 250 * _press_space_counter);
            _press_space_flag = false;
            _speed = 50;
        }


        //fish get controlled by rod

        
        if (_set_hook_flag == true && Hinge.jointAngle <= decrease_speed_condition && _speed  > 5f)
        {
            _speed -= decrease_speed;
            Debug.Log("Decrease speed");
        }

        //when rod experience big force, fish will escape
        if (_set_hook_flag == true && Controlling_Rod.force.magnitude >= lose_condition)
        {
            fishing_rod_damage += 1;
        }

        if (fishing_rod_damage > max_damage || _speed >= 70f)
        {
            Restart_Button.SetActive(true);
            joint = Fish.GetComponent<FixedJoint2D>();
            Destroy(joint);
            Debug.Log("Joint Break accessed");
        }

    }


    // Water floating force and resistance
    void OnTriggerEnter2D(Collider2D collision)
    {   
        if (collision.gameObject.CompareTag("water"))
        {
            //edit here to control floating force
            Fish.GetComponent<Rigidbody2D>().AddForce((Vector3.up) * 0.001f);

            //edit here to control resistance
            Fish.GetComponent<Rigidbody2D>().velocity = 0.95f * Fish.GetComponent<Rigidbody2D>().velocity;
            Debug.Log("1");
        }
    }

}