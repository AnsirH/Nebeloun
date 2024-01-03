using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour 
{
    // Ǯ���� ������Ʈ ������
    [SerializeField]
    private Poolable poolObj;

    // �ʱ⿡ �Ҵ��� ������Ʈ ��
    [SerializeField]
    private int allocateCount;

    // ������Ʈ�� �־�� ����
    private Stack<Poolable> poolStack = new Stack<Poolable>();

    private void Start()
    {
        Allocate(allocateCount);
    }

    /// <summary>
    /// �Ҵ�
    /// </summary>
    /// <param name="allocateCount">�Ҵ� ����</param>
    public void Allocate(int allocateCount)
    {
        for (int i = 0; i < allocateCount; i++)
        {
            Poolable allocateObj = Instantiate(poolObj, transform);
            allocateObj.Create(this);
            poolStack.Push(allocateObj);
        }
    }

    public GameObject Pop()
    {
        Poolable obj = poolStack.Pop();
        obj.gameObject.SetActive(true);

        // Ǯ�� ������Ʈ�� 3������ ������ 5�� �߰� �Ҵ�
        if (poolStack.Count < 3)
        {
            Allocate(5);
        }
        return obj.gameObject;
    }

    public void Push(Poolable obj)
    {
        obj.gameObject.SetActive(false);
        poolStack.Push(obj);
    }
}
