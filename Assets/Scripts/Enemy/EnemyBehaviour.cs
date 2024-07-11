using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private Rigidbody[] enemyRagdoll;
    private Collider enemyCollider;

    private Vector3 attackDirection = Vector3.zero;
    private float attackImpulse;

    private bool isDead = false;

    void Start()
    {
        enemyRagdoll = GetComponentsInChildren<Rigidbody>();
        DisableRaggdoll();
    }

    void ToggleRagdoll(bool isRagdoll)
    {
        foreach (Rigidbody body in enemyRagdoll)
        {
            body.isKinematic = !isRagdoll;
            if (isRagdoll)
            {
                body.AddForce(attackDirection * attackImpulse, ForceMode.Impulse);
            }
        }
        GetComponent<Animator>().enabled = !isRagdoll;
    }

    public void ReceiveAttack(Vector3 attacker, float impulse = 10f)
    {
        isDead = true;
        attackDirection = attacker;
        attackImpulse = impulse;
        ToggleRagdoll(true);
    }

    public void DisableRaggdoll()
    {
        ToggleRagdoll(false);
    }

    public bool GetIsDead()
    {
        return isDead;
    }
}
