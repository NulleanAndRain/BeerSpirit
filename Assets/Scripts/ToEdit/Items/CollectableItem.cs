using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class CollectableItem : MonoBehaviour
{
	[SerializeField]
	private float _dissolveDuration;

	public Collider trigger;

	public event UnityAction<Collider> OnCollect = delegate { };
	public event UnityAction OnDissolveStart = delegate { };

	[SerializeField]
	private bool _collectableOnlyByPlayer;

	void OnTriggerEnter(Collider other)
	{
		if (_collectableOnlyByPlayer && other.tag != "Player") return;
		OnCollect(other);
	}

	public void Collect()
	{
		if (OnDissolveStart != null)
		{
			StartCoroutine(Dissolve());
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private IEnumerator Dissolve()
	{
		trigger.enabled = false;
		OnDissolveStart();
		yield return new WaitForSeconds(_dissolveDuration);
		Destroy(gameObject);
	}
}
