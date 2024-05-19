using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class CharacterSelectionManager : MonoBehaviourPunCallbacks
{
    public GameObject characterButtonsContainer;
    public Color selectedColor = Color.green;
    public Color defaultColor = Color.white;
    public PlayerListManager playerListManager;

    private Button[] characterButtons;
    private int selectedCharacterIndex = -1;
    private Dictionary<int, bool> playerReadyStatus = new Dictionary<int, bool>();

    private void Start()
    {
        // Get all the character buttons from the CharacterButtonsContainer
        characterButtons = characterButtonsContainer.GetComponentsInChildren<Button>();

        // Assign click listeners to character buttons
        for (int i = 0; i < characterButtons.Length; i++)
        {
            int index = i;
            characterButtons[i].onClick.AddListener(() => SelectCharacter(index));
        }

        // Initialize player ready status
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            playerReadyStatus[player.ActorNumber] = false;
        }
    }

    private void SelectCharacter(int index)
    {
        if (selectedCharacterIndex == index)
        {
            // Deselect the character if already selected
            DeselectCharacter();
        }
        else
        {
            // Deselect the previously selected character
            DeselectCharacter();

            // Select the new character
            selectedCharacterIndex = index;
            characterButtons[index].GetComponent<Image>().color = selectedColor;

            // Notify other players about the character selection
            photonView.RPC("UpdateCharacterSelection", RpcTarget.Others, PhotonNetwork.LocalPlayer.ActorNumber, index);

            // Update player ready status
            playerReadyStatus[PhotonNetwork.LocalPlayer.ActorNumber] = true;

            // Check if all players are ready
            if (CheckAllPlayersReady())
            {
                // Load the game scene for all players
                PhotonNetwork.LoadLevel("GameScene");
            }
        }
    }

    private void DeselectCharacter()
    {
        if (selectedCharacterIndex >= 0)
        {
            characterButtons[selectedCharacterIndex].GetComponent<Image>().color = defaultColor;
            selectedCharacterIndex = -1;

            // Update player ready status
            playerReadyStatus[PhotonNetwork.LocalPlayer.ActorNumber] = false;
        }
    }

    [PunRPC]
    private void UpdateCharacterSelection(int playerActorNumber, int characterIndex)
    {
        // Update the character selection for other players
        characterButtons[characterIndex].GetComponent<Image>().color = Color.red;

        // Display player name on the character button
        Player player = PhotonNetwork.CurrentRoom.GetPlayer(playerActorNumber);
        if (player != null)
        {
            characterButtons[characterIndex].GetComponentInChildren<TextMeshProUGUI>().text = player.NickName;
        }

        // Update player ready status
        playerReadyStatus[playerActorNumber] = true;
    }

    private bool CheckAllPlayersReady()
    {
        foreach (bool isReady in playerReadyStatus.Values)
        {
            if (!isReady)
            {
                return false;
            }
        }
        return true;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (playerListManager != null)
        {
            playerListManager.UpdatePlayerList();
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        playerReadyStatus.Remove(otherPlayer.ActorNumber);

        if (playerListManager != null)
        {
            playerListManager.UpdatePlayerList();
        }
    }
}