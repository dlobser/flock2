using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class F_SprayParticlesFromController : NetworkBehaviour
{
	private SteamVR_TrackedController _controller;
	private PrimitiveType _currentPrimitiveType = PrimitiveType.Sphere;
	public ParticleSystem particleSystem;
	ParticleSystem.EmissionModule emission;
	GameObject trackedObject;
	public float sprayAmount = 3;

	[SyncVar]//(hook="UpdateSpray")]
	public bool spray;

	[Command]
	void CmdUpdateSpray (bool newVal) {
		spray = newVal;
	}

	void Update(){

		if (trackedObject == null) {
			if (this.GetComponent<F_CopyXForms> ().target != null) {
				trackedObject = this.GetComponent<F_CopyXForms> ().target.gameObject;
				if (trackedObject != null) {
					if (trackedObject.GetComponent<SteamVR_TrackedController> () != null) {
						_controller = trackedObject.GetComponent<SteamVR_TrackedController> ();
						_controller.TriggerClicked += HandleTriggerClicked;
						_controller.TriggerUnclicked += HandleTriggerUnClicked;
					}
				}
			}
		}

		if (spray) {
			emission.rateOverTime = sprayAmount;
		}
		else
			emission.rateOverTime = 0;
	}

	private void Start()
	{
		emission = particleSystem.emission;

	}

	private void OnDisable()
	{
		if(_controller!=null)
			_controller.TriggerClicked -= HandleTriggerClicked;
	}

	private void HandleTriggerClicked(object sender, ClickedEventArgs e)
	{
		CmdUpdateSpray (true);
	}

	private void HandleTriggerUnClicked(object sender, ClickedEventArgs e)
	{
		CmdUpdateSpray (false);
	}

}