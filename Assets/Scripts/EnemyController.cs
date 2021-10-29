using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Animator animator;
    Rigidbody rigi;
    GameObject player;
    bool startBool;

    void Start()
    {
        player = GameObject.FindWithTag("Player").gameObject;
        animator = gameObject.GetComponent<Animator>();
        rigi = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        startBool = player.GetComponent<PlayerController>().startBool;
        if (startBool == true)
        {
            animator.SetInteger("Enemy", 1);
            if (gameObject.transform.position.y < -4.1f)
            {
                animator.SetInteger("Enemy", 2);
                Invoke("DestroyEnemies", 3f);
            }
            else
            {
                rigi.velocity = new Vector3(0, rigi.velocity.y, 6f);
            }
        }
       
    }
    void DestroyEnemies()
    {
        Destroy(gameObject);
    }  
}
