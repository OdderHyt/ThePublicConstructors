using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour {
	public static CameraController instance { private set; get; }

	public new Camera camera { private set; get; }
	public float hFOV { private set; get; }
	public float vFOV { private set; get; }


	[SerializeField] [Range(0f, 1f)] private float speedz = 1f, speedxy = 0.5f;
	[SerializeField] [Range(0f, 25f)] private float minimum = 10f;

	float a, b;

	private void Awake() {
		if(instance == null) {
			instance = this;
		} else {
			Destroy(this);
		}
		camera = gameObject.GetComponent<Camera>();
		transform.position = Vector3.zero;

		float vFOV = camera.fieldOfView;
		float radAngle = vFOV * Mathf.Deg2Rad;
		float radHFOV = 2 * Mathf.Atan(Mathf.Tan(radAngle / 2) * camera.aspect);
		float hFOV = Mathf.Rad2Deg * radHFOV;
	}

	private void Update() {

	}

	//(player to center)*tan(180-vFOV-30) 

	private void FixedUpdate() {

		a = (GameManager.instance.playerCenter.x - GameManager.instance.players.First().transform.position.x) * Mathf.Tan((180 - (CameraController.instance.hFOV / 2) - 90) * Mathf.Deg2Rad);
		b = (GameManager.instance.playerCenter.y - GameManager.instance.players.First().transform.position.y) * Mathf.Tan((180 - (CameraController.instance.vFOV / 2) - 90) * Mathf.Deg2Rad);

		transform.position = new Vector3(
		Mathf.Lerp(transform.position.x, GameManager.instance.playerCenter.x, speedxy),
		Mathf.Lerp(transform.position.y, GameManager.instance.playerCenter.y, speedxy),
		Mathf.Lerp(transform.position.z, -Mathf.Max(a, b, minimum), speedz));
	}

	private void OnDrawGizmos() {
		Gizmos.DrawSphere(new Vector3(GameManager.instance.playerCenter.x, GameManager.instance.playerCenter.y, -Mathf.Max(a, b, minimum)), 0.5f);
	}
}
