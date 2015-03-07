using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets;

public class ModelController : MonoBehaviour,IModelControl
{
	public Dictionary<SfNuiControl.KinectV2JointType,Transform> JointTypes;

	public void rotateJoint(SfNuiControl.KinectV2JointType joint, Vector3 vector) {
		JointTypes[joint].Rotate(vector);
	}
}