using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetController : MonoBehaviour
{

    // Start is called before the first frame update
    GameObject obj;
    public int cant = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
