using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [Header("Canvases")]
    public GameObject mainMenuCanvas;
    public GameObject audioCanvas;
    public GameObject videoCanvas;
    public GameObject helpCanvas;
    public GameObject creditsCanvas;
    public GameObject shopCanvas;
    public GameObject achivmentsCanvas;
    public GameObject tutorialCanvas;
    public GameObject checklistCanvas;
    public PlayerData playerData;
    public PlayerMove playerMove;

    private bool isPaused = false;

    void Start()
    {
        CloseAllMenus();
        mainMenuCanvas.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }
    public void PauseGame()
    {
        mainMenuCanvas.SetActive(true);
        isPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (playerMove != null)
            playerMove.SetMovementEnabled(false);
    }

    public void ResumeGame()
    {
        CloseAllMenus();
        mainMenuCanvas.SetActive(false);
        isPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerMove != null)
            playerMove.SetMovementEnabled(true);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        playerData.SaveData();
#else
        Application.Quit();
#endif
    }

    public void OpenAudioMenu()
    {
        CloseAllMenus();
        audioCanvas.SetActive(true);
    }

    public void OpenVideoMenu()
    {
        CloseAllMenus();
        videoCanvas.SetActive(true);
    }

    public void OpenHelpMenu()
    {
        CloseAllMenus();
        helpCanvas.SetActive(true);
    }

    public void OpenCreditsMenu()
    {
        CloseAllMenus();
        creditsCanvas.SetActive(true);
    }

    public void BackToMainMenu()
    {
        CloseAllMenus();
        mainMenuCanvas.SetActive(true);
    }
    public void OpenAchivmentsMenu()
    {
        CloseAllMenus();
        achivmentsCanvas.SetActive(true);
    }
    public void OpenShopMenu()
    {
        CloseAllMenus();
        shopCanvas.SetActive(true);
    }
    public void OpenTutorialMenu()
    {
        CloseAllMenus();
        tutorialCanvas.SetActive(true);
    }
    public void OpenChecklistMenu()
    {
        CloseAllMenus();
        checklistCanvas.SetActive(true);
    }
    private void CloseAllMenus()
    {
        if (audioCanvas != null) audioCanvas.SetActive(false);
        if (videoCanvas != null) videoCanvas.SetActive(false);
        if (helpCanvas != null) helpCanvas.SetActive(false);
        if (creditsCanvas != null) creditsCanvas.SetActive(false);
        if (shopCanvas != null) shopCanvas.SetActive(false);
        if (achivmentsCanvas != null) achivmentsCanvas.SetActive(false);
        if (tutorialCanvas != null)tutorialCanvas.SetActive(false);
        if (checklistCanvas != null)checklistCanvas.SetActive(false);
    }
}