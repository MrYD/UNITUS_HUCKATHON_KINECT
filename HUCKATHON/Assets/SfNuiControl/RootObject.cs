using UnityEngine;
using System.Collections;

public class RootObject : MonoBehaviour {

    public Vector3 angle = new Vector3(0, 20, 0);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate(this.angle * Time.deltaTime);
	}
}
