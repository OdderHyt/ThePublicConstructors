using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager instance { private set; get; }
	public Vector3 sceneCenter { private set; get; }
	[SerializeField] public List<GameObject> players;

	private float time = 0;

	[Header("Background objects settings")]
	[SerializeField] [Range(0f, 25f)] private float maxTime = 5f;
	[SerializeField] private List<GameObject> backgroundObjects;

	[Header("Player magnet settings")]
	[SerializeField] [Range(0f, 25f)] private float minDistance = 5f, force = 1;
	private void Awake() {
		if(instance == null) {
			instance = this;
		} else {
			Destroy(this);
		}
	}

	private void FixedUpdate() {
		sceneCenter = new Vector3(
		players.Average(x => x.transform.position.x),
		players.Average(x => x.transform.position.y),
		players.Average(x => x.transform.position.z));


		magnetPlayer();
	}

	private void OnDrawGizmos() {
		Gizmos.DrawSphere(sceneCenter,1);
	}

	void magnetPlayer() {
		foreach(var player in players) {
			if(Vector3.Distance(player.transform.position, sceneCenter) > minDistance) {
				player.GetComponent<Rigidbody>().AddForce((sceneCenter - player.transform.position) * player.GetComponent<Rigidbody>().mass * Time.deltaTime * force);
			}

		}
	}
}

