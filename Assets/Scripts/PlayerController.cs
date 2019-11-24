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

    public float launchSpeed;
    private string hAxis, vAxis, launch;

    private bool onCooldown = false;
    public Vector2 joystickVector, joystickVectorSquare;

    [SerializeField]
    private float cooldown, cooldownMax = 2f;


    // Start is called before the first frame update
    void Start() {
        hAxis = "Horizontal" + playerID;
        vAxis = "Vertical" + playerID;
        launch = "Launch" + playerID;
        cooldown = cooldownMax;
    }

    // Update is called once per frame
    void Update() {
        joystickVectorSquare = new Vector3(Input.GetAxisRaw(hAxis), Input.GetAxisRaw(vAxis));
        joystickVector = joystickVectorSquare * (1/Mathf.Cos()+Mathf.Sin());
        indicator.transform.localRotation = new Quaternion(joystickVector.x, joystickVector.y, 0.0f, 0.0f);
        indicator.transform.position = this.transform.position + new Vector3(joystickVector.x, joystickVector.y) * 3;
        
        //indicator.transform.position = new Vector2(Input.GetAxis(hAxis), Input.GetAxis(vAxis)).normalized;
        if (Input.GetButtonDown(launch) && !onCooldown) {
            rb.AddForce(new Vector2(joystickVector.x, joystickVector.y) * launchSpeed, ForceMode.Impulse);
            onCooldown = true;
        } else if (!Input.GetButton(launch)) {
            cooldown -= Time.deltaTime;
            if (cooldown <= 0F) {
                cooldown = cooldownMax;
                onCooldown = false;
            }
        }
        void OnCollisionEnter(Collision coll) {
            if (coll.collider.CompareTag("Player")) {
                //Explode();
            }
        }
        //void Explode() {
        //    GameObject firework = Instantiate(HitEffect, position, Quaternion.identity);
        //    firework.GetComponent<ParticleSystem>().Play();


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
