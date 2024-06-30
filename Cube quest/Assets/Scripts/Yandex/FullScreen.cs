using UnityEngine;
using YG;

public class FullScreen : MonoBehaviour
{
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
}
