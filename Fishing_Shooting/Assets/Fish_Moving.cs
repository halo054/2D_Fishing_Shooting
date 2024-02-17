using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish_Moving : MonoBehaviour
{
    private float _speed = 50f;
    public GameObject Fish;
    private bool _fish_on_flag;
    private float _fish_check_time = 0.0f;
    private int[] _key_to_press_array;
    private int _current_key;
    private int _array_index = -1;


    // Start is called before the first frame update
    void Start()
    {
        _fish_on_flag = false;
        _fish_check_time = Time.time + Random.Range(2f, 3f);
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time > _fish_check_time && _fish_on_flag == false)
        {
            _fish_check_time = _fish_check_time + Random.Range(2f, 3f);
            if (Random.Range(-1f, 1f) >= 0)
            {
                //gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * _speed * 5);
                Fish.GetComponent<Rigidbody2D>().AddForce((-Vector3.up + Vector3.right) * _speed * 5);
                _fish_on_flag = true;
                _fish_check_time = _fish_check_time + Random.Range(0.5f, 3f);
            }

        }

        if (_fish_on_flag == true && Time.time > _fish_check_time)
        {
            _fish_check_time = _fish_check_time + Random.Range(0.5f, 3f);
            Fish.GetComponent<Rigidbody2D>().AddForce((-Vector3.up + Vector3.right) * _speed * 5);
            _speed++;
            //Debug.Log(_speed);
        }

        //to delay the escape of fish
        if (Input.GetKeyDown(KeyCode.Space) && _fish_on_flag == true)
        {
            _speed -= 0.08f;
        }

        //generate keys in _key_to_press_array
        if (_fish_on_flag == true && _array_index == -1)
        {
            int i;
            int a;
            _key_to_press_array = new int[6];
            // 0 represent left; 1 for up; 2 for right; 3 for down
            for (i = 0; i <= 5; i++)
            {
                a = Random.Range(0, 4);
                _key_to_press_array[i] = a;
            }


            //ToDo: Translate 0,1,2,3 into arrows in UI

            Debug.Log(_key_to_press_array[0] + " " + _key_to_press_array[1] + " " + _key_to_press_array[2] + " "
                + _key_to_press_array[3] + " " + _key_to_press_array[4] + " " + _key_to_press_array[5] + " ");
            _array_index = 0;
        }


        //when the right arrow is pressed, point to the next key(when not reach to end) or decrease speed and reset the arrow list
        if (_fish_on_flag == true && _array_index >= 0)
        {
            
            _current_key = _key_to_press_array[_array_index];
            if (Input.GetKeyDown(KeyCode.LeftArrow) && _current_key == 0 || Input.GetKeyDown(KeyCode.UpArrow) && _current_key == 1 ||
                Input.GetKeyDown(KeyCode.RightArrow) && _current_key == 2 || Input.GetKeyDown(KeyCode.DownArrow) && _current_key == 3)
            {
                if (_array_index == 5)
                {
                    if (_speed <= 10)
                    {
                        Fish.GetComponent<Rigidbody2D>().AddForce((Vector3.up) *  1000);
                        _array_index = -1;
                        Debug.Log(_speed);
                    }



                    else {
                        _array_index = -1;
                        _speed -= 15;
                        //_speed -= 10;
                        Debug.Log(_speed); }
                    
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
}