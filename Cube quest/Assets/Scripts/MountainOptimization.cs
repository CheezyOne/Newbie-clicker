using UnityEngine;
using UnityEngine.UI;
using YG;

public class MountainOptimization : MonoBehaviour
{
    [SerializeField] private GameObject _normalMountain, _optimizedMountian, _highGraphicsText, _lowGraphicsText;
    private void Start()
    {
        if(Application.isMobilePlatform || YandexGame.EnvironmentData.isMobile)
        {
            ChangeOptimization(true);
        }
        else
        {
            ChangeOptimization(false);
        }
    }
    private void ChangeOptimization(bool Optimized)
    {
        if(Optimized)
        {
            _normalMountain.SetActive(false);
            _optimizedMountian.SetActive(true);
            _highGraphicsText.SetActive(false);
            _lowGraphicsText.SetActive(true);
        }
        else
        {
            _normalMountain.SetActive(true);
            _optimizedMountian.SetActive(false);
            _highGraphicsText.SetActive(true);
            _lowGraphicsText.SetActive(false);
        }
    }
    public void ChangeButton()
    {
        if (_normalMountain.activeSelf)
            ChangeOptimization(true);
        else
            ChangeOptimization(false);
    }
}
