using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    protected Rigidbody[] enemyRagdoll;
    protected Collider enemyCollider;

    protected Vector3 attackDirection = Vector3.zero;
    protected float attackImpulse;

    protected bool isDead = false;

    virtual protected void Start()
    {
        enemyRagdoll = GetComponentsInChildren<Rigidbody>();
        DisableRaggdoll();
    }

    virtual protected void ToggleRagdoll(bool isRagdoll)
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

    virtual public void ReceiveAttack(Vector3 attacker, float impulse = 100f)
    {
        isDead = true;
        attackDirection = attacker;
        attackImpulse = impulse * Time.deltaTime;
        ToggleRagdoll(true);
    }

    virtual public void DisableRaggdoll()
    {
        ToggleRagdoll(false);
    }

    public bool GetIsDead()
    {
        return isDead;
    }
}
