using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
	public static int countGround = 0;
	public static int directionCount = 0;
	private int maxDirection = 5;

	[SerializeField] private List<Ground> grounds = new List<Ground>();
	[SerializeField] private float timeToInit = 1.5f;
	[SerializeField] private float timeToDestroy = 0.5f;
	[SerializeField] private float yToDown = 10.0f;

	private IEnumerator Start()
	{
		StartCoroutine(InitAnimation());

		if (grounds.Count != 0)
		{
			yield return new WaitUntil(() => countGround < 10);

			Vector3 direction = (Random.value < 0.5f ? Vector3.forward : Vector3.right);
			if (Mathf.Abs(directionCount) > maxDirection)
			{
				if (directionCount > 0)
					direction = Vector3.forward;
				else
					direction = Vector3.right;
			}

			if (direction == Vector3.forward)
				directionCount--;
			else
				directionCount++;

			Vector3 positionToSpawn = transform.position + direction;
			positionToSpawn.y = 15;

			Ground newGround = Instantiate(grounds[Random.Range(0, grounds.Count)], positionToSpawn, Quaternion.identity, transform.parent);
			newGround.name = "Ground";
			countGround++;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			countGround--;
			StartCoroutine(DestroyAnimation());
		}
	}

	private IEnumerator InitAnimation()
	{
		float elapsedTime = 0.0f;
		Vector3 from = transform.position;
		Vector3 to = transform.position;
		to.y = 0;

		while (elapsedTime < timeToInit)
		{
			elapsedTime = Mathf.Min(elapsedTime + Time.deltaTime, timeToInit);
			float factor = elapsedTime / timeToDestroy;
			transform.position = Vector3.Lerp(from, to, factor);
			yield return null;
		}
	}

	private IEnumerator DestroyAnimation()
	{
		yield return new WaitForSeconds(1.0f);

		float elapsedTime = 0.0f;
		Vector3 from = transform.position;
		Vector3 to = transform.position + Vector3.down * yToDown;

		while (elapsedTime < timeToDestroy)
		{
			elapsedTime = Mathf.Min(elapsedTime + Time.deltaTime, timeToDestroy);
			float factor = elapsedTime / timeToDestroy;
			transform.position = Vector3.Lerp(from, to, factor);
			yield return null;
		}

		Destroy(gameObject);
	}
}
