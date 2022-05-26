using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollectableItem))]
public class HealCollectable : MonoBehaviour
{
    [Min(0)]
    [SerializeField]
    private float HealAmount;
    CollectableItem _collectable;

    void Start()
    {
        _collectable = GetComponent<CollectableItem>();
        _collectable.OnCollect += Collect;
    }

    void Collect(Collider coll)
    {
        var h = coll.GetComponent<Health>();
        if (h == null) return;
        h.Heal(HealAmount);
        _collectable.Collect();
    }
}