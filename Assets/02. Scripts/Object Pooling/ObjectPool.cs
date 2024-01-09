using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public enum ObjectType
    {
        Enemy,
        Bullet
    }

    [System.Serializable]
    public struct Pool 
    {
        public ObjectType type;     // ������Ʈ Ÿ��
        public GameObject prefab;   // ������Ʈ ������
        public int size;
    }

    // Ǯ ����Ʈ
    public List<Pool> pools;

    public Dictionary<ObjectType, Stack<GameObject>> poolDictionary = new Dictionary<ObjectType, Stack<GameObject>>();


    // �ʱ⿡ �Ҵ��� ������Ʈ ��
    [SerializeField]
    private int allocateCount;

    private void Awake()
    {
        foreach (var pool in pools)
        {
            Stack<GameObject> objectPool = new Stack<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, transform);
                obj.SetActive(false);
                objectPool.Push(obj);
                obj.GetComponent<IPoolable>().Create(objectPool);
            }
            poolDictionary.Add(pool.type, objectPool);
        }
    }

    /// <summary>
    /// �Ҵ�
    /// </summary>
    /// <param name="allocateCount">�Ҵ� ����</param>
    public void Allocate(int count, ObjectType objectType)
    {
        foreach(var pool in pools)
        {
            if (pool.type != objectType)
            {
                continue;
            }
            for (int i = 0; i < count; i++)
            {
                GameObject obj = Instantiate(pool.prefab, transform);
                obj.SetActive(false);
                poolDictionary[pool.type].Push(obj);
            }
        }
    }

    public GameObject Pop(ObjectType objectType)
    {
        if (!poolDictionary.ContainsKey(objectType))
        {
            // ���ٸ� ���ο� Ǯ ����� �߰��� ��.
            return null;
        }

        GameObject obj;
        if (poolDictionary[objectType].TryPop(out obj))
        {
            obj.SetActive(true);
            return obj;
        }
        else
        {
            Allocate(5, objectType);
            return poolDictionary[objectType].Pop();
        }
    }

    public void Push(GameObject obj, ObjectType type)
    {
        obj.SetActive(false);
        poolDictionary[type].Push(obj);
    }
}
