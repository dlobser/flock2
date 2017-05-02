using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{
	public class ON_NodeGeometry : MonoBehaviour {

		public GameObject nodeGeometry;
		private GameObject nodeGeo;

//		public void Start(){
//			Init ();
//		}

		public void Init () {
			nodeGeo = Instantiate (nodeGeometry);
			nodeGeo.transform.SetParent (this.transform);
			nodeGeo.transform.localScale = Vector3.one;
			nodeGeo.transform.localEulerAngles = Vector3.zero;
			nodeGeo.transform.localPosition = Vector3.zero;
		}
	}
}