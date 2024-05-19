using UnityEngine;
using UnityEngine.UI;

public class HubNavigationManager : MonoBehaviour
{
    public GameObject[] hubButtonsContainers;
    public Button nextButton;
    public Button backButton;

    private int currentContainerIndex = 0;

    private void Start()
    {
        nextButton.onClick.AddListener(ShowNextContainer);
        backButton.onClick.AddListener(ShowPreviousContainer);
        UpdateContainerVisibility();
        UpdateButtonVisibility();
    }

    private void ShowNextContainer()
    {
        if (currentContainerIndex < hubButtonsContainers.Length - 1)
        {
            hubButtonsContainers[currentContainerIndex].SetActive(false);
            currentContainerIndex++;
            hubButtonsContainers[currentContainerIndex].SetActive(true);
            UpdateButtonVisibility();
        }
    }

    private void ShowPreviousContainer()
    {
        if (currentContainerIndex > 0)
        {
            hubButtonsContainers[currentContainerIndex].SetActive(false);
            currentContainerIndex--;
            hubButtonsContainers[currentContainerIndex].SetActive(true);
            UpdateButtonVisibility();
        }
    }

    private void UpdateContainerVisibility()
    {
        for (int i = 0; i < hubButtonsContainers.Length; i++)
        {
            hubButtonsContainers[i].SetActive(i == currentContainerIndex);
        }
    }

    private void UpdateButtonVisibility()
    {
        nextButton.gameObject.SetActive(currentContainerIndex < hubButtonsContainers.Length - 1);
        backButton.gameObject.SetActive(currentContainerIndex > 0);
    }
}