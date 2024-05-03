using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] private AudioClip[] _allMusic;
    private AudioSource _audioSource => GetComponent<AudioSource>();
    private int _musicIndex = 0;

    private void Update()
    {
        if (_audioSource.isPlaying)
            return;
        _musicIndex++;
        if (_musicIndex == _allMusic.Length)
            _musicIndex = 0;
        _audioSource.clip = _allMusic[_musicIndex];
        _audioSource.Play();
    }
}
