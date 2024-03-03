using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controlling_Rod : MonoBehaviour
{
    public GameObject Anchor;
    private HingeJoint2D hinge2D;
    private JointMotor2D updated_motor;
    private float motorspeed = 0f;

    void Start()
    {
        hinge2D = Anchor.GetComponent<HingeJoint2D>();
        updated_motor = hinge2D.motor;


    }
    // Update is called once per frame
    void Update()
    {
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
