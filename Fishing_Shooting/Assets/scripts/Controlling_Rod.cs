using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controlling_Rod : MonoBehaviour
{
    public GameObject Anchor;
    private HingeJoint2D hinge2D;
    private JointMotor2D updated_motor;
    private float motorspeed = 0f;
    private Vector2 _force;

    //the counter used to control the interval between two joint force check.
    private float _next_check = 0.5f;


    void Start()
    {
        hinge2D = Anchor.GetComponent<HingeJoint2D>();
        updated_motor = hinge2D.motor;
        _next_check = 1f;

    }
    // Update is called once per frame
    void Update()
    {
        _force = hinge2D.GetReactionForce(0.5f);
        
        if (Time.time >= _next_check)
        {
            Debug.Log("Current force:" + _force.magnitude);
            _next_check += 1f;
        }


        if (Input.GetKeyDown(KeyCode.A))
        {
            //edit here to edit motor speed increasement
            motorspeed+=0.1f;
            //motor speed is negated to make the motor spinning backward
            updated_motor.motorSpeed = -motorspeed;
            Anchor.GetComponent<HingeJoint2D>().motor = updated_motor;
            Debug.Log("current force:" + updated_motor.motorSpeed);

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            //edit here to edit motor speed increasement
            motorspeed -= 0.1f;
            //motor speed is negated to make the motor spinning backward
            updated_motor.motorSpeed = -motorspeed;
            Anchor.GetComponent<HingeJoint2D>().motor = updated_motor;
            Debug.Log("current force:" + updated_motor.motorSpeed);

        }
    }
}