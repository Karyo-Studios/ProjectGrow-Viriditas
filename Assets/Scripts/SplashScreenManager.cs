using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenManager : MonoBehaviour
{
    public float delay = 3f; // Delay in seconds before transitioning to the character selection scene

    private void Start()
    {
        // Invoke the LoadCharacterSelectionScene method after the specified delay
        Invoke("LoadCharacterSelectionScene", delay);
    }

    private void LoadCharacterSelectionScene()
    {
        // Load the character selection scene
        SceneManager.LoadScene(1);
    }
}