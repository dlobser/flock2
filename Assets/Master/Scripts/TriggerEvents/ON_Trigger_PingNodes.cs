using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{
	public class ON_Trigger_PingNodes : ON_TriggerEvents {

//		public ON_ObjectPool pool;
//
//		public bool pingpong;
//
//		float counter = 0;
//		bool triggered = false;
//
//		void OnEnable(){
//			base.OnEnable ();
//		}
//
//		void OnDisable(){
//			base.OnDisable ();
//		}
//
//		public override void Ping() {
//			base.Ping ();
//		}
//
//		public override void Ping(float t){
////			if (!triggered)
////				GetInitialPosition ();
//			Animate (t);
//			triggered = true;
//		}
//
//		public override void Reset(){
//			base.Reset ();
//		}
//
//		void Idle(){
//			triggered = false;
//		}
//
//		IEnumerator HandlePing(){
//			counter = 0;
//			while (counter < timeToScale) {
//				counter += Time.deltaTime;
//				Animate (counter / timeToScale);
//				yield return null;
//			}
//			if(pingpong)
//				StartCoroutine(UnAnimate ());
//			else
//				Reset ();
//		}
//
//		IEnumerator UnAnimate(){
//			while (counter > 0) {
//				counter -= Time.deltaTime;
//				Animate (counter / timeToScale);
//				yield return null;
//			}
//			Reset ();
//		}
//
//		void Animate(float t){
//			if (smoothMove)
//				t = Mathf.SmoothStep (0, 1, t);
//
////			while (counter < 1) {
////				counter += (Time.deltaTime * (pingSpeed / dist));
////				pingGeo.transform.localPosition = Vector3.Lerp (this.transform.localPosition, sibling.transform.localPosition, (counter / 1));
////				float sc = Mathf.Sin (Mathf.PI * (counter / 1));// ((Mathf.Cos(Mathf.PI * 2 * (counter / 1)) - 1) * -.5f);
////				sc *= inScale;
////				pingGeo.transform.localScale = new Vector3 (sc, sc, sc);
////
////				if (bounce) {
////					newHeight.Set (0, sc * bounceHeight, 0);
////					pingGeo.transform.Translate (newHeight);
////				}
////
////				//pingGeo.transform.localScale = new Vector3(pingGeo.transform.localScale.x  , pingGeo.transform.localScale.y , dist * (counter/1) );
////				yield return new WaitForSeconds (Time.deltaTime);
////			}
//
//			this.transform.localScale = Vector3.Lerp (init,  privateScale, t);
//
//		}
	}
}
