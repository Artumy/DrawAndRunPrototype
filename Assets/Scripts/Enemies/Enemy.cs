using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _splineForEnemy;

    public static event Action<GameObject> Deleted;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Saw saw))
        {
            saw.PlayEffect();
            Deleted?.Invoke(_splineForEnemy);
            Destroy(_splineForEnemy);
        }
        
    }
}
