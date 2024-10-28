using System.Collections;
using System.Linq;
using Unity.Cinemachine;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] private CinemachineCamera virtualCamera;
	[SerializeField] private float speed = 10.0f;
	[SerializeField] private LayerMask groundLayer;
	private Vector3 direction = Vector3.zero;
	private int score = 0;
	public bool isDead = false;

	public int Score
	{
		get => score;
		set
		{
			score = value;
			HudHandler.current.UpdateScore(score);
		}
	}

	public bool IsMoving => direction != Vector3.zero;

	private void Update()
	{
		if (!isDead)
			UpdateInputs();

		UpdateMovement();
		CheckGround();
	}

	private void UpdateInputs()
	{
		if (!Input.GetMouseButtonDown(0))
			return;

		ChangeMovement();
		Score++;
	}

	private void UpdateMovement()
	{
		transform.position += direction * Time.deltaTime * speed;
	}

	private void CheckGround()
	{
		if (Physics.OverlapBox(transform.position, Vector3.one * 0.1f, Quaternion.identity, groundLayer).Any())
			return;

		direction = Vector3.down * 2.5f;
		isDead = true;
		virtualCamera.gameObject.SetActive(false);
		StartCoroutine(PlayerDeath());
	}

	private void ChangeMovement()
	{
		if (direction == Vector3.forward)
			direction = Vector3.right;
		else
			direction = Vector3.forward;
	}

	private IEnumerator PlayerDeath()
	{
		yield return new WaitForSeconds(1.0f);
		GameManager.instance.OnPlayerDeath();
	}
}
