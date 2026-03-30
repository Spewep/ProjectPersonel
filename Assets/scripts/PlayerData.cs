using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;
    public static event Action OnDataChanged;

    [Header("HUD")]
    public Text moneyText;
    public Text pointsText;
    public Image xpBar;
    public Text levelText;

    [Header("Valores do Player")]
    public int money = 0;
    public int points = 0;
    public int level = 0;
    public int currentXP = 0;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        money = PlayerPrefs.GetInt("Money", 0);
        points = PlayerPrefs.GetInt("Points", 0);
        level = PlayerPrefs.GetInt("Level", 0);
        currentXP = PlayerPrefs.GetInt("XP", 0);

        UpdateHUD();
    }

    void OnEnable()
    {
        OnDataChanged += UpdateHUD;
    }

    void OnDisable()
    {
        OnDataChanged -= UpdateHUD;
    }

    public void AddMoney(int amount)
    {
        money += amount;
        OnDataChanged?.Invoke();
    }

    public void AddPoints(int amount)
    {
        points += amount;
        AddXP(amount);
    }

    public void AddXP(int amount)
    {
        currentXP += amount;
        while (currentXP >= XPToNextLevel())
        {
            currentXP -= XPToNextLevel();
            level++;
        }
        OnDataChanged?.Invoke();
    }

    int XPToNextLevel()
    {
        return 2 * (int)Mathf.Pow(2, level);
    }

    void UpdateHUD()
    {
        if (moneyText != null) moneyText.text = "Dinheiro: " + money;
        if (pointsText != null) pointsText.text = "Pontos: " + points;
        if (levelText != null) levelText.text = "Nível: " + level;
        if (xpBar != null)  xpBar.fillAmount = (float)currentXP / XPToNextLevel();
    }

    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
        money = 0;
        points = 0;
        level = 0;
        currentXP = 0;
        OnDataChanged?.Invoke();
    }
    public void SaveData()
    {
        PlayerPrefs.SetInt("Money", money);
        PlayerPrefs.SetInt("Points", points);
        PlayerPrefs.SetInt("Level", level);
        PlayerPrefs.SetInt("XP", currentXP);
        PlayerPrefs.Save();

        Debug.Log("Dados salvos!");
    }
    public bool SpendMoney(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            OnDataChanged?.Invoke();
            return true;
        }

        return false;
    }

    public bool SpendPoints(int amount)
    {
        if (points >= amount)
        {
            points -= amount;
            OnDataChanged?.Invoke();
            return true;
        }

        return false;
    }
}