using System.Collections;
using UnityEngine;
using YG;

public class FullScreen : MonoBehaviour
{
    [SerializeField] private float _timeBeforeAd;
    private void OnEnable()
    {
        BoostersButtons.onBoostAdStart += ShowRewardedAdd;
    }
    private void OnDisable()
    {
        BoostersButtons.onBoostAdStart -= ShowRewardedAdd;
    }
    private void ShowRewardedAdd(int AddIndex)
    {
        YandexGame.RewVideoShow(AddIndex);
    }
    private void Start()
    {
        StartCoroutine(AdCoroutine());
    }
    private IEnumerator AdCoroutine()
    {
        yield return new WaitForSeconds(_timeBeforeAd);
        YandexGame.FullscreenShow();
        yield return AdCoroutine();
    }
}
