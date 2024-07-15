using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    [SerializeField] protected uint initialMaxGrabs = 2;
    protected uint maxGrabs;
    [SerializeField] protected float heightOffsetAdditive = 1f;
    protected float heightOffset;

    protected List<GameObject> grabbedEnemies = new List<GameObject>();

    virtual protected void Start()
    {
        maxGrabs = initialMaxGrabs;
    }

    virtual public void MoveGrabbed(Vector3 playerMove)
    {
        if(grabbedEnemies.Count > 0)
        {
            float followSpeed = 50f;
            Vector3 previousPosition = grabbedEnemies[0].transform.position;
            foreach (GameObject enemy in grabbedEnemies)
            {
                Vector3 targetPosition = previousPosition + playerMove;
                targetPosition.y = enemy.transform.position.y;
                enemy.transform.position = Vector3.Lerp(enemy.transform.position, targetPosition, Time.deltaTime * followSpeed);
                previousPosition = enemy.transform.position;
                followSpeed *= 0.9f;
            }
        }
    }

    virtual public void RotateGrabbed(Vector3 playerRotation)
    {
        if (grabbedEnemies.Count > 0)
        {
            float rotationSpeed = 50f;
            Quaternion previousRotation = grabbedEnemies[0].transform.rotation;

            foreach (GameObject enemy in grabbedEnemies)
            {
                Quaternion targetRotation = previousRotation * Quaternion.Euler(playerRotation);
                enemy.transform.rotation = Quaternion.Lerp(enemy.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
                previousRotation = enemy.transform.rotation;
                rotationSpeed *= 0.9f; 
            }
        }
    }

    virtual public void GrabEnemy(GameObject enemy, EnemyBehaviour behaviour)
    {
        if (!grabbedEnemies.Contains(enemy) && grabbedEnemies.Count < maxGrabs)
        {
            enemy.transform.Rotate(90f, 0f, 0f);
            if (behaviour)
            {
                behaviour.DisableRaggdoll();
            }
            Vector3 newPosition = transform.position + Vector3.up * (GetComponent<Collider>().bounds.size.y + heightOffset);
            enemy.transform.position = newPosition;
            heightOffset += heightOffsetAdditive;
            grabbedEnemies.Add(enemy);
        }
    }

    virtual public void ReleaseEnemy()
    {
        if (grabbedEnemies.Count > 0)
        {
            foreach (GameObject enemy in grabbedEnemies)
            {
                EnemyBehaviour behaviour = enemy.GetComponent<EnemyBehaviour>();
                if (behaviour)
                {
                    behaviour.ReceiveAttack(transform.forward, 200f);
                }
                enemy.transform.Rotate(-90f, 0f, 0f);
            }
            grabbedEnemies.Clear();
            heightOffset = 0f;
        }
    }

    virtual public void IncreaseMaxGrab(uint value)
    {
        maxGrabs += value;
    }
}
