using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ChallengeCard : MonoBehaviour, IPointerClickHandler
{
    public TextMeshProUGUI challengeNameText;
    public GameObject expandedChallengeObject;
    public Button voteButton;
    public Button expandedChallengeButton;

    private ChallengesManager challengesManager;
    private bool isExpanded = false;

    private void Start()
    {
        challengesManager = FindObjectOfType<ChallengesManager>();

        if (expandedChallengeObject != null)
        {
            expandedChallengeObject.SetActive(false);
        }

        if (voteButton != null)
        {
            voteButton.onClick.AddListener(OnVoteButtonClicked);
        }

        if (expandedChallengeButton != null)
        {
            expandedChallengeButton.onClick.AddListener(ToggleExpand);
        }
    }

    public void SetChallengeDetails(string challengeName)
    {
        if (challengeNameText != null)
        {
            challengeNameText.text = string.IsNullOrEmpty(challengeName) ? "Challenge" : challengeName;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ToggleExpand();
    }

    private void ToggleExpand()
    {
        isExpanded = !isExpanded;

        if (expandedChallengeObject != null)
        {
            expandedChallengeObject.SetActive(isExpanded);
        }

        if (isExpanded)
        {
            challengesManager.OnChallengeExpanded(this);
        }
        else
        {
            challengesManager.OnChallengeCollapsed();
        }
    }

    private void OnVoteButtonClicked()
    {
        int challengeIndex = transform.GetSiblingIndex();
        challengesManager.OnChallengeSelected(challengeIndex);
        ToggleExpand();
    }
}