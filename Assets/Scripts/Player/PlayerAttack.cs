using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Class References")]
    [SerializeField] private PlayerAnimation playerAnimationRef;
    [SerializeField] private PlayerGrab playerGrabRef;

    [SerializeField] private float attackForce = 10f;
    private const float attackRelease = 0.5f;
    private float attackTime = 0;

    private List<GameObject> enemiesInRange = new List<GameObject>();

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Q))
        {
            Grab();
        }

        if(Input.GetKey(KeyCode.E))
        {
            Attack();
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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameObject enemy = other.transform.parent.gameObject;
            enemiesInRange.Add(enemy);
            Debug.Log(enemy);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameObject enemy = other.transform.parent.gameObject;
            enemiesInRange.Remove(enemy);
        }
    }

    public void Attack()
    {
        attackTime = 0;
        playerAnimationRef.SetIsAttacking(true);
        foreach (GameObject enemy in enemiesInRange)
        {
            enemy.GetComponent<EnemyBehaviour>().ReceiveAttack(transform.forward, attackForce * Time.deltaTime);
        }
    }

    public void Grab()
    {
        foreach (GameObject enemy in enemiesInRange)
        {
            if(enemy.GetComponent<EnemyBehaviour>().GetIsDead())
            {
                enemy.GetComponent<EnemyBehaviour>().DisableRaggdoll();
                playerGrabRef.GrabEnemy(enemy);
            }
        }
    }
}
