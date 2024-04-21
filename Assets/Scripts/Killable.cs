using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : MonoBehaviour
{
	public int health = 100;
	//public GameObject DeathPrefab;

	void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.CompareTag("Trap"))
		{
			TakeDamage(50);
		}
	}

	void TakeDamage(int damage)
	{
		health -= damage;
		if (health <= 0)
		{
			Debug.Log("Entity has been destroyed.");
			//Instantiate(DeathPrefab, transform.position, Quaternion.identity);
			Destroy(gameObject); 
		}
		else
		{
			Debug.Log("Entity took damage. Current health: " + health);
		}
	}
}
