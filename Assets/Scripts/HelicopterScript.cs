using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterScript : MonoBehaviour
{
    public int speed;
    bool Go,PlayerMove;
    GameObject Player;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player = collision.gameObject;
            PlayerMove=true;
            Invoke("GoChange", 2f);
            collision.gameObject.GetComponent<PlayerController>().Speed = 0;
            collision.gameObject.GetComponent<Rigidbody>().isKinematic=true;
            collision.gameObject.transform.parent = transform;
        }
    }
    private void Update()
    {
        if (Go == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position+new Vector3(10,3,0), Time.deltaTime * speed);
        }
        if(PlayerMove)
        {
            Player.transform.position = Vector3.MoveTowards(Player.transform.position, transform.position+new Vector3(0,0.5f,0), Time.deltaTime * 10f);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, 340), Time.deltaTime * 8f);
        }
    }
    public void GoChange()
    {
        Go = true;
    }
}
