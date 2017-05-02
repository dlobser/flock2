using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{
	public class ON_NodeGraph : MonoBehaviour {

		public List<ON_Node> Nodes { get; set; }
		public Dictionary<string,ON_Node[]> Edges;

		void Awake(){
			Nodes = new List<ON_Node> ();
			Edges = new Dictionary<string,ON_Node[]>();
		}	
	}
}