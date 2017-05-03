using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public interface ITouchable {
	void Touch (NetworkInstanceId handId);
	void Untouch (NetworkInstanceId handId);
}
