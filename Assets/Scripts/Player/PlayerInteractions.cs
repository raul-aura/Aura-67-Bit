using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractions : MonoBehaviour
{
    [Header("Class References")]
    [SerializeField] protected PlayerAnimation playerAnimationRef;
    [SerializeField] protected PlayerGrab playerGrabRef;

    [Header("Attack")]
    [SerializeField] protected float attackForce = 100f;
    protected const float attackRelease = 0.5f;
    protected float attackTime = 0;
    protected List<GameObject> enemiesInRange = new List<GameObject>();

    virtual protected void Update()
    {
        if(Input.GetKeyUp(KeyCode.Q))
        {
            Grab();
        }

        if(Input.GetKey(KeyCode.E))
        {
            Attack();
        }
        if (Input.GetKey(KeyCode.R))
        {
            Release();
        }
        if (attackTime < attackRelease)
        {
            attackTime += Time.deltaTime;
        }
        else
        {
            playerAnimationRef.SetIsAttacking(false);
        }
    }

    virtual protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameObject enemy = other.transform.parent.gameObject;
            enemiesInRange.Add(enemy);
        }
    }

    virtual protected void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameObject enemy = other.transform.parent.gameObject;
            enemiesInRange.Remove(enemy);
        }
    }

    virtual public void Attack()
    {
        attackTime = 0;
        playerAnimationRef.SetIsAttacking(true);
        foreach (GameObject enemy in enemiesInRange)
        {
            EnemyBehaviour enemyBehaviour = enemy.GetComponent<EnemyBehaviour>();
            if (!enemyBehaviour.GetIsDead())
            {
                enemyBehaviour.ReceiveAttack(transform.forward, attackForce);
            }
        }
    }

    virtual public void Grab()
    {
        foreach (GameObject enemy in enemiesInRange)
        {
            EnemyBehaviour enemyBehaviour = enemy.GetComponent<EnemyBehaviour>();
            if(enemyBehaviour)
            {
                if(enemyBehaviour.GetIsDead())
                {
                    playerGrabRef.GrabEnemy(enemy, enemyBehaviour);
                }
            }
        }
    }

    virtual public void Release()
    {
        playerGrabRef.ReleaseEnemy();
    }
}
