using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LobbyNetworkManager : Photon.PunBehaviour
{
    [SerializeField]
    private int MaxPlayerPerRoom = 2;
    private Text connectState;
    private Text connectPlayer;
    // Start is called before the first frame update
    //private void Awake()
    //{
    //    PhotonNetwork.ConnectToRegion(CloudRegionCode.cn, "0.0.1");
    //}
    void Start()
    {
        PhotonNetwork.autoJoinLobby = false;
        PhotonNetwork.automaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings("0.0.1");
        connectState = transform.Find("ConnectState").GetComponent<Text>();
        connectPlayer = transform.Find("ConnectPlayer").GetComponent<Text>();
        connectState.text = "Connecting";
    }

    public override void OnConnectedToMaster()
    {
        connectState.text = "Connected";
        connectPlayer.text = "Not in Room";
        Debug.Log("Linking Region is :" + PhotonNetwork.CloudRegion);
    }
    public override void OnDisconnectedFromPhoton()
    {
        connectState.text = "Disconnected";
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void StartMatching()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    

    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        PhotonNetwork.CreateRoom("1");
        connectState.text = "Join Room Failed";
    }

    public override void OnCreatedRoom()
    {
        connectState.text = "Created Room";
    }

    public override void OnJoinedRoom()
    {
        UpdatePlayerCount();
        connectState.text = "Joined Room Succeed";
    }

    void UpdatePlayerCount()
    {
        connectPlayer.text = PhotonNetwork.playerList.Length + " player joined";
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        int playerCount = PhotonNetwork.playerList.Length;
        UpdatePlayerCount();
        if (playerCount == MaxPlayerPerRoom)
        {
            if (PhotonNetwork.isMasterClient)
            {
                PhotonNetwork.LoadLevel(1);
            }
        }
    }


}
