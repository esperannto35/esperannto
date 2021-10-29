using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFalling : MonoBehaviour
{
    [SerializeField] private Color TilesColor;
    Renderer TilesRenderer;
    public GameObject Player;
    bool startBool;


    private void Update()
    {
        startBool=Player.GetComponent<PlayerController>().startBool;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(startBool == true)
        {
            if (collision.gameObject.tag == "Tiles")
            {

                collision.gameObject.AddComponent<Rigidbody>();
                collision.gameObject.GetComponent<BoxCollider>().enabled = false;


                TilesRenderer = collision.gameObject.GetComponent<Renderer>();
                TilesRenderer.material.color = TilesColor;

                collision.gameObject.AddComponent<CubeDestroy>();
            }
        }
        
    }
    
}
