using UnityEngine;
using System.Collections;

public class TextBehaviour : MonoBehaviour {

    public string InputText { get; set; } 

	// Use this for initialization
	void Start () {
        RunOnRuby();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void RunOnRuby() {
        ConectToIronRuby.Launcher.LaunchIronRuby();
    }
}
