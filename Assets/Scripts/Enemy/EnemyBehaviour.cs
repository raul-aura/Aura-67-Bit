using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float forceMultiplier = 15f;

    private Rigidbody[] enemyRagdoll;
    private Vector3 attackDirection = Vector3.zero;

    void Start()
    {
        enemyRagdoll = GetComponentsInChildren<Rigidbody>();
        ToggleRagdoll(false);
    }


    void ToggleRagdoll(bool isRagdoll)
    {
        foreach (Rigidbody body in enemyRagdoll)
        {
            body.isKinematic = !isRagdoll;
            if (isRagdoll)
            {
                body.AddForce(attackDirection * forceMultiplier, ForceMode.Impulse);
            }
        }
        GetComponent<Animator>().enabled = !isRagdoll;
    }

    public void ReceiveAttack(Vector3 attacker)
    {
        attackDirection = attacker;
        ToggleRagdoll(true);
    }
}
