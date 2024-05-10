using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public GameObject[] wheels;
    public bool fourWheelDrive = true;

    public float maxMotorTorque;
    public float maxSteeringAngle;
    public float maxBrakeTorque;

    // Update is called once per frame
    void Update()
    {
        bool brakeEngaged = Input.GetKey(KeyCode.Space);
        for(int i = 0; i < 4; i++)
        {
            var col = wheels[i].GetComponent<WheelCollider>();
            if(i < 2)
            {
                col.steerAngle = maxSteeringAngle * Input.GetAxis("Horizontal");
            }
            if(fourWheelDrive || (!fourWheelDrive && i >= 2))
            {
                // Ternáris operátor - Ternary operator
                // X változó = Y állítás ? X értéke, hogyha Y == true : X értéke, hogyha Y == false
                col.motorTorque = brakeEngaged ? 0f : maxMotorTorque * Input.GetAxis("Vertical");
                col.brakeTorque = brakeEngaged ? maxBrakeTorque : 0f;
            }
            Vector3 p;
            Quaternion q;
            col.GetWorldPose(out p, out q);
            wheels[i].transform.rotation = q;
        }
    }
}
