using UnityEngine;

public class Enemy : MonoBehaviour, IInteractable
{
    [Header("Status")]
    public int maxHealth = 5;
    private int currentHealth;

    [Header("Recompensa")]
    public int rewardMoney = 5;
    public int rewardPoints = 5;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void Interact()
    {
        int damage = 1;

        PlayerAttack pa = FindObjectOfType<PlayerAttack>();

        if (pa != null)
        {
            damage = pa.GetDamage();
        }

        TakeDamage(damage);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Debug.Log("Inimigo tomou dano! Vida: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Inimigo morreu!");

        PlayerData.Instance.AddMoney(rewardMoney);
        PlayerData.Instance.AddPoints(rewardPoints);

        Destroy(gameObject);
    }
}