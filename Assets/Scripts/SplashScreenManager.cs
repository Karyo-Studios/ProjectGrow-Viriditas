using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenManager : MonoBehaviour
{
    private bool canProceed = false;

    private void Start()
    {
        // Enable the flag to allow proceeding after a short delay
        Invoke("EnableProceed", 0.5f);
    }

    private void Update()
    {
        // Check if any key is pressed and if the flag is enabled
        if (canProceed && Input.anyKeyDown)
        {
            // Load the challenge lobby scene
            SceneManager.LoadScene("ChallengeLobby");
        }
    }

    private void EnableProceed()
    {
        // Enable the flag to allow proceeding
        canProceed = true;
    }
}