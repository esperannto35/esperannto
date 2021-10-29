using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroytheCubes", 2f);
    }

    // Update is called once per frame
    void DestroytheCubes()
    {
        Destroy(gameObject);
    }
}
