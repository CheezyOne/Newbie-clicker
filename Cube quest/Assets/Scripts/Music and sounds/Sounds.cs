using UnityEngine;

public class Sounds : MonoBehaviour
{
    [SerializeField] private AudioClip[] _allSounds;
    private AudioSource _audioSource;

    private void OnEnable()
    {
        _audioSource= GetComponent<AudioSource>();
        Dice.onDiceStop += PlayDiceDisappearence;
        ShopButtons.onDecreaseMoney += PlayBuyUpgrade;
        SteveHolder.onSteveClick += PlayClick;
        SteveUpgrade.onSteveUpgrade += PlaySteveUpgrade;
        AchievmentsGetter.onNewAchievmentGet += PlayAchievmentGet;
    }
    private void OnDisable()
    {
        Dice.onDiceStop -= PlayDiceDisappearence;
        ShopButtons.onDecreaseMoney -= PlayBuyUpgrade;
        SteveHolder.onSteveClick -= PlayClick;
        SteveUpgrade.onSteveUpgrade -= PlaySteveUpgrade;
        AchievmentsGetter.onNewAchievmentGet -= PlayAchievmentGet;
    }

    private void PlaySteveUpgrade(int Useless)
    {
        PlaySound(_allSounds[Random.Range(0,2)]);
    }
    private void PlayClick()
    {
        PlaySound(_allSounds[2]);
    }
    private void PlayDiceDisappearence(float Useless, float Useless1, string Useless2)
    {
        PlaySound(_allSounds[3], 0.8f);
    }
    private void PlayBuyUpgrade(float Useless)
    {
        PlaySound(_allSounds[4], Volume: 0.7f);
    }
    private void PlayAchievmentGet(string Useless, string Useless1, Sprite Useless2)
    {
        PlaySound(_allSounds[5], 0.9f, 1,1);
    }
    private void PlaySound(AudioClip Clip, float Volume = 1f, float Pitch1 = 1f, float Pitch2 = 1f, float Delay = 0)
    {
        _audioSource.clip = Clip;
        _audioSource.volume = Volume;
        _audioSource.pitch = Random.Range(Pitch1, Pitch2);
        if (Delay == 0)
        {
            _audioSource.PlayOneShot(Clip, Volume);
        }
        else
        {
            _audioSource.PlayDelayed(Delay);
        }
    }
}
