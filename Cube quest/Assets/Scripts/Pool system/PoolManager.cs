using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static List<ObjectPool> ObjectPools = new();
    private static Transform _poolObjectsHolder;
    private void Awake()
    {
        _poolObjectsHolder = transform;
    }
    public static GameObject SpawnObject(GameObject ObjectToSpawn, Vector3 SpawnPosition, Quaternion SpawnRotation)
    {
        ObjectPool Pool = null;
        foreach(ObjectPool pool in ObjectPools)
        {
            if(pool.PoolName == ObjectToSpawn.name)
            {
                Pool = pool;
                break;
            }
        }

        if(Pool == null)
        {
            Pool = new ObjectPool() { PoolName = ObjectToSpawn.name };
            ObjectPools.Add(Pool);
        }


        GameObject SpawnableObject = Pool.InactiveObjects.FirstOrDefault();

        if(SpawnableObject == null)
        {
            SpawnableObject = Instantiate(ObjectToSpawn, SpawnPosition, SpawnRotation, _poolObjectsHolder);
        }
        else
        {
            SpawnableObject.transform.SetPositionAndRotation(SpawnPosition, SpawnRotation);
            Pool.InactiveObjects.Remove(SpawnableObject);
            SpawnableObject.SetActive(true);
        }
        return SpawnableObject;
    }
    public static void ReturnObjectToPool(GameObject ObjectToReturn)
    {
        string RealObjectName = ObjectToReturn.name.Substring(0, ObjectToReturn.name.Length - 7);
        ObjectPool Pool = null;
        foreach(ObjectPool pool in ObjectPools)
            if(pool.PoolName == RealObjectName)
            {
                Pool = pool;
                break;
            }
        if (Pool == null)
            Debug.LogWarning("There's no such pool");
        else
        {
            ObjectToReturn.SetActive(false);
            Pool.InactiveObjects.Add(ObjectToReturn);
        }
    }
}
public class ObjectPool
{
    public string PoolName;
    public List<GameObject> InactiveObjects = new();
}