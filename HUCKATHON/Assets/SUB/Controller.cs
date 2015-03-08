using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {
	public Transform cTransform {
		get {
			if (!_cTransform)
				_cTransform = transform;
			return _cTransform;
		}
	}
	Transform _cTransform;
	public Rigidbody cRigidbody {
		get {
			if (!_cRigidbody)
				_cRigidbody = GetComponent<Rigidbody>();
			return _cRigidbody;
		}
	}
	Rigidbody _cRigidbody;

	Vector3 inputHor;
	Vector3 inputVer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update() {
		GetInput();
	}

	void FixedUpdate() {
		cRigidbody.AddForce(inputVer*15);

	}

	private void GetInput() {
		inputVer = new Vector3(0, 0, Input.GetAxisRaw("Vertical"));
		inputHor = new Vector3(0,Input.GetAxisRaw("Horizontal"),0);
	}
}
