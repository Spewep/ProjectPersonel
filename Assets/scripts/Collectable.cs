using UnityEngine;

public class Collectable : MonoBehaviour, IInteractable
{
    public int value = 1;
    public bool Destroyable, enemy, rock, pillar;

    public void Interact()
    {
        int valueFinal = value;

        SkillManager sm = FindObjectOfType<SkillManager>();

        if (sm != null && sm.HasSkill("Mineração Rápida"))
        {
            valueFinal *= 2;
        }

        if (PlayerData.Instance == null)
        {
            Debug.LogError("PlayerData NÃO EXISTE!");
        }
        if (pillar == true ) { PlayerData.Instance.AddMoney(valueFinal); PlayerData.Instance.AddPoints(valueFinal); };

        if (enemy == true ) { PlayerData.Instance.AddPoints(valueFinal); }

        if (rock == true ) { PlayerData.Instance.AddMoney(valueFinal); }

        if (Destroyable == true)Destroy(gameObject);
    }
}