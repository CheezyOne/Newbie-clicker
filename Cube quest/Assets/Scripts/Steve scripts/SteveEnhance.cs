using DG.Tweening;
using UnityEngine;

public class SteveEnhance : MonoBehaviour
{
    private Vector3 _normalScale, _enhancedScale;
    private void Awake()
    {
        _normalScale = transform.localScale;
        _enhancedScale = transform.localScale + transform.localScale * 0.1f;
    }
    private void OnEnable()
    {
        SteveHolder.onSteveClick += Enhance;
    }
    private void OnDisable()
    {
        SteveHolder.onSteveClick -= Enhance;
    }
    private void Enhance() 
    {
        Sequence EnhanceSequence = DOTween.Sequence();
        EnhanceSequence.Append(transform.DOScale(_enhancedScale, 0.1f));
        EnhanceSequence.Append(transform.DOScale(_normalScale, 0.1f));
        EnhanceSequence.Play();
    }
}
