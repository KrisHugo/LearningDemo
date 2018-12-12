using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetwork : Photon.PunBehaviour
{
    public PlayerInfo pi;
    [SerializeField]
    private CameraController camCtrl;

    public Behaviour[] stuffsNeedDisable;

    [SerializeField]
    private string remotePlayerLayer = "RemotePlayer";
    [SerializeField]
    private string localPlayerLayer = "LocalPlayer";
    [SerializeField]
    private string dontDrawLayer = "DontDraw";

    [SerializeField]
    private GameObject playerGraghics;
    PhotonView pv;
    WeaponController wc;

    void Start()
    {
        pi = GetComponent<PlayerInfo>();
        pv = GetComponent<PhotonView>();
        camCtrl = transform.Find("CameraHandle").GetComponent<CameraController>();
        wc = GetComponent<WeaponController>();
        if (!pv.isMine)
        {
            AssignRemotePlayer();
            pi.OnStart();
            foreach (Behaviour stuff in stuffsNeedDisable)
            {
                stuff.enabled = false;
            }
        }
        else
        {
            AssignLocalPlayer();
            camCtrl.Setup();
            wc.Setup(camCtrl.GetCamera());
            GetComponent<PlayerMotor>().SetCameraController(camCtrl);
            //SetLayerRecursively(playerGraghics, LayerMask.NameToLayer(dontDrawLayer));
        }
        gameObject.name = "Player:" + pv.viewID;
    }

    void AssignRemotePlayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remotePlayerLayer);
    }
    void AssignLocalPlayer()
    {
        gameObject.layer = LayerMask.NameToLayer(localPlayerLayer);
    }

    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        obj.layer = newLayer;
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }

    void Update()
    {
        
    }
}
