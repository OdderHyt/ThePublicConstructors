﻿using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public static GameManager instance { private set; get; }
	public Vector3 playerCenter { private set; get; }
	public Vector3 playerVelocity { private set; get; }
	public float BackgroundObjectSpeed { get => backgroundObjectSpeed; }
	private bool roundEnd;

	private PlayerController winner;

	[SerializeField] public List<GameObject> levelObjects = new List<GameObject>();
	[SerializeField] public GameObject parachute, parachuteInPlay, player, bottom;

	private float spawnTime = 0;

	[SerializeField] private TextMeshProUGUI countDownTimer;

	[Header("Background objects settings")]
	[SerializeField] [Range(0f, 25f)] private float spawnTimeMax = 5f, backgroundObjectSpeed = 1;
	[SerializeField] [Range(100f, 1000f)] private float minDistance, maxDistance;
	[SerializeField] private float roundTime = 60;
	[SerializeField] private List<GameObject> backgroundObjects;

	[Header("Player magnet settings")]
	[SerializeField] [Range(0f, 25f)] private float maxPlayerToPlayerDistance = 5f;


	private void Awake() {
		if(instance == null) {
			instance = this;
		} else {
			Destroy(this);
		}

		PlayerController.playerID = 1;
		for(int i = 0; i < Input.GetJoystickNames().Length; i++) {
			GameObject newPlayer = Instantiate(player, new Vector3(Random.Range(-90, 90), 50, 0), Quaternion.identity);
			newPlayer.name = "player " + PlayerController.playerID;
			levelObjects.Add(newPlayer);
		}

		parachuteInPlay = Instantiate(parachute, Vector3.zero, Quaternion.identity);
		levelObjects.Add(parachuteInPlay);
	}

	private void FixedUpdate() {
		playerCenter = new Vector3(
		levelObjects.Average(x => x.transform.position.x),
		levelObjects.Average(x => x.transform.position.y),
		levelObjects.Average(x => x.transform.position.z));

		playerVelocity = new Vector3(
		levelObjects.Average(x => x.GetComponent<Rigidbody>().velocity.x),
		levelObjects.Average(x => x.GetComponent<Rigidbody>().velocity.y),
		levelObjects.Average(x => x.GetComponent<Rigidbody>().velocity.z));

		foreach(var player in levelObjects) { //add force to players to keep them together and add for to players towrads the center of the screen if playersCenter magnatuide is below minDistance
			player.GetComponent<Rigidbody>().AddForce(-playerVelocity - playerCenter * player.GetComponent<Rigidbody>().mass);
			if(Vector3.Distance(playerCenter, player.transform.position) > maxPlayerToPlayerDistance) {
				player.GetComponent<Rigidbody>().AddForce((playerCenter - player.transform.position) * player.GetComponent<Rigidbody>().mass);
			}

		}

		spawnTime -= Time.deltaTime;
		if(spawnTime <= 0) {
			spawnTime = Random.Range(0f, spawnTimeMax);

			float spreadZ = Random.Range(minDistance, maxDistance);
			float spreadX = spreadZ / Mathf.Tan(Mathf.Deg2Rad * (CameraController.instance.vFOV / 2));
			float spreadY = spreadZ / Mathf.Tan(Mathf.Deg2Rad * (CameraController.instance.hFOV / 2));


			Instantiate(backgroundObjects.ElementAt(Random.Range(0, backgroundObjects.Count)), new Vector3(Random.Range(-spreadX, spreadX), -spreadY, spreadZ), Quaternion.identity);
		}

		roundTime -= Time.deltaTime;

		if(roundEnd) {

			countDownTimer.SetText(winner.name + " won!\nnew game in: " + ((int)roundTime).ToString());
			if(roundTime < 0) {
				SceneManager.LoadScene(0);
			}
		} else {
			countDownTimer.SetText(((int)roundTime).ToString());
			if(roundTime <= 0 && ParachuteController.instance == null) { //levelObjects.FindIndex(p => p.GetComponent<PlayerController>().parachute.activeSelf == true) >= 0
				winner = levelObjects.Find(x => x.GetComponent<PlayerController>().parachute.activeSelf == true).GetComponent<PlayerController>();
				roundTime = 15;
				roundEnd = true;
				foreach(var item in levelObjects) {
					item.GetComponent<Rigidbody>().useGravity = true;
				}
				winner.rb.useGravity = false;
				winner.openParachute.SetActive(true);
				bottom.SetActive(false);
			}
		}
	}
	public bool dropParachute(GameObject pc, Rigidbody rb) {
		parachuteInPlay = Instantiate(parachute, pc.transform.position, pc.transform.rotation);
		parachuteInPlay.GetComponent<Rigidbody>().velocity = rb.velocity;
		parachuteInPlay.GetComponent<Rigidbody>().angularVelocity = rb.angularVelocity;
		levelObjects.Add(parachuteInPlay);
		return false;
	}
	public bool pickUpParachute() {
		levelObjects.Remove(parachuteInPlay);
		Destroy(parachuteInPlay);
		ParachuteController.instance = null;
		return true;
	}
}

