using UnityEngine;

public class Bonus : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			other.GetComponent<Player>().Score += 2;
			Destroy(gameObject);
		}
	}
}
