using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    public void PlayEffect()
    {
        _particleSystem.gameObject.SetActive(true);
        _particleSystem.Play();
        Destroy(_particleSystem, 5f);
    }
}
