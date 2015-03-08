using System;
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

	Vector3 input;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update() {
		GetInput();
	}

	void FixedUpdate() {
		cRigidbody.AddForce(input*14);
		cTransform.RotateAround(new Vector3(0,1,0),0);
	}

	private void GetInput() {
		input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
	}
}
