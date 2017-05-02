using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;

namespace ON{
	[RequireComponent(typeof(ON_NodeGraph))]
	public class ON_MakeGraphFromMesh : MonoBehaviour {

	    List<ON_Node> nodes;
		Dictionary<string,ON_Node[]> edges;

	    public Mesh mesh;
	    Mesh weldedMesh;
	    public ON_Node node;
	    public GameObject GraphParent;

		public bool exportMesh;
	    public bool weld = false;

		public bool finishedBuilding { get; set; }
	   
		void Start () {
			
			finishedBuilding = false;
			nodes = GetComponent<ON_NodeGraph> ().Nodes;// new List<ON_Node>();
			edges = GetComponent<ON_NodeGraph> ().Edges;// new List<ON_Node>();

	        if(!weld)
	            weldedMesh = mesh;
	        else
	            weldedMesh = Weld.CopyMesh(mesh);
			
	        Weld.AutoWeld(weldedMesh, .0000001f, 15f);

	        if (GraphParent == null)
	            GraphParent = this.gameObject;

			StartCoroutine(instanceNodes());
		}

		IEnumerator instanceNodes(){
			for (int i = 0; i < weldedMesh.vertices.Length; i++)
			{
				nodes.Add(Instantiate(node));
				nodes[i].transform.parent = GraphParent.transform;
				nodes[i].transform.localPosition = weldedMesh.vertices[i];
				nodes[i].gameObject.name = "Node_" + i;
				nodes [i].id = i;
				if (nodes [i].GetComponent<ON_NodeGeometry> () != null)
					nodes [i].GetComponent<ON_NodeGeometry> ().Init ();
				yield return null;

			}
			StartCoroutine (addNeighbors ());
			yield return null;
		}

		IEnumerator addNeighbors(){
			for (int i = 0; i < weldedMesh.triangles.Length; i+=3)
			{
				nodes[weldedMesh.triangles[i]].AddSibling	 (nodes, edges, weldedMesh.triangles[i + 1]);
				nodes[weldedMesh.triangles[i]].AddSibling	 (nodes, edges, weldedMesh.triangles[i + 2]);
				nodes[weldedMesh.triangles[i + 1]].AddSibling(nodes, edges, weldedMesh.triangles[i]);
				nodes[weldedMesh.triangles[i + 1]].AddSibling(nodes, edges, weldedMesh.triangles[i + 2]);
				nodes[weldedMesh.triangles[i + 2]].AddSibling(nodes, edges, weldedMesh.triangles[i]);
				nodes[weldedMesh.triangles[i + 2]].AddSibling(nodes, edges, weldedMesh.triangles[i + 1]);
				yield return null;
			}
			finishedBuilding = true;
			yield return null;
		}


		public List<ON_Node> GetNodeList(){
			return nodes;
		}
	}
}
