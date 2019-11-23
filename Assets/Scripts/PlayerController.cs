using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public int playerNumber = 1;
    public float horizontalSpeed = 10f,
                 upSpeed = 10f,
                 downSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerNumber == 1)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
                rb.AddForce(new Vector2(-horizontalSpeed, 0));
            if (Input.GetKey(KeyCode.RightArrow))
                rb.AddForce(new Vector2(horizontalSpeed, 0));
            if (Input.GetKey(KeyCode.UpArrow))
                rb.AddForce(new Vector2(0, upSpeed));
            if (Input.GetKey(KeyCode.DownArrow))
                rb.AddForce(new Vector2(0, -downSpeed));
        }
        else if (playerNumber == 2)
        {
            if (Input.GetKey(KeyCode.A))
                rb.AddForce(new Vector2(-horizontalSpeed, 0));
            if (Input.GetKey(KeyCode.D))
                rb.AddForce(new Vector2(horizontalSpeed, 0));
            if (Input.GetKey(KeyCode.W))
                rb.AddForce(new Vector2(0, upSpeed));
            if (Input.GetKey(KeyCode.S))
                rb.AddForce(new Vector2(0, -downSpeed));
        }
    }
}
