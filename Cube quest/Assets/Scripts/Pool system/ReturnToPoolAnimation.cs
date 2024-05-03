using System.Collections;
using UnityEngine;

public class ReturnToPoolAnimation : MonoBehaviour
{
    private void OnParticleSystemStopped()
    {
        PoolManager.ReturnObjectToPool(gameObject);

    }
}
