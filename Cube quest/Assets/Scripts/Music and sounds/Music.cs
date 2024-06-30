using System.Collections;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] private AudioClip[] _allMusic;
    private AudioSource _audioSource;
    private int _musicIndex = 0;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        StartCoroutine(MusicCoroutine());
    }
    private IEnumerator MusicCoroutine()
    {
        _audioSource.clip = _allMusic[_musicIndex];
        _audioSource.Play();
        yield return new WaitForSeconds(_allMusic[_musicIndex].length);
        _musicIndex++;
        if (_musicIndex == _allMusic.Length)
            _musicIndex = 0;
        yield return MusicCoroutine();
    }
}
