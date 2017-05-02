using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class SettingsJSON 
{
//	public float allowedSessionTime;

	public float birdColliderSize;
	public float birdMaxHeadSize;
	public float birdMinHeadSize;
	public float birdNearDistance;
	public float birdFarDistance;

	public Vector2 Holobound1;
	public Vector2 Holobound2;
	public Vector2 Holobound3;
	public Vector2 Holobound4;

	public float bugColliderSize;
	public float bugDeathTime;
	public float bugMaxSize;
	public float bugMinSize;

	public bool emitFromHead;
	public bool emitFromFarthestQuadrant;
	public bool emitFromInitialGrid;
	public float experienceLengthSeconds;
	public float deathLengthSeconds;

	public float faderLevelsMax;
	public float faderLevelsMin;


	public float graceTime;
	public string globalHeadsetUIMessage;

	public float maxSpeedToSitStill;

	public string slowMessage;
	public Vector4 shadowColor;
	public float shadowSize;

	public string timeToDieMessage;
	public float timeLeftToDie;

	public float warnForSeconds;
	public float wingLineRendererLength;

}

