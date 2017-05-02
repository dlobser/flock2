using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{
	public class ON_InteractableEvents : MonoBehaviour {
		
		public delegate void EnterAction();
		public event EnterAction OnEnter;

		public delegate void ExitAction();
		public event ExitAction OnExit;

		public delegate void HoverAction(float t);
		public event HoverAction OnHover;

		public delegate void IdleAction();
		public event IdleAction OnIdle;

		public delegate void TriggerAction();
		public event TriggerAction OnTrigger;

		public bool debug;

		public float counter { get; set; }
		public float timeToTrigger = 1;

		[Tooltip("Speed that OnHover will return to zero")]
		public float returnMultiplier = 1;

		public bool pinged { get; set; }
		private bool prevPinged { get; set; }

		public bool idling { get; set; }

		public int triggerCounter { get; set; }


		[Tooltip("How many times will it trigger")]
		public int maxTriggers;

		public bool blocked { get; set; }
		public int blockCounter { get; set; }

		public bool neverBlockExit;
		public bool neverBlockEnter;
		public bool dontRetriggerUntilExit;

		int exitCounter =0;

		public void Ping(){
			pinged = true;
			idling = false;
		}
			
		void Update () {
			if (pinged && !prevPinged) {
				CheckBlocked ();
				if (!blocked || neverBlockEnter) {
					HandleEnter ();

				}
			} else if (pinged && prevPinged) {
				CheckBlocked ();
				if(!blocked)
					HandleHover ();
			} else if (!pinged && prevPinged) {
				CheckBlocked ();
				if (!blocked || neverBlockExit) {
					HandleExit ();
					exitCounter--;
				}
			} else if (!pinged && !prevPinged && !idling) {
				CheckBlocked ();
				if(!blocked)
					HandleReset ();
			}
			prevPinged = pinged;
			pinged = false;
		}

		public void CheckBlocked(){
			if (blockCounter > 0)
				blocked = true;
			else
				blocked = false;
			if(debug)
				Debug.Log ("blockCounter: " + blockCounter);
		}

		public void HandleEnter(){
			if(OnEnter!=null)
				OnEnter ();
			if (debug)
				Debug.Log ("Enter:" + this.gameObject.name);
		}

		public void HandleHover(){
			if (debug)
				Debug.Log ("Hover:" + this.gameObject.name);
			if (counter < timeToTrigger) {
				if(OnHover!=null)
					OnHover (counter / timeToTrigger);
				counter += Time.deltaTime;
			} else {
				if (OnTrigger != null) {
					triggerCounter++;
					if (maxTriggers == 0 || triggerCounter <= maxTriggers) {
						
						if (dontRetriggerUntilExit && exitCounter == 0)
							OnTrigger ();
						else if(!dontRetriggerUntilExit)
							OnTrigger ();
						
						if(exitCounter<1)
							exitCounter++;
					}
				}
			}
		}

		public void HandleExit(){
			if(OnExit!=null)
				OnExit ();
			if (debug)
				Debug.Log ("Exit:" + this.gameObject.name);
		}

		public void HandleReset(){
			if (debug)
				Debug.Log ("Reset:" + this.gameObject.name);
			if (counter > 0) {
				counter -= Time.deltaTime * returnMultiplier;
				counter = counter < 0 ? 0 : counter;
				if (OnHover != null)
					OnHover (counter / timeToTrigger);
				
			} else {
				if (OnIdle != null) {
					OnIdle ();
				}
				idling = true;
			}
		}
	}
}
