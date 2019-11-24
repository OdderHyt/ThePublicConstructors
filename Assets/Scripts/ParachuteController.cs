using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParachuteController : MonoBehaviour
{
	public static ParachuteController instance;
	private void Awake() {
		if (instance == null) {
			instance = this;
		} else {
			Destroy(gameObject);
		}
		StartCoroutine(wait());
	}

	IEnumerator wait() {
		yield return new WaitForSeconds(1);
		gameObject.GetComponent<CapsuleCollider>().enabled = true;
	}
}
