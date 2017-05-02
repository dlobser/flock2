using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class FindEmptyCoordinateMVideo : MonoBehaviour {

	public float innerRadius;
	public float outerRadius;
	public float height;

	public Vector3 emptyCoordinate;

	public Vector3 GetEmptyCoordinate(){
		Vector2 circle = Random.insideUnitCircle;
		circle *= outerRadius - innerRadius;
		circle = circle + (circle.normalized*innerRadius) ;
		return new Vector3 (circle.x, height, circle.y);
	}


}
