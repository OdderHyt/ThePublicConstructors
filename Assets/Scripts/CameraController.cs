using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour {
	new Camera camera;

	[SerializeField] [Range(0f, 1f)] private float speedz = 0.5f, speedxy = 0.5f;
	[SerializeField] [Range(1f, 10f)] private float edge = 4f;
	[SerializeField] [Range(0f, 10f)] private float minimum = 10f;

	private void Awake() {
		camera = gameObject.GetComponent<Camera>();
		transform.position = Vector3.zero;
	}

	private void Update() {
		transform.position = new Vector3(
		Mathf.Lerp(transform.position.x, GameManager.instance.sceneCenter.x, speedxy),
		Mathf.Lerp(transform.position.y, GameManager.instance.sceneCenter.y, speedxy),
		Mathf.Lerp(transform.position.z, (Mathf.Tan(camera.fieldOfView / 2) * Mathf.Max(Mathf.Abs(transform.position.x - GameManager.instance.players.First().transform.position.x), Mathf.Abs(transform.position.y - GameManager.instance.players.First().transform.position.y), minimum) / edge), speedz));

	}
}
