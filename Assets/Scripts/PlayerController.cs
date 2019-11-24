using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public Rigidbody rb;
    public Material mat;

    [SerializeField]
    public int playerID;

    [SerializeField]
    public GameObject indicator;

    public float horizontalSpeed = 10f,
                 upSpeed = 10f,
                 downSpeed = 10f,
                 punchSpeed = 100f,
                 punchSpeedMax = 300f;
    private float punchCharge = 0f;
    private string hAxis, vAxis, launch;


    // Start is called before the first frame update
    void Start() {
        hAxis = "Horizontal" + playerID;
        vAxis = "Vertical" + playerID;
        launch = "Launch" + playerID;
    }

    // Update is called once per frame
    void Update() {
        indicator.transform.localRotation = new Quaternion(Input.GetAxis(hAxis), Input.GetAxis(vAxis), 0.0f, 0.0f);
        indicator.transform.position = this.transform.position + new Vector3(Input.GetAxis(hAxis), Input.GetAxis(vAxis)).normalized*3;
        //indicator.transform.position = new Vector2(Input.GetAxis(hAxis), Input.GetAxis(vAxis)).normalized;
        if (Input.GetButton(launch)) {
            Debug.Log(hAxis);
            Debug.Log(vAxis);
            rb.AddForce(new Vector2(Input.GetAxis(hAxis), Input.GetAxis(vAxis))*100);
        }
        //if (Input.GetButton(launch)) {
        //    if (punchCharge <= punchSpeedMax) {
        //        punchCharge += punchSpeed * Time.deltaTime;
        //        mat.SetColor("_Color", new Color((punchCharge * 255.0f / punchSpeedMax), 0, (255.0f - punchCharge * 255.0f / punchSpeedMax)));
        //        Debug.Log(punchCharge * 255.0f / punchSpeedMax);
        //    }
        //    /*else
        //    {
        //        punchCharge
        //    }*/
        //} else if (Input.GetButtonUp(launch)) {
        //    rb.AddForce(new Vector2(Mathf.Clamp(Input.GetAxis(hAxis) * 100, -1f, 1f) * punchCharge, Mathf.Clamp(Input.GetAxis(vAxis) * 100, -1f, 1f) * punchCharge * ((Input.GetAxis(vAxis) > 0) ? upSpeed : downSpeed)));
        //    punchCharge = 0f;
        //    mat.SetColor("_Color", new Color(0, 0, 255));
        //} else {
        //    rb.AddForce(new Vector2(Input.GetAxis(hAxis) * horizontalSpeed * Time.deltaTime, 0));
        //}


    }
}
