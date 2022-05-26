using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class ScoreCollector : MonoBehaviour {
	public event UnityAction<float> onScoreUpdate = delegate { };

	private float _score;
	public float Score
	{
		get => _score; 
		set
		{
			_score = value;
			onScoreUpdate?.Invoke(Score);
		}
	}

	/// <summary>
	/// Substracts nearest multiple of 5 amount of score to given percent and returns substracted amount
	/// </summary>
	/// <param name="percents"></param>
	public void LoseScorePecents(float percents) {
		Score *= (1 - percents);
	}

	/// <summary>
	/// Same as <method>LoseScorePecents</method>, but random in range
	/// </summary>
	/// <param name="min"></param>
	/// <param name="max"></param>
	public void LoseScorePecentsRange(float min, float max) {
		LoseScorePecents(Random.Range(min, max));
	}
}
