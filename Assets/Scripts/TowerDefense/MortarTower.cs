using UnityEngine;
using System.Collections.Generic;

public class MortarTower : MonoBehaviour
{

	[SerializeField, Range(0.5f, 5f)]
	float shotsPerSecond = 1f;

	[SerializeField]
	Transform mortar = default;
    
    [SerializeField]
    float targetingRange = 5f;

    [SerializeField, Min(1f)]
    float shellSpeedMultiplier = 1f;

    float launchSpeed;
    float launchProgress;

    public GameObject shellPrefab;
    private List<Transform> enemiesInRange = new List<Transform>();

	void Awake () {
		OnValidate();
	}

	void OnValidate () {
		float x = targetingRange + 0.25001f;
		float y = -mortar.position.y;
		launchSpeed = Mathf.Sqrt(9.81f * (y + Mathf.Sqrt(x * x + y * y)));
	}
    
    void Update()
    {
		launchProgress += shotsPerSecond * Time.deltaTime;
		while (launchProgress >= 1f) {
            Transform target = GetClosestEnemy();
			if (target != null) {
				Launch(target);
				launchProgress -= 1f;
			}
			else {
				launchProgress = 0.999f;
			}
		}
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Add(other.transform);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
             enemiesInRange.Remove(other.transform);
        }
    }

    Transform GetClosestEnemy()
    {
    Transform closestEnemy = null;
    float shortestDistance = Mathf.Infinity;

    foreach (Transform enemy in enemiesInRange)
    {
		    if(enemy != null)
		    {
	        float distanceToEnemy = Vector3.Distance(transform.position, enemy.position);
	        if (distanceToEnemy < shortestDistance)
	        {
	            shortestDistance = distanceToEnemy;
	            closestEnemy = enemy;
	        }
	       } else
	       {
		       enemiesInRange.Remove(enemy);
		       closestEnemy = null;
		       return closestEnemy;
	       }
    }
    return closestEnemy;
    }

    public void Launch (Transform targetMortar) 
    {
		Vector3 launchPoint = mortar.position;
		Vector3 targetPoint = targetMortar.position;
		targetPoint.y = 0f;

		Vector2 dir;
		dir.x = targetPoint.x - launchPoint.x;
		dir.y = targetPoint.z - launchPoint.z;

        float x = dir.magnitude;
		float y = -launchPoint.y;
		dir /= x;

		float g = 9.81f;
		float shellSpeed = launchSpeed;
		float shellAcc = shellSpeed * shellSpeed;

		float launchArc = shellAcc * shellAcc - g * (g * x * x + 2f * y * shellAcc);
		float tanTheta = (shellAcc + Mathf.Sqrt(launchArc)) / (g * x);
		float cosTheta = Mathf.Cos(Mathf.Atan(tanTheta));
		float sinTheta = cosTheta * tanTheta;
        
        //snelheid bepalen
        Vector3 velocity = new Vector3(
        shellSpeed * cosTheta * dir.x,
        shellSpeed * sinTheta,
        shellSpeed * cosTheta * dir.y
        ) * shellSpeedMultiplier;

        GameObject shellObj = Instantiate(shellPrefab, launchPoint, Quaternion.identity);
        Shell s = shellObj.GetComponent<Shell>();
        s.Initialize(launchPoint, targetPoint, velocity);


	}
}

