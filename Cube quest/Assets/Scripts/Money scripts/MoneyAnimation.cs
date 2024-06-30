using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MoneyAnimation : MonoBehaviour
{
    private void Awake()
    {
        transform.DOMoveX(transform.position.x+Random.Range(-1f,1f),Random.Range(0.3f,1f));
        transform.DOMoveY(transform.position.y + Random.Range(-2f, 0), Random.Range(0.3f, 1f));
        GetComponent<Text>().DOFade(0, Random.Range(2, 3));
    }
}
