using UnityEngine;

[System.Serializable]
public class Skill
{
    public string skillName;
    public CostType costType;
    public int cost;

    public bool isUnlocked = false;

    public void Buy()
    {
        if (isUnlocked)
        {
            Debug.Log("Já comprado");
            return;
        }

        bool canBuy = false;

        if (costType == CostType.Money)
        {
            canBuy = PlayerData.Instance.SpendMoney(cost);
        }
        else if (costType == CostType.Points)
        {
            canBuy = PlayerData.Instance.SpendPoints(cost);
        }

        if (canBuy)
        {
            Unlock();
        }
        else
        {
            Debug.Log("Não tem recurso suficiente");
        }
    }

    void Unlock()
    {
        isUnlocked = true;
        Debug.Log("Skill desbloqueada: " + skillName);
    }
}