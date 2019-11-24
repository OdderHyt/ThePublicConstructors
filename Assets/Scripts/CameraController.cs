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
	[SerializeField] [Range(0f, 25f)] private float minZoom = 10f, minEdge = 25f;

	private void Awake() {
		if(instance == null) {
			instance = this;
		} else {
			Destroy(this);
		}
		camera = gameObject.GetComponent<Camera>();
		transform.position = Vector3.zero;

		vFOV = camera.fieldOfView;
		hFOV = Mathf.Rad2Deg * (2 * Mathf.Atan(Mathf.Tan((vFOV * Mathf.Deg2Rad) / 2) * camera.aspect));
	}

	private void FixedUpdate() {
		float x = GameManager.instance.players.OrderBy(i => i.transform.position.magnitude).First().transform.position.x;
		float y = GameManager.instance.players.OrderBy(i => i.transform.position.magnitude).First().transform.position.y;

		float a = ((Mathf.Abs(GameManager.instance.playerCenter.x - x) + minEdge) / Mathf.Tan(Mathf.Deg2Rad * (CameraController.instance.hFOV / 2)));
		float b = ((Mathf.Abs(GameManager.instance.playerCenter.y - y) + minEdge) / Mathf.Tan(Mathf.Deg2Rad * (CameraController.instance.vFOV / 2)));

		transform.position = new Vector3(
		Mathf.Lerp(transform.position.x, GameManager.instance.playerCenter.x, speedxy),
		Mathf.Lerp(transform.position.y, GameManager.instance.playerCenter.y, speedxy),
		Mathf.Lerp(transform.position.z, -Mathf.Max(a, b, minZoom), speedz));
	}
}
