using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fish_moving_WASD_uprightdownleft : MonoBehaviour
{
    private float _speed = 50f;
    public GameObject Fish;
    public bool _fish_on_flag;
    public bool _set_hook_flag;
    private float _fish_check_time = 0.0f;
    private float _last_fish_check_time = 0.0f;
    private int[] _key_to_press_array;
    private int _current_key;
    private int _array_index = -1;
    public GameObject[] fishObjects; // Array to hold the individual fish squares
    public Sprite[] fishSpritesArray1; // Array to hold the first set of fish sprites
    public Sprite[] fishSpritesArray2; // Array to hold the second set of fish sprites
    private SpriteRenderer[] _fishRenderers; // Array to hold the sprite renderers of the fish
    public float spriteScale = 0.05f; // Variable to adjust sprite size
    public GameObject restart_button;

    // Start is called before the first frame update
    void Start()
    {
        _fish_on_flag = false;
        _set_hook_flag = false;
        _fish_check_time = 2 + Time.time + Random.Range(2f, 3f);
        _last_fish_check_time = 0f;
        _fishRenderers = new SpriteRenderer[6]; // Initialize array to hold sprite renderers
        for (int i = 0; i < 6; i++)
        {
            _fishRenderers[i] = fishObjects[i].GetComponent<SpriteRenderer>();
        }
    }

    void OnJointBreak2D()
    {
        restart_button.SetActive(true);
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

            if (Time.time > _fish_check_time && _set_hook_flag == false){

                _last_fish_check_time = _fish_check_time;
                _fish_check_time = _fish_check_time + Random.Range(2f, 3f);
                if (Random.Range(-1f, 1f) >= 0){
                    //gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * _speed * 5);
                    Fish.GetComponent<Rigidbody2D>().AddForce((-Vector3.up + Vector3.right) * _speed * 5);
                    _fish_on_flag = true;
                }
            }

            if (_fish_on_flag == true){
                if(Input.GetKeyDown(KeyCode.Space) && Time.time <= _last_fish_check_time + 1)
                {
                _set_hook_flag = true;
                Debug.Log("1");
            }

                //if not set hook, let fish escape
                else if(Time.time > _last_fish_check_time + 1)
                {
                _fish_on_flag = false;

                }
        }
            



        //let fish constantly moving, and sprint occasionally
        if (_set_hook_flag == true)
        {
            Fish.GetComponent<Rigidbody2D>().AddForce((-Vector3.up + Vector3.right) * _speed * 0.002f);

            if (Time.time > _fish_check_time)
            {
                _fish_check_time = _fish_check_time + Random.Range(0.5f, 3f);
                Fish.GetComponent<Rigidbody2D>().AddForce((-Vector3.up + Vector3.right) * _speed * 3);
                _speed++;
                Debug.Log(_speed);

            }
        }

            if (_set_hook_flag == true && Time.time > _fish_check_time)
            {
                _fish_check_time = _fish_check_time + Random.Range(0.5f, 3f);
                Fish.GetComponent<Rigidbody2D>().AddForce((-Vector3.up + Vector3.right) * _speed * 3);
                _speed++;
                //Debug.Log(_speed);
            }

            //to delay the escape of fish
            if (Input.GetKeyDown(KeyCode.Space) && _set_hook_flag == true)
            {
                _speed -= 0.08f;
            }

            //generate keys in _key_to_press_array
            if (_set_hook_flag == true && _array_index == -1)
            {
                int i;
                int a;
                _key_to_press_array = new int[6];
                // 0 represent left; 1 for up; 2 for right; 3 for down
                for (i = 0; i <= 5; i++)
                {
                    a = Random.Range(0, 4);
                    _key_to_press_array[i] = a;
                    UpdateFishSprites(fishSpritesArray1); // Initialize fish sprites with the first array
                }


                //ToDo: Translate 0,1,2,3 into arrows in UI

                Debug.Log(_key_to_press_array[0] + " " + _key_to_press_array[1] + " " + _key_to_press_array[2] + " "
                          + _key_to_press_array[3] + " " + _key_to_press_array[4] + " " + _key_to_press_array[5] + " ");
                _array_index = 0;
            }


            //when the right arrow is pressed, point to the next key(when not reach to end) or decrease speed and reset the arrow list
            if (_set_hook_flag == true && _array_index >= 0)
            {

                _current_key = _key_to_press_array[_array_index];
                if (Input.GetKeyDown(KeyCode.A) && _current_key == 0 ||
                    Input.GetKeyDown(KeyCode.W) && _current_key == 1 ||
                    Input.GetKeyDown(KeyCode.D) && _current_key == 2 ||
                    Input.GetKeyDown(KeyCode.S) && _current_key == 3 ||
                    Input.GetKeyDown(KeyCode.LeftArrow) && _current_key == 0 ||
                    Input.GetKeyDown(KeyCode.UpArrow) && _current_key == 1 ||
                    Input.GetKeyDown(KeyCode.RightArrow) && _current_key == 2 ||
                    Input.GetKeyDown(KeyCode.DownArrow) && _current_key == 3)
                {
                    UpdateSpriteArray();
                    if (_array_index == 5)
                    {
                        if (_speed <= 10)
                        {
                            Fish.GetComponent<Rigidbody2D>().AddForce((Vector3.up) * 1000);
                            _array_index = -1;
                            Debug.Log(_speed);
                        }



                        else
                        {
                            _array_index = -1;
                            _speed -= 15;
                            //_speed -= 10;
                            Debug.Log(_speed);
                        }

                    }
                    else
                    {
                        _array_index++;
                        _current_key = _key_to_press_array[_array_index];
                        Debug.Log("accessed");
                    }
                }
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
}