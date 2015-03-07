﻿using System;
using UnityEngine;

public interface IModelControl {
	void rotateJoint(SfNuiControl.KinectV2JointType joint, Vector3 vector);
}

namespace SfNuiControl {
	[Serializable]
	public enum KinectV2JointType {
		SpineBase = 0,
		SpineMid = 1,
		Neck = 2,
		Head = 3,
		ShoulderLeft = 4,
		ElbowLeft = 5,
		WristLeft = 6,
		HandLeft = 7,
		ShoulderRight = 8,
		ElbowRight = 9,
		WristRight = 10,
		HandRight = 11,
		HipLeft = 12,
		KneeLeft = 13,
		AnkleLeft = 14,
		FootLeft = 15,
		HipRight = 16,
		KneeRight = 17,
		AnkleRight = 18,
		FootRight = 19,
		SpineShoulder = 20,
		HandTipLeft = 21,
		ThumbLeft = 22,
		HandTipRight = 23,
		ThumbRight = 24,
		Count = 25,
	}
}