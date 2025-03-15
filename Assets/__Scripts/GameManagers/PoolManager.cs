using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static List<PooledObjectInfo> ObjectPools = new List<PooledObjectInfo>();


    private GameObject _objectPoolEmptyHolder;

    private static GameObject _projectilesSystemEmpty;
    private static GameObject _diamondsSystemEmpty;
    private static GameObject _enemiesSystemEmpty;



    public enum PoolType
    {
        Projectiles,
        Diamonds,
        Enemies,
        None
    }

    public static PoolType PoolngType;


    private void Awake()
    {
        SetupEmpties();
    }

    private void SetupEmpties()
    {
        _objectPoolEmptyHolder = new GameObject("Pooled Objects");

        _projectilesSystemEmpty = new GameObject("Projectiles");
        _projectilesSystemEmpty.transform.SetParent(_objectPoolEmptyHolder.transform);

        _enemiesSystemEmpty = new GameObject("Enemies");
        _enemiesSystemEmpty.transform.SetParent(_objectPoolEmptyHolder.transform);

        _diamondsSystemEmpty = new GameObject("Diamonds");
        _diamondsSystemEmpty.transform.SetParent(_objectPoolEmptyHolder.transform);
    }

    public static GameObject SpawnObject(GameObject objectToSpawn, Transform initialState, PoolType poolType = PoolType.None)
    {
        PooledObjectInfo info = ObjectPools.Find(p => p.LookupString == objectToSpawn.name);

        if (info == null)
        {
            info = new PooledObjectInfo() { LookupString = objectToSpawn.name };
            ObjectPools.Add(info);
        }

        GameObject spawnableObject = info.InactiveObjects.FirstOrDefault();

        if (spawnableObject == null)
        {
            spawnableObject = Instantiate(objectToSpawn, initialState.position, initialState.rotation);
            
            Transform parent = SetParentObject(poolType);

            parent = parent == null ? initialState : parent;

            spawnableObject.transform.SetParent(parent.transform);
        }
        else
        {
            spawnableObject.transform.position = initialState.position;
            spawnableObject.transform.rotation = initialState.rotation;
            info.InactiveObjects.Remove(spawnableObject);
            spawnableObject.SetActive(true);
        }

        return spawnableObject;
    }



    //public static GameObject SpawnObject(GameObject objectToSpawn, Transform parentTranform)
    //{
    //    PooledObjectInfo info = ObjectPools.Find(p => p.LookupString == objectToSpawn.name);

    //    if (info == null)
    //    {
    //        info = new PooledObjectInfo() { LookupString = objectToSpawn.name };
    //        ObjectPools.Add(info);
    //    }

    //    GameObject spawnableObject = info.InactiveObjects.FirstOrDefault();

    //    if (spawnableObject == null)
    //    {
    //        spawnableObject = Instantiate(objectToSpawn, parentTranform);
    //    }
    //    else
    //    {
    //        info.InactiveObjects.Remove(spawnableObject);
    //        spawnableObject.SetActive(true);
    //    }

    //    return spawnableObject;
    //}


    private static Transform SetParentObject(PoolType poolType)
    {
        switch (poolType)
        {
            case PoolType.Projectiles:
                return _projectilesSystemEmpty.transform;

            case PoolType.Enemies:
                return _enemiesSystemEmpty.transform;

            case PoolType.Diamonds:
                return _diamondsSystemEmpty.transform;

            case PoolType.None:
                return null;

            default:
                return null;
        }
    }


    public static void ReturnObjectToPool(GameObject obj)
    {
        string goName = obj.name.Substring(0, obj.name.Length - 7); //Удаляем приставку (clone) у имени объекта

        PooledObjectInfo info = ObjectPools.Find(p => p.LookupString == goName);

        if (info == null)
        {
            Debug.LogWarning("Пытаешься удалить объект, которого не было в пуле!!!");
        }
        else
        {
            obj.SetActive(false);
            info.InactiveObjects.Add(obj);
        }
    }

}

public class PooledObjectInfo
{
    public string LookupString;
    public List<GameObject> InactiveObjects = new List<GameObject>();
}
