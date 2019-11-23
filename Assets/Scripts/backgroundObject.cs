using UnityEngine;

public class backgroundObject : MonoBehaviour
{
	private float speed = 1;
	private void Awake() {
		speed = GameManager.instance.BackgroundObjectSpeed;
	}
	private void Update() {
		if(transform.position.y > -(transform.position.z / Mathf.Sin(CameraController.instance.camera.fieldOfView / 2))) {
			Destroy(gameObject);
		}
	}
	void FixedUpdate()
    {
		transform.position = transform.position + Vector3.up * speed;
    }
}
