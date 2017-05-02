using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{
	[RequireComponent(typeof(ON_NodeGraph))]
	public class ON_DrawGeoBetweenNodes : MonoBehaviour {

		public GameObject wireGeometry;
		public List<GameObject> wires;
		public GameObject wireParent;
		ON_Node node;

		float counter = 0;

		ON_NodeGraph nodes;
		bool finishedBuilding;

		void Start(){
			if (wireParent == null)
				wireParent = this.gameObject;
			wires = new List<GameObject> ();
			nodes = GetComponent<ON_NodeGraph> ();
		}

		void Update () {
			if (GetComponent<ON_MakeGraphFromMesh> ().finishedBuilding && !finishedBuilding) {
				StartCoroutine (MakeGeometry ());
				finishedBuilding = true;
			}
		}

		IEnumerator MakeGeometry(){
			foreach(KeyValuePair<string, ON_Node[]> entry in nodes.Edges)
			{
				Vector3 a = entry.Value [0].transform.position;
				Vector3 b = entry.Value [1].transform.position;
				GameObject wire = Instantiate (wireGeometry);
				wire.transform.position = Vector3.Lerp (a, b, .5f);
				wire.transform.LookAt (a);
				wire.transform.localScale = new Vector3 (
					wire.transform.localScale.x, wire.transform.localScale.x,
					Vector3.Distance (a, b));
				wires.Add (wire);
				wire.transform.SetParent (wireParent.transform);
				yield return null;
			}
		}
	}
}
