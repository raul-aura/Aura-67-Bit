using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    [SerializeField] private float heightOffsetAdditive = 1f;
    private float heightOffset;

    private List<GameObject> grabbedEnemies = new List<GameObject>();

    public void MoveGrabbed(Vector3 playerMove)
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

    public void RotateGrabbed(Vector3 playerRotation)
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

    public void GrabEnemy(GameObject enemy)
    {
        if (!grabbedEnemies.Contains(enemy))
        {
            enemy.transform.Rotate(90f, 0f, 0f);
            Vector3 newPosition = transform.position + Vector3.up * (GetComponent<Collider>().bounds.size.y + heightOffset);
            enemy.transform.position = newPosition;
            heightOffset += heightOffsetAdditive;
            grabbedEnemies.Add(enemy);
        }
    }
}
