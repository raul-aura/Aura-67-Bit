using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 0.3f;
    private float cooldownElapsed = 0;
    private bool canAttack = true;

    private List<GameObject> enemiesInRange = new List<GameObject>();

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Attack();
        }
        if (!canAttack)
        {
            cooldownElapsed += Time.deltaTime;
            if (cooldownElapsed >= attackCooldown)
            {
                canAttack = true;
                cooldownElapsed = 0;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameObject enemy = other.gameObject;
            enemiesInRange.Add(enemy);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameObject enemy = other.gameObject;
            enemiesInRange.Remove(enemy);
        }
    }

    public void Attack()
    {
        if (canAttack)
        {
            foreach (GameObject enemy in enemiesInRange)
            {
                enemy.GetComponentInParent<EnemyBehaviour>().ReceiveAttack(transform.forward);
            }
            canAttack = false;
        }
    }
}
