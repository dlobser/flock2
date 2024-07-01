using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.XR;
using Valve.VR;

public class F_Player : NetworkBehaviour {

    //for example purposes
    public InputField inputField;
    public Renderer cubeMat;
    //public bool isGreen;

    public GameObject vrCameraRig;
    public GameObject fpsCtrl;
    GameObject vrCameraRigInstance;
    GameObject fpsCtrlInstance;

    public GameObject[] AvatarHide;
    public GameObject[] AvatarShow;
    public GameObject[] AvatarSpectate;

    [SyncVar]
    public bool spectator;

    [Command]
    public void CmdSyncSpectator(bool val) {
        spectator = val;
        SetSpectator();
    }

    public void SetSpectator() {
        for (int i = 0; i < AvatarSpectate.Length; i++) {
            AvatarSpectate[i].SetActive(!spectator);
        }
    }

    void Update() {
        if(Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.J)) {
            spectator = !spectator;
            CmdSyncSpectator(spectator);
            if(isLocalPlayer && this.GetComponent<F_IsLocalPlayer>() != null)
                SetSpectator();
        }
    }

    public override void OnStartLocalPlayer () {
        if (GameObject.Find("TangoParent")) {
            CmdSyncSpectator(true);
            SetSpectator();
            return;
        }

        if (spectator)
            SetSpectator();

        if (!isClient)
            return;

        // delete main camera
        Camera.main.gameObject.SetActive(false);

        this.gameObject.AddComponent<F_IsLocalPlayer>();

        if (XRSettings.isDeviceActive) {
            vrCameraRigInstance = (GameObject)Instantiate(vrCameraRig, transform.position, transform.rotation);

            Debug.Log(XRSettings.isDeviceActive);

            Transform bodyOfVrPlayer = transform.Find("VRPlayerBody");
            if (bodyOfVrPlayer != null)
                bodyOfVrPlayer.parent = null;

            GameObject head = vrCameraRigInstance.GetComponentInChildren<SteamVR_Camera>().gameObject;
            transform.parent = head.transform;

            GameObject.Find("ResetPlayer").GetComponent<F_ResetPlayer>().PlayerController = vrCameraRigInstance;
        } else {
            fpsCtrlInstance = (GameObject)Instantiate(fpsCtrl, transform.position, transform.rotation);

            Debug.Log(XRSettings.isDeviceActive);

            GameObject head = fpsCtrlInstance.transform.GetChild(0).gameObject;
            transform.parent = head.transform;

            GameObject.Find("ResetPlayer").GetComponent<F_ResetPlayer>().PlayerController = fpsCtrlInstance;
        }

        F_Players.thisPlayer = this.gameObject;
        F_Players.thisPlayerID = F_Players.players;

        if (isLocalPlayer) {
            for (int i = 0; i < AvatarHide.Length; i++) {
                AvatarHide[i].SetActive(false);
            }
            for (int i = 0; i < AvatarShow.Length; i++) {
                AvatarShow[i].SetActive(true);
            }
        }
    }
}
