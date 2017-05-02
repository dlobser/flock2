using System;
using UnityEngine;

public struct IndexedPos 
{
	public int index;
	public Vector3 position;
	public IndexedPos(int _index, Vector3 _position){
		this.index = _index;
		this.position = _position; 
	}
}

