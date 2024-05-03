using UnityEngine;

public class SteveUpgradeEffect : MonoBehaviour
{
    [SerializeField] private GameObject _diamondEffect, _emeraldEffect;
    private void OnEnable()
    {
        SteveUpgrade.onSteveUpgrade += CreateEffect;
    }
    private void OnDisable()
    {
        SteveUpgrade.onSteveUpgrade -= CreateEffect;
    }
    private void CreateEffect(int Useless)
    {
        Destroy(Instantiate(_diamondEffect, transform.position, Quaternion.identity, transform), 5f);
        Destroy(Instantiate(_emeraldEffect, transform.position, Quaternion.identity, transform), 5f);
    }
}
