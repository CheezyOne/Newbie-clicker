using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AchievmentsPopUp : MonoBehaviour
{
    [SerializeField] private GameObject _achievmentPrefab;
    [SerializeField] private Transform _moveToPosition;
    private void OnEnable()
    {
        AchievmentsGetter.onNewAchievmentGet += PopUp;
    }
    private void OnDisable()
    {
        AchievmentsGetter.onNewAchievmentGet -= PopUp;
    }
    private void PopUp(string Text, string Name, Sprite Image)
    {
        GameObject NewAchievment = Instantiate(_achievmentPrefab, gameObject.transform);
        Destroy(NewAchievment, 4f);
        if ((float)Screen.width / (float)Screen.height > 1.61)
        {
            NewAchievment.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        else if ((float)Screen.width / (float)Screen.height > 1.51)
        {
            NewAchievment.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
        }
        else 
        {
            NewAchievment.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        }
        NewAchievment.transform.GetChild(1).GetComponent<Image>().sprite = Image;
        NewAchievment.transform.GetChild(2).GetComponent<Text>().text = Text;
        NewAchievment.transform.GetChild(3).GetComponent<Text>().text = Name;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(NewAchievment.transform.DOMove(_moveToPosition.position, 0.5f));
        sequence.Append(NewAchievment.transform.DOMoveY(transform.position.y-10, 0.5f).SetDelay(2f));
    }
}
