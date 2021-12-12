using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private AudioSource _audioSource;
    private bool _isOpened;
    private float _deltaVolume;
    private float _volume;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _isOpened = false;
        _deltaVolume = 0.01f;
    }

    private void Start()
    {
        _audioSource.Play();
        _audioSource.volume = 0f;
    }

    private void FixedUpdate()
    {
        Alarm();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _isOpened = !_isOpened;
            Open();
        }
    }

    private void Open()
    {
        _animator.SetTrigger("Person");
    }

    private void Alarm()
    {
        if (_isOpened)
        {
            _volume = Mathf.MoveTowards(_audioSource.volume, _audioSource.maxDistance, _deltaVolume);
            _audioSource.volume = _volume;
        }
        else 
        {
            _volume = Mathf.MoveTowards(_audioSource.volume, _audioSource.minDistance, -_deltaVolume);
            _audioSource.volume = _volume;
        }
    }
}
