using TMPro;
using UnityEngine;

public class HudHandler : MonoBehaviour
{
	public static HudHandler current;
	[SerializeField] private TMP_Text scoreTextMesh;

	[SerializeField] private GameObject MenuPanel;
	[SerializeField] private GameObject InGamePanel;
	[SerializeField] private TMP_Text infoTextMesh;

	private void Awake()
	{
		current = this;
	}

	private void Start()
	{
		infoTextMesh.text = $"MEILLEUR SCORE: {GameManager.instance.maxScore}\r\nNOMBRE DE PARTIES: {GameManager.instance.playCount}";
	}

	private void Update()
	{
		if (!InGamePanel.activeSelf)
		{
			if (Input.GetMouseButtonDown(0))
			{
				ActiveInGame(true);
			}
		}
	}

	public void UpdateScore(int score)
	{
		scoreTextMesh.text = $"{score}";
	}

	public void ActiveInGame(bool active)
	{
		MenuPanel.SetActive(!active);
		InGamePanel.SetActive(active);
	}
}
