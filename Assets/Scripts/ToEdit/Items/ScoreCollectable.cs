using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollectableItem))]
public class ScoreCollectable : MonoBehaviour {

    [SerializeField]
    private float _score;
    private CollectableItem _collectable;

    void Start() {
        _collectable = GetComponent<CollectableItem>();
        _collectable.OnCollect += Collect;
    }

    void Collect(Collider other)
    {
        var collector = other.GetComponent<ScoreCollector>();
        if (collector == null) return;
        collector.Score += _score;
        _collectable.Collect();
    }
}
