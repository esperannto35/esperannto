using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngelScript : MonoBehaviour
{
    public bool TailHeli, LeftSide;
    float Y;
    public float RotationSpeed;


    // Update is called once per frame
    void Update()
    {
        if (!TailHeli)
        {
            if (LeftSide)
            {
                Y += RotationSpeed * Time.deltaTime;
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, Y, transform.eulerAngles.z);
            }
            else
            {
                Y -= RotationSpeed * Time.deltaTime;
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, Y, transform.eulerAngles.z);
            }
        }
        else
        {
            Y -= RotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(Y, transform.eulerAngles.y, transform.eulerAngles.z);
        }
    }
}
