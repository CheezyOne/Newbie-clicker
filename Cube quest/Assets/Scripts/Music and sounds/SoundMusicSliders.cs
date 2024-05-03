using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundMusicSliders : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private Slider _musicSlider, _soundSlider;

    private const string MUSIC_STRING = "MusicParam", SOUNDS_STRING = "SoundParam";
    private void Awake()
    {
        _musicSlider.onValueChanged.AddListener(SliderMusic);
        _soundSlider.onValueChanged.AddListener(SliderSound);
    }
    private void SliderMusic(float Value)
    {
        _mixer.SetFloat(MUSIC_STRING,Mathf.Log10(Value)*20);
    }
    private void SliderSound(float Value)
    {
        _mixer.SetFloat(SOUNDS_STRING, Mathf.Log10(Value) * 20);
    }
}
