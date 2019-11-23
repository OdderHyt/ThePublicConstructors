using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGController : MonoBehaviour
{
    public float speed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(0,transform.position.y+speed);
        //if(transform.position.y>15)
            //transform.position = new Vector2(0, -15);
    }
}
