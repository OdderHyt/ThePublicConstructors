using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private string hAxis, vAxis, launch;
	private bool onCooldown = false;
	private Vector2 joystickVector, joystickVectorSquare;
	private float launchSpeed = 33;

	[SerializeField] public Rigidbody rb;
	public static int playerID = 1;
	[SerializeField] public Material[] mats;
	[SerializeField] public GameObject indicator, parachute, IDLE, openParachute;
	[SerializeField] private float cooldown, cooldownMax = 2f;

	void Start() {
		hAxis = "Horizontal" + playerID;
		vAxis = "Vertical" + playerID;
		launch = "Launch" + playerID;
		IDLE.GetComponent<MeshRenderer>().material = mats[playerID-1];
		indicator.GetComponent<MeshRenderer>().material = mats[playerID - 1];
		cooldown = cooldownMax;
		playerID++;
	}

	private void Update() {
		joystickVectorSquare = new Vector3(Input.GetAxisRaw(hAxis), Input.GetAxisRaw(vAxis));
		joystickVector = joystickVectorSquare;
		indicator.transform.localRotation = new Quaternion(joystickVector.x, joystickVector.y, 0.0f, 0.0f);
		indicator.transform.position = this.transform.position + new Vector3(joystickVectorSquare.x, joystickVectorSquare.y).normalized * 3;
	}

	void FixedUpdate() {
		if(Input.GetButtonDown(launch) && !onCooldown) {
			rb.AddForce(new Vector2(joystickVector.x, joystickVector.y) * launchSpeed * (parachute.activeSelf ? 0.75f : 1f), ForceMode.Impulse);
			onCooldown = true;
		} else if(!Input.GetButton(launch)) {
			cooldown -= Time.deltaTime;
			if(cooldown <= 0F) {
				cooldown = cooldownMax;
				onCooldown = false;
			}
		}
	}

	void OnCollisionEnter(Collision coll) {
		if(coll.collider.gameObject.CompareTag("Parachute")) {
			parachute.SetActive(GameManager.instance.pickUpParachute());
		} else if (coll.collider.gameObject.CompareTag("Player") && parachute.activeSelf) {
			parachute.SetActive(GameManager.instance.dropParachute(parachute, rb));
		}
	}
}
