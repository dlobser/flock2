using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public interface IUsable {
	void StartUsing(NetworkInstanceId handId);
	void StopUsing(NetworkInstanceId handId);
}
