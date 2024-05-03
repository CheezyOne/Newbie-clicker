using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDiceRolling : MonoBehaviour
{
    private float _rollSpeed, _xRoll, _yRoll, _zRoll, _rollTime;
    private void Awake()
    {
        NewRoll();
        StartCoroutine(CubeRoll());
    }
    private IEnumerator CubeRoll()
    {
        yield return new WaitForSeconds(_rollTime);
        NewRoll();
        yield return CubeRoll();
    }
    private void Update()
    {
        transform.Rotate(new Vector3(_xRoll, _yRoll, _zRoll), _rollSpeed*Time.deltaTime);
    }
    private void NewRoll()
    {
        _rollSpeed = Random.Range(40, 75);
        _xRoll=Random.Range(-360, 360);
        _yRoll = Random.Range(-360, 360);
        _zRoll = Random.Range(-360, 360);
        _rollTime = Random.Range(3, 5);
    }
}
