using System.Collections;
using UnityEngine;

public class ReturnToPool : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(ReturnObjectToPool());
    }
    private IEnumerator ReturnObjectToPool()
    {
        yield return new WaitForSeconds(1f);
        PoolManager.ReturnObjectToPool(gameObject);
    }
}
