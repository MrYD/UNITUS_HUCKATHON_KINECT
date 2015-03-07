using UnityEngine;
using System.Collections;
using Assets;

public class ModelController : MonoBehaviour,IModelControl {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void rotateJoint(SfNuiControl.KinectV2JointType joint, Vector3 vector) {

		switch (joint) {
			case SfNuiControl.KinectV2JointType.SpineBase:
				
				break;
			case SfNuiControl.KinectV2JointType.SpineMid:
				break;
			case SfNuiControl.KinectV2JointType.Neck:
				break;
			case SfNuiControl.KinectV2JointType.Head:
				break;
			case SfNuiControl.KinectV2JointType.ShoulderLeft:
				break;
			case SfNuiControl.KinectV2JointType.ElbowLeft:
				break;
			case SfNuiControl.KinectV2JointType.WristLeft:
				break;
			case SfNuiControl.KinectV2JointType.HandLeft:
				break;
			case SfNuiControl.KinectV2JointType.ShoulderRight:
				break;
			case SfNuiControl.KinectV2JointType.ElbowRight:
				break;
			case SfNuiControl.KinectV2JointType.WristRight:
				break;
			case SfNuiControl.KinectV2JointType.HandRight:
				break;
			case SfNuiControl.KinectV2JointType.HipLeft:
				break;
			case SfNuiControl.KinectV2JointType.KneeLeft:
				break;
			case SfNuiControl.KinectV2JointType.AnkleLeft:
				break;
			case SfNuiControl.KinectV2JointType.FootLeft:
				break;
			case SfNuiControl.KinectV2JointType.HipRight:
				break;
			case SfNuiControl.KinectV2JointType.KneeRight:
				break;
			case SfNuiControl.KinectV2JointType.AnkleRight:
				break;
			case SfNuiControl.KinectV2JointType.FootRight:
				break;
			case SfNuiControl.KinectV2JointType.SpineShoulder:
				break;
			case SfNuiControl.KinectV2JointType.HandTipLeft:
				break;
			case SfNuiControl.KinectV2JointType.ThumbLeft:
				break;
			case SfNuiControl.KinectV2JointType.HandTipRight:
				break;
			case SfNuiControl.KinectV2JointType.ThumbRight:
				break;
			case SfNuiControl.KinectV2JointType.Count:
				break;
			default:
				break;
		}
	}
}