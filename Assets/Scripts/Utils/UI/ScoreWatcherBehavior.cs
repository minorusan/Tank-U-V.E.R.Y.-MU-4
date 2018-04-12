using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core.GamePlay;


namespace Utils.UI
{
	
}
public class ScoreWatcherBehavior : MonoBehaviour
{
	private int _currentScore;
	private int _highScore;

	public Text highScore;
	public Text[] currentScore;

	private void Awake()
	{
		HealthBehaviour.didDieAtPositionGlobal += OnMonsterDied;
		_highScore = PlayerPrefs.GetInt ("high_score");
	}

	void OnMonsterDied(Vector3 obj)
	{
		_currentScore++;
		if (_currentScore > _highScore)
		{
			_highScore = _currentScore;
			PlayerPrefs.SetInt ("high_score", _highScore);
			highScore.text = _highScore.ToString ();
		}
		for (int i = 0; i < currentScore.Length; i++)
		{
			currentScore [i].text = _currentScore.ToString ();
		}
	}

	private void OnDisable()
	{
		HealthBehaviour.didDieAtPositionGlobal -= OnMonsterDied;
	}
}
