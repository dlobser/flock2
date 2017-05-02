using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class WeldMesh : MonoBehaviour {

	static string file = "Assets/WeldedMesh.asset";
	static Mesh mesh;
	static Mesh weldedMesh;

	[MenuItem("ON/Weld")]

	static void weld()
	{
//		file = EditorGUILayout.TextField ("Text Field", file);
		mesh = Selection.activeGameObject.GetComponent<MeshFilter> ().sharedMesh;

		weldedMesh = Weld.CopyMesh(mesh);
		AssetDatabase.CreateAsset(weldedMesh, file );
		AssetDatabase.SaveAssets();
//		WeldMesh window = (WeldMesh)EditorWindow.GetWindow (typeof (WeldMesh));
	}
//	void OnGUI () {
//		GUILayout.Label ("Base Settings", EditorStyles.boldLabel);
//		ScriptableWizard.DisplayWizard<WeldMesh> ("boobs");
//		file = EditorGUILayout.TextField ("Text Field", file);
////		mesh = EditorGUILayout.
//
////		groupEnabled = EditorGUILayout.BeginToggleGroup ("Optional Settings", groupEnabled);
////		myBool = EditorGUILayout.Toggle ("Toggle", myBool);
////		myFloat = EditorGUILayout.Slider ("Slider", myFloat, -3, 3);
////		EditorGUILayout.EndToggleGroup ();
//	}
////

//
//	void Start () {
//		weldedMesh = Weld.CopyMesh(mesh);
//		AssetDatabase.CreateAsset(weldedMesh, file );
//		AssetDatabase.SaveAssets();
//	}

}
