using UnityEngine;

public class Shell : MonoBehaviour
{
	Vector3 launchPoint, targetPoint, launchVelocity;
    float age;
    [SerializeField] float maxLifetime = 10f;

    bool exploded = false;
	
	public void Initialize (
		Vector3 launchPoint, Vector3 targetPoint, Vector3 launchVelocity
	) {
		this.launchPoint = launchPoint;
        this.targetPoint = targetPoint;
		this.launchVelocity = launchVelocity;

        age = 0f;
	}

    void Update(){
        age += Time.deltaTime;
        
        if (age > maxLifetime)
        {
            Explode();
            return;
        }

		Vector3 shellPosition = launchPoint + launchVelocity * age;
		shellPosition.y -= 0.5f * 9.81f * age * age;

        if (float.IsNaN(shellPosition.x))
        {
            Destroy(gameObject);
            return;
        }

		transform.localPosition = shellPosition;
        Vector3 lookDirection = launchVelocity;
		lookDirection.y -= 9.81f * age;
		transform.localRotation = Quaternion.LookRotation(lookDirection);

        if (transform.position.y <= 0.05f)
        {
            Explode();
        }


    }
    void OnTriggerEnter(Collider other)
    {
        if (exploded) return;

        if (other.CompareTag("Enemy"))
        {
            Explode();
        }
    }

    void Explode()
    {
        if (exploded) return; 
        exploded = true;

        Debug.Log("KABOOM!");

        Destroy(gameObject, 0.01f);
    }
}

