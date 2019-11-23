using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public Material mat;
    public int playerNumber = 1;
    public float horizontalSpeed = 10f,
                 upSpeed = 10f,
                 downSpeed = 10f,
                 punchSpeed = 100f,
                 punchSpeedMax = 1000f;
    private float punchCharge = 0f;
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
        if (Input.GetButton(attack))
        {
            if (punchCharge <= punchSpeedMax)
            {
                punchCharge += punchSpeed * Time.deltaTime;
                mat.SetColor("_Color", new Color((punchCharge*255.0f/punchSpeedMax), 0, (255.0f - punchCharge * 255.0f / punchSpeedMax)));
                Debug.Log(punchCharge * 255.0f / punchSpeedMax);
            }
            /*else
            {
                punchCharge
            }*/
        } else if(Input.GetButtonUp(attack))
        {
            rb.AddForce(new Vector2(Mathf.Clamp(Input.GetAxis(hAxis)*100,-1f,1f) * punchCharge, Mathf.Clamp(Input.GetAxis(vAxis) * 100, -1f, 1f) * punchCharge * ((Input.GetAxis(vAxis)>0)?upSpeed:downSpeed)));
            punchCharge = 0f;
            mat.SetColor("_Color", new Color(0, 0, 255));
        } else {
            rb.AddForce(new Vector2(Input.GetAxis(hAxis) * horizontalSpeed * Time.deltaTime, 0));
        }
        
        
    }
}
