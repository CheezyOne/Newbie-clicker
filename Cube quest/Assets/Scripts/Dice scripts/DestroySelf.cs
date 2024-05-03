using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    [SerializeField] private float _timeToDie=2f;
    private void Start()
    {
        Destroy(gameObject, _timeToDie);
    }
}
