using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager instance { private set; get; }
	public Vector3 playerCenter { private set; get; }
	public Vector3 playerVelocity { private set; get; }
	public float BackgroundObjectSpeed { get => backgroundObjectSpeed; }

	[SerializeField] public List<GameObject> players;

	private float time = 0;

	[Header("Background objects settings")]
	[SerializeField] [Range(0f, 25f)] private float maxTime = 5f, backgroundObjectSpeed = 1;
	[SerializeField] [Range(100f, 1000f)] private float minDistance, maxDistance;

	[SerializeField] private List<GameObject> backgroundObjects;

	[Header("Player magnet settings")]
	[SerializeField] [Range(0f, 25f)] private float maxPlayerToPlayerDistance = 5f;
	private void Awake() {
		if(instance == null) {
			instance = this;
		} else {
			Destroy(this);
		}
	}

	private void FixedUpdate() {
		playerCenter = new Vector3(
		players.Average(x => x.transform.position.x),
		players.Average(x => x.transform.position.y),
		players.Average(x => x.transform.position.z));

		playerVelocity = new Vector3(
		players.Average(x => x.GetComponent<Rigidbody>().velocity.x),
		players.Average(x => x.GetComponent<Rigidbody>().velocity.y),
		players.Average(x => x.GetComponent<Rigidbody>().velocity.z));

		foreach(var player in players) { //add force to players to keep them together and add for to players towrads the center of the screen if playersCenter magnatuide is below minDistance
			player.GetComponent<Rigidbody>().AddForce(-playerVelocity - playerCenter * player.GetComponent<Rigidbody>().mass);
			if(Vector3.Distance(playerCenter, player.transform.position) > maxPlayerToPlayerDistance) {
				player.GetComponent<Rigidbody>().AddForce((playerCenter - player.transform.position) * player.GetComponent<Rigidbody>().mass);
			}

		}

		time -= Time.deltaTime;
		if (time <= 0) {
			time = Random.Range(0f, maxTime);

			float spreadZ = Random.Range(minDistance, maxDistance);
			float spreadX = spreadZ / Mathf.Tan(Mathf.Deg2Rad * (CameraController.instance.vFOV / 2));
			float spreadY = spreadZ / Mathf.Tan(Mathf.Deg2Rad * (CameraController.instance.hFOV / 2));


			Instantiate(backgroundObjects.ElementAt(Random.Range(0, backgroundObjects.Count)), new Vector3(Random.Range(-spreadX, spreadX), -spreadY, spreadZ), Quaternion.identity);
		}
	}

	private void OnDrawGizmos() {
		Gizmos.DrawSphere(playerCenter, 1);
	}

}

