using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OpenShop : MonoBehaviour
{
    private bool _isOpen = false;
    private float _xDistance = 530;
    [SerializeField] private GameObject _shop;
    public void OpenCloseShop()
    {
        if (!_isOpen)
            _shop.transform.DOLocalMove(new Vector3(_shop.transform.localPosition.x - _xDistance, _shop.transform.position.y, _shop.transform.position.z), 0.3f);
        else
            _shop.transform.DOLocalMove(new Vector3(_shop.transform.localPosition.x + _xDistance, _shop.transform.position.y, _shop.transform.position.z), 0.3f);
        _isOpen = !_isOpen;
        StartCoroutine(WaitForMovement());
    }
    private IEnumerator WaitForMovement()
    {
        GetComponent<Button>().interactable = false;
        yield return new WaitForSeconds(0.4f);
        GetComponent<Button>().interactable = true;
    }
}
