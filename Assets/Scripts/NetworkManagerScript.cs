using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public InputField roomCodeInput;
    public Button joinButton;
    public Button createButton;

    private void Start()
    {
        // Assign click listeners to buttons
        joinButton.onClick.AddListener(JoinRoom);
        createButton.onClick.AddListener(CreateRoom);
    }

    private void CreateRoom()
    {
        // Generate a random room code
        string roomCode = GenerateRoomCode();

        // Set the room options
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 5; // Set the maximum number of players allowed in the room

        // Create the room with the generated code and options
        PhotonNetwork.CreateRoom(roomCode, roomOptions);
    }

    private void JoinRoom()
    {
        // Get the entered room code
        string roomCode = roomCodeInput.text;

        // Join the room with the entered code
        PhotonNetwork.JoinRoom(roomCode);
    }

    private string GenerateRoomCode()
    {
        // Generate a random 4-digit room code
        return Random.Range(1000, 9999).ToString();
    }

    public override void OnJoinedRoom()
    {
        // Load the Challenge Lobby scene when the player joins a room
        PhotonNetwork.LoadLevel("ChallengeLobby");
    }
}