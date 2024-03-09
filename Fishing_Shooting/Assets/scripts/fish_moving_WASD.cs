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
    public bool _set_hook_flag;
    private float _fish_check_time = 0.0f;
    private float _current_fish_check_time = 0.0f;
    private int[] _key_to_press_array;
    private int _current_key;
    private int _array_index = -1;
    public GameObject[] fishObjects; // Array to hold the individual fish squares
    public Sprite[] fishSpritesArray1; // Array to hold the first set of fish sprites
    public Sprite[] fishSpritesArray2; // Array to hold the second set of fish sprites
    private SpriteRenderer[] _fishRenderers; // Array to hold the sprite renderers of the fish
    public float spriteScale = 0.05f; // Variable to adjust sprite size
    public GameObject Restart_Button;
    public TextMeshProUGUI textComponent; // 在Unity编辑器中将Canvas上的Text组件拖拽到这个变量中
    public FixedJoint2D joint;
    public HingeJoint2D Hinge;


    private float _press_space_timer;
    private int _press_space_counter;
    private bool _press_space_flag;

    // Start is called before the first frame update
    void Start()
    {
        Hinge = Anchor.GetComponent<HingeJoint2D>();
        _fish_on_flag = false;
        _fish_check_time = 2 + Time.time + Random.Range(2f, 3f);
        _fishRenderers = new SpriteRenderer[6]; // Initialize array to hold sprite renderers
        for (int i = 0; i < 6; i++)
        {
            _fishRenderers[i] = fishObjects[i].GetComponent<SpriteRenderer>();
        }
    }



    // Update is called once per frame
    void Update()
    {
            if (_key_to_press_array == null)
            {
                foreach (GameObject fishObject in fishObjects)
                {
                    fishObject.SetActive(false);
                }
            }
            else
            {
                foreach (GameObject fishObject in fishObjects)
                {
                    fishObject.SetActive(true);
                }
            }

            if (Time.time > _fish_check_time && _fish_on_flag == false){
                _current_fish_check_time = _fish_check_time;
                _fish_check_time = _fish_check_time + Random.Range(0.5f, 3f);
                if (Random.Range(-1f, 1f) >= 0)
                {
                    Fish.GetComponent<Rigidbody2D>().AddForce((-Vector3.up + Vector3.right) * _speed * 5);
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
                _speed++;
                //Debug.Log(_speed);
            }

        }

        

         // Press F to decide to pull fish
         if (Input.GetKeyDown(KeyCode.F) && _set_hook_flag == true && _speed <= 20f)
         {
         _press_space_timer = Time.time + 2f;
         _press_space_counter = 8;
         _press_space_flag = true;
         
         }

        if ( _press_space_flag == true && Time.time < _press_space_timer)
        {
            if (Input.GetKeyDown(KeyCode.Space)) {
                _press_space_counter--;
                Debug.Log(_press_space_counter);
            }
            if (_press_space_counter == 0)
            {
                //add force to pull fish
                Fish.GetComponent<Rigidbody2D>().AddForce((Vector3.up) * 1000);
                _press_space_flag = false;
                _speed = 70;
            }
        }
        // if fail to press space enough, add force back
        if (_press_space_flag == true && Time.time > _press_space_timer)
        {
            _speed = 70;
            _press_space_flag = false;
        }


        //fish get controlled by rod

        
        if (_set_hook_flag == true && Hinge.jointAngle <= -25f)
        {
            _speed -= 0.005f;
            Debug.Log("Decrease speed");
        }

        //when rod experience big force, fish will escape
        if (_set_hook_flag == true && Controlling_Rod.force.magnitude >= 15f)
        {
            Restart_Button.SetActive(true);
            joint = Fish.GetComponent<FixedJoint2D>();
            Destroy(joint);
            Debug.Log("Joint Break accessed");
        }

    }


    void UpdateFishSprites(Sprite[] spritesArray)
    {
        for (int i = 0; i < 6; i++)
        {
            _fishRenderers[i].sprite = spritesArray[_key_to_press_array[i]];
            _fishRenderers[i].transform.localScale = new Vector3(spriteScale, spriteScale, 1.0f); // Adjust sprite size
        }
    }

    // Function to update the sprite array
    void UpdateSpriteArray()
    {
        if (_array_index != -1)
        {
            // Update the sprite for the corresponding key
            int keyIndex = _current_key;
            _fishRenderers[_array_index].sprite = fishSpritesArray2[keyIndex]; // Update the sprite
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