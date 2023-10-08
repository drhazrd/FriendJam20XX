using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    public float attackCooldown = 1f; // Cooldown between attacks in seconds
    public Animator animator;

    private float lastAttackTime;

    void Update()
    {
        // Check for attack input
        if (Input.GetKeyDown(KeyCode.Space) && Time.time - lastAttackTime >= attackCooldown)
        {
            Attack();
        }
    }

    void Attack()
    {
        // Play different attacks based on the combo count
        int comboCount = animator.GetInteger("ComboCount");
        string attackAnimation = "Attack" + (comboCount + 1);

        // Trigger the attack animation in the animator
        animator.SetTrigger(attackAnimation);

        // Increase the combo count for the next attack
        animator.SetInteger("ComboCount", (comboCount + 1) % 3);

        // Record the time of the last attack
        lastAttackTime = Time.time;
    }
}
