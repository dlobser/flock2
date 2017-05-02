using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{
	public class ON_Node : MonoBehaviour {

		public List<ON_Node> siblings;
		public List<int> siblingIndices { get; set; }

		public int id;

	    void Awake () {
			
	        siblings = new List<ON_Node>();
	        siblingIndices = new List<int>();
		
		}

		public bool AddSibling(List<ON_Node> nodes, int index){
			bool alreadyIndexed = false;
			for (int i = 0; i < siblingIndices.Count; i++)
			{
				if(index == siblingIndices[i])
				{
					alreadyIndexed = true;
					break;
				}
			}
			if (!alreadyIndexed)
			{
				siblingIndices.Add(index);
				siblings.Add(nodes[index]);
			}
			return alreadyIndexed;
		}

		public void AddSibling(List<ON_Node> nodes, Dictionary<string,ON_Node[]> edges, int index){
			bool addEdge = AddSibling (nodes, index);
			if (!addEdge) {
//				
				if (!edges.ContainsKey (index+","+id) && !edges.ContainsKey (id+","+index)) {
					edges.Add ((id+","+index), new ON_Node[]{ this, nodes [index] });

				}
			}

		}
	}
}
