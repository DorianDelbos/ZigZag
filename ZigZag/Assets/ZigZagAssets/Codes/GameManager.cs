using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	public int maxScore = 0;
	public int playCount = 0;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			transform.SetParent(null);
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
			return;
		}

		maxScore = PlayerPrefs.GetInt("MaxScore");
		playCount = PlayerPrefs.GetInt("PlayCount");
	}

	public void OnPlayerDeath()
	{
		Player player = FindAnyObjectByType<Player>();

		maxScore = Mathf.Max(maxScore, player.Score);

		PlayerPrefs.SetInt("MaxScore", maxScore);
		PlayerPrefs.SetInt("PlayCount", ++playCount);

		Ground.countGround = 0;
		Ground.directionCount = 0;

		SceneManager.LoadScene(0);
	}
}
