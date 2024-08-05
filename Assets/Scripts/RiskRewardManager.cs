using UnityEngine;

public class RiskRewardManager : MonoBehaviour
{
    public static RiskRewardManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EvaluateActionOutcome(UniversalCharacterController character, LocationManager.LocationAction action)
    {
        float successRate = CalculateSuccessRate(character, action);
        bool isSuccessful = Random.value < successRate;

        int scoreChange = isSuccessful ? 10 : -5; // Adjust these values as needed
        string outcomeDescription = isSuccessful ? "successfully completed" : "failed";

        GameManager.Instance.UpdatePlayerScore(character.characterName, scoreChange);
        GameManager.Instance.UpdateGameState(character.characterName, $"{outcomeDescription} action: {action.actionName}");

        // Display outcome to the player
        DisplayOutcome(isSuccessful, scoreChange);

        // Update character's mental model
        AIManager aiManager = character.GetComponent<AIManager>();
        if (aiManager != null)
        {
            aiManager.AddMemory($"{outcomeDescription} {action.actionName} at {Time.time}");
            aiManager.UpdateEmotionalState(isSuccessful ? EmotionalState.Happy : EmotionalState.Sad);
        }
    }

    private float CalculateSuccessRate(UniversalCharacterController character, LocationManager.LocationAction action)
    {
        float baseRate = action.baseSuccessRate;
        float roleBonus = (character.aiSettings.characterRole == action.requiredRole) ? 0.2f : 0f;
        
        // You can add more modifiers here based on character stats, experience, etc.

        return Mathf.Clamp01(baseRate + roleBonus);
    }

    private void DisplayOutcome(bool isSuccessful, int scoreChange)
    {
        // Implement this method to show a UI element with the outcome
        // For example, you could use a prefab with animation to display "+10 points" or "-5 points"
        Debug.Log($"Action {(isSuccessful ? "Succeeded" : "Failed")}! Score change: {scoreChange}");
    }
}