using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{
	public class ON_ObjectPool : MonoBehaviour {

	    public GameObject poolContainer;
	    public int poolSize;
	    public GameObject poolObject;
	    Vector3 scale;

	    void Awake () {
			if (poolContainer == null)
				poolContainer = this.gameObject;
	        scale = poolObject.transform.localScale;
	        for (int i = 0; i < poolSize; i++)
	        {
	            GameObject thisPoolObject = Instantiate(poolObject);
	            thisPoolObject.transform.parent = poolContainer.transform;
	        }
		}

	    public GameObject PoolInstantiate()
	    {
	        if (poolContainer.transform.childCount == 0)
	        {
	            GameObject thisPoolObject = Instantiate(poolObject);
	            thisPoolObject.transform.parent = poolContainer.transform;
	        }
			poolContainer.transform.GetChild (0).gameObject.SetActive (true);
	        return poolContainer.transform.GetChild(0).gameObject;
	    }

	    public void PoolDestroy(GameObject thisPoolObject)
	    {
	        thisPoolObject.transform.parent = poolContainer.transform;
			thisPoolObject.transform.localPosition = Vector3.zero;

//			thisPoolObject.transform.localScale = Vector3.zero;
			thisPoolObject.SetActive (false);
	    }
	}
}
