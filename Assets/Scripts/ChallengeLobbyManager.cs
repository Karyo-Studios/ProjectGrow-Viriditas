using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class ChallengeLobbyManager : MonoBehaviourPunCallbacks
{
    public PlayerListManager playerListManager;
    public Button[] hubButtons;

    private void Start()
    {
        // Set the initial state of the hub buttons
        SetHubButtonsInteractable(false);

        // Connect to the Photon server
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        // Assign click listeners to hub buttons
        for (int i = 0; i < hubButtons.Length; i++)
        {
            int index = i;
            hubButtons[i].onClick.AddListener(() => SelectHub(index));
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon server.");
        // Join or create a room after successful connection
        PhotonNetwork.JoinOrCreateRoom("ChallengeLobby", new RoomOptions(), TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room: " + PhotonNetwork.CurrentRoom.Name);
        SetHubButtonsInteractable(true);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        playerListManager.UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        playerListManager.UpdatePlayerList();
    }

    private void SelectHub(int index)
    {
        // Set the selected hub index in the room properties
        ExitGames.Client.Photon.Hashtable roomProperties = new ExitGames.Client.Photon.Hashtable();
        roomProperties["SelectedHubIndex"] = index;
        PhotonNetwork.CurrentRoom.SetCustomProperties(roomProperties);

        // Load the Challenges scene
        PhotonNetwork.LoadLevel("Challenges");
    }

    private void SetHubButtonsInteractable(bool interactable)
    {
        for (int i = 0; i < hubButtons.Length; i++)
        {
            hubButtons[i].interactable = interactable;
        }
    }
}