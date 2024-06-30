using UnityEngine;
using YG;

public class DiceSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _dicePrefabs;
    [SerializeField] private Material[] _materials;
    [SerializeField] private GameObject[] _particles;
    [SerializeField] private string _diceTarget;
    private GameObject _dicePrefab;
    private int _diceCounter = 0;

    private const float _throwForce = 200f;
    private void OnEnable()
    {
        ShopButtons.onCubeUpgrade += UpgradeCube;
        SteveHolder.onSteveClick += RollDice;
        ShopButtons.onClickBuy += RollBlueDice;
        ShopButtons.onTickBuy += RollBlueDice;
        ResetProgress.Reset += ResetDice;
    }
    private void OnDisable()
    {
        ShopButtons.onCubeUpgrade -= UpgradeCube;
        SteveHolder.onSteveClick -= RollDice;
        ShopButtons.onClickBuy -= RollBlueDice;
        ShopButtons.onTickBuy -= RollBlueDice;
        ResetProgress.Reset -= ResetDice;
    }
    private void Awake()
    {
        _dicePrefab = _dicePrefabs[YandexGame.savesData.CurrentDice];
        _diceCounter = YandexGame.savesData.CurrentDice;
    }
    private void UpgradeCube()
    {
        _diceCounter++;
        _dicePrefab = _dicePrefabs[_diceCounter];
    }
    private Vector3 GetRandomForceVector3()
    {
        return new Vector3
            (
            UnityEngine.Random.Range(-_throwForce, _throwForce),
            UnityEngine.Random.Range(-_throwForce, _throwForce),
            UnityEngine.Random.Range(-_throwForce, _throwForce)
            );
    }
    private void ApplyRandomGold(GameObject NewCube)
    {
        if (UnityEngine.Random.Range(0, 100) == 69)
        {
            Dice DiceComponent = NewCube.GetComponent<Dice>();
            DiceComponent.IsGolden = true;
            DiceComponent.ParticleSystem = _particles[1];
            if (NewCube.GetComponent<Renderer>() != null)
            {
                NewCube.GetComponent<Renderer>().material = _materials[0];//Bepis sent these dices a bit strange, so...
            }
            else
            {
                NewCube.transform.GetChild(0).GetComponent<Renderer>().material = _materials[0];
            }
        }
    }
    private void RollBlueDice(float IncreaseValue, string Target)
    {
        GameObject NewCube = PoolManager.SpawnObject(_dicePrefab, transform.position, Quaternion.identity);
        NewCube.GetComponent<Dice>().ParticleSystem = _particles[2];
        if (NewCube.GetComponent<Renderer>() != null)
        {
            NewCube.GetComponent<Renderer>().material = _materials[1];//Bepis sent these dices a bit strange, so...
        }
        else
        {
            NewCube.transform.GetChild(0).GetComponent<Renderer>().material = _materials[1];
        }
        Vector3 RandomVector = GetRandomForceVector3();
        NewCube.GetComponent<Rigidbody>().AddForce(RandomVector);
        NewCube.transform.SetPositionAndRotation(transform.position, UnityEngine.Random.rotation);
        NewCube.GetComponent<Dice>().Target = Target;
        NewCube.GetComponent<Dice>().ValueToIncrease = IncreaseValue;
    }
    private void RollDice()
    {
        GameObject NewCube = PoolManager.SpawnObject(_dicePrefab, transform.position, Quaternion.identity);
        NewCube.GetComponent<Dice>().ParticleSystem = _particles[0];
        ApplyRandomGold(NewCube);
        Vector3 RandomVector = GetRandomForceVector3();
        NewCube.GetComponent<Rigidbody>().AddForce(RandomVector);
        NewCube.transform.SetPositionAndRotation(transform.position, UnityEngine.Random.rotation);
        NewCube.GetComponent<Dice>().Target = _diceTarget;
    }
    private void ResetDice()
    {
        YandexGame.savesData.CurrentDice =0;
        _diceCounter = 0;
        _dicePrefab = _dicePrefabs[0];
    }
}
