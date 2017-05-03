using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{
	public class ON_TriggerEvents: MonoBehaviour {

		public ON_InteractableEvents interactable { get; set; }

		public enum parameters {OnEnter,OnExit,OnHover,OnTrigger};
		public parameters type;

	    public bool triggered { get; set; }
		public bool blocking;

	    public virtual void Ping() {
			if(blocking)
				interactable.blockCounter++;
		}
		public virtual void Ping(float t) { }

		public virtual void Animate(){}
		public virtual void Reset() { 
			if(blocking && interactable.blockCounter > 0)
				interactable.blockCounter--;
		}

		public virtual void OnEnable(){
			interactable = GetComponent<ON_InteractableEvents> ();
			switch (type) {
				case parameters.OnEnter:
					interactable.OnEnter += Ping;
					break;
				case parameters.OnExit:
					interactable.OnExit += Ping;
					break;
				case parameters.OnHover:
					interactable.OnHover += Ping;
					break;
				case parameters.OnTrigger:
					interactable.OnTrigger += Ping;
					break;
				default:
					break;
			}
			interactable.OnIdle += Reset;
		}

		public virtual void OnDisable()
		{
			switch (type) {
				case parameters.OnEnter:
					interactable.OnEnter -= Ping;
					break;
				case parameters.OnExit:
					interactable.OnExit -= Ping;
					break;
				case parameters.OnHover:
					interactable.OnHover -= Ping;
					break;
				case parameters.OnTrigger:
					interactable.OnTrigger -= Ping;
					break;
				default:
					break;
			}	
			interactable.OnIdle -= Reset ;
		}
	}
}


