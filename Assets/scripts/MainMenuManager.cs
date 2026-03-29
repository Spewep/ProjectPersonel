using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Menus / Abas")]
    public GameObject creditsMenu;       
    public GameObject settingsMenu;
    [Header("Cenas")]
    public string playSceneName = "NomeDaCena";

    void Start()
    {
        if (creditsMenu != null) creditsMenu.SetActive(false);
        if (settingsMenu != null) settingsMenu.SetActive(false);
    }

    public void PlayGame()
    {
        if (!string.IsNullOrEmpty(playSceneName))
            SceneManager.LoadScene(playSceneName);
        else
            Debug.LogWarning("Nome da cena de Play não definido!");
    }

    public void OpenCredits()
    {
        if (creditsMenu != null)
            creditsMenu.SetActive(true);
    }

    public void OpenSettings()
    {
        if (settingsMenu != null)
            settingsMenu.SetActive(true);
    }

    public void CloseCredits()
    {
        if (creditsMenu != null)
            creditsMenu.SetActive(false);
    }

    public void CloseSettings()
    {
        if (settingsMenu != null)
            settingsMenu.SetActive(false);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
