using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    [Header("Config")]
    public float attackRange = 2f;
    public SkillManager skillManager;


    [Header("Cooldown")]
    public float attackCooldown = 0.5f;
    private float lastAttackTime;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    void Attack()
    {
        Vector3 origin = transform.position + Vector3.up;
        Vector3 direction = transform.forward;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, attackRange))
        {
            //Debug.Log("Acertou: " + hit.collider.name);

            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                interactable.Interact();
            }
        }

        Debug.DrawRay(origin, direction * attackRange, Color.green, 1f);
    }
    public int GetDamage()
    {
        int damage = 1;

        if (skillManager != null && skillManager.HasSkill("Ataque Forte"))
        {
            damage = 3;
        }

        return damage;
    }
}
