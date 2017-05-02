using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{

	public ExpState expState;

	public int level { get; set; }

	public float dyingTime;
	public string actorName = "";
	private List<float> bugsEatenLog;
	private int bugsEaten = 0;
	public float sessionStartTime;
	public string zoneName;

	public PlayerData ()
	{
		level = 0;
		bugsEatenLog = new List<float> ();
		expState = ExpState.Living;
		sessionStartTime = Time.time;
		zoneName = "nowhere";
	}


	public float levelStartTime = 0.0f;

	public void resetPlayerData ()
	{ 
		level = 0;
		bugsEaten = 0;
		bugsEatenLog = new List<float> ();
		expState = ExpState.Living;
		sessionStartTime = Time.time;
	}

	public void addBugEaten ()
	{
		bugsEaten++;
		bugsEatenLog.Add (Time.time);
		if (expState == ExpState.Warn) { 
			expState = ExpState.Living;
		}
	}

	public int bugsEatenSince (float since)
	{
		float whichSince = since;
		//			float whichSince = Time.time - since;
		if (bugsEatenLog != null) {
			return bugsEatenLog.FindAll (b => b > whichSince).Count;
		} else {
			return 0;
		}
	}

	public int bugsAte ()
	{ 
		return bugsEaten;
	}
}