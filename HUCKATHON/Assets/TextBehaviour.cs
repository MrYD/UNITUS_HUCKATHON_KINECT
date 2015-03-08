using System;
using UnityEngine;
using System.Collections;
using System.Windows.Forms;

public class TextBehaviour : MonoBehaviour
{
	public TextMesh oText;
	public Boolean shiftflag;

    public string InputText { get; set; } 

	// Use this for initialization
	void Start () {
        RunOnRuby();
		shiftflag = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TypeKeys(string s)
	{
		switch (s)
		{
			case "space":
				oText.text +=" ";
				return;
			case "enter":
				oText.text += "\r\n";
				return;
			case "back":
				oText.text = oText.text.Substring(0, oText.text.Length - 1);
				return;
			case "win":
				System.Diagnostics.Process.Start(System.Environment.CurrentDirectory+@"\Assets\win.exe");
				return;
			case "shift":
			shiftflag = true;
				return;
			default :
				if (shiftflag)
				{
					switch (s)
					{
						case "1":
							oText.text += "!";
							return;
						case "2":
							oText.text += "\"";
							return;
						case"3":
							oText.text += "#";
							return;
						case"4":
							oText.text += "$";
							return;
						case"5":
							oText.text += "%";
							return;
						case"6":
							oText.text += "&";
							return;
						case"7":
							oText.text += "'";
							return;
						case"8":
							oText.text += "(";
							return;
						case"9":
							oText.text += ")";
							return;
						case",":
							oText.text += "<";
							return;
						case".":
							oText.text += ">";
							return;
						default:
							oText.text += s.ToUpper();
							return;
					}
					//oText.text += s.ToUpper();
					shiftflag = false;
				}
				else
					oText.text += s;
				return;
		}
		
	}

    void RunOnRuby() {
        ConectToIronRuby.Launcher.LaunchIronRuby();
    }
}
