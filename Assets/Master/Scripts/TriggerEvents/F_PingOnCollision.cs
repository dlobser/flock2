using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON { 
    public class F_PingOnCollision : MonoBehaviour {
        public string otherName = "Viewer";
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == otherName) {
                GetComponent<ON_InteractableEvents>().Ping();
                Debug.Log("pop");
            }

        }
    }
}
