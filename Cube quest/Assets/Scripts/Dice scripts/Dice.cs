using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Dice : MonoBehaviour
{        
    private Rigidbody _rb;
    [SerializeField] private List<Transform> _sides;
    [SerializeField] private Material _standartMaterial;
    public GameObject ParticleSystem;
    public string Target;
    public bool IsGolden = false;
    public float ValueToIncrease = 0;
    public static Action<float, float, string> onDiceStop;

    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody>();
        StartCoroutine(WaitForCubeToStop());
        StopCoroutine(OutOfBoundsCoroutine());
    }

    private IEnumerator OutOfBoundsCoroutine()
    {
        yield return new WaitForSeconds(50f);
        StopRolling();
    }
    private IEnumerator WaitForCubeToStop()
    {
        yield return new WaitForSeconds(0.5f);
        if (_rb.velocity.magnitude <= 0.01f)
        {
            StopRolling();
            yield break;
        }
        yield return WaitForCubeToStop();
    }
    private void StopRolling()
    {
        // Определение результата (грани кубика)
        int UpperSide = 0;
        for(int i=0;i< _sides.Count;i++)
        {
            if (_sides[i].position.y > _sides[UpperSide].position.y)
                UpperSide = i;
        }
        onDiceStop?.Invoke(UpperSide + 1, ValueToIncrease, Target);
        DestroyCube();
    }
    public void DestroyCube()
    {
        if (transform.TryGetComponent<Renderer>(out Renderer RendererComponent))
            RendererComponent.material = _standartMaterial;
        else
            transform.GetChild(0).GetComponent<Renderer>().material = _standartMaterial;
        PoolManager.SpawnObject(ParticleSystem, transform.position, Quaternion.identity);
        IsGolden = false;
        PoolManager.ReturnObjectToPool(gameObject);
    }
}
