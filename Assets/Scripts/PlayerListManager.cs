using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class PlayerListManager : MonoBehaviourPunCallbacks
{
    public Button[] playerPlaceholders;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        UpdatePlayerList();
    }

    public override void OnJoinedRoom()
    {
        UpdatePlayerList();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
    }

    public void UpdatePlayerList()
    {
        foreach (Button placeholder in playerPlaceholders)
        {
            placeholder.gameObject.SetActive(false);
        }

        Player[] players = PhotonNetwork.PlayerList;
        for (int i = 0; i < players.Length; i++)
        {
            if (i >= playerPlaceholders.Length)
            {
                break;
            }

            Button placeholder = playerPlaceholders[i];
            placeholder.gameObject.SetActive(true);

            TextMeshProUGUI playerNameText = placeholder.GetComponentInChildren<TextMeshProUGUI>();
            if (players[i].IsLocal)
            {
                playerNameText.text = "ME";
            }
            else
            {
                playerNameText.text = players[i].NickName;
            }
        }
    }

    public void UpdatePlayerVotingStatus(int playerActorNumber, bool hasVoted)
    {
        if (playerActorNumber <= 0 || playerActorNumber > playerPlaceholders.Length)
        {
            return;
        }

        Button placeholder = playerPlaceholders[playerActorNumber - 1];

        Image placeholderImage = placeholder.GetComponent<Image>();
        if (placeholderImage != null)
        {
            placeholderImage.color = hasVoted ? new Color(0x0D / 255f, 0x81 / 255f, 0x57 / 255f) : Color.white;
        }
    }
}