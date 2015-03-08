using UnityEngine;
using System.Collections;

public class KeyInputer : MonoBehaviour
{
	public string s;
	public TextBehaviour tb;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.name == "unitychan_dynamic_locomotion")
		{
			tb.TypeKeys(this.name);
		}
	}
}
