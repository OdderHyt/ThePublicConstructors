using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public int playerNumber = 1;
    public float horizontalSpeed = 10f,
                 upSpeed = 10f,
                 downSpeed = 10f,
                 punchSpeed = 1000f;
    private string hAxis,
            vAxis,
            attack,
            altAttack;
    

    // Start is called before the first frame update
    void Start()
    {
        if(playerNumber==1)
        {
            hAxis = "Horizontal1";
            vAxis = "Vertical1";
            attack = "Attack1";
            altAttack = "AltAttack1";
        } else if(playerNumber==2)
        {
            hAxis = "Horizontal2";
            vAxis = "Vertical2";
            attack = "Attack2";
            altAttack = "AltAttack2";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(attack))
        {
            rb.AddForce(new Vector2(Input.GetAxis(hAxis) * punchSpeed, Input.GetAxis(vAxis) * punchSpeed));
        }
        rb.AddForce(new Vector2(Input.GetAxis(hAxis) * horizontalSpeed * Time.deltaTime, 0));
        
    }
}
