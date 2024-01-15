using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolable
{
    // �Ҵ�� ������Ʈ Ǯ
    public Queue<GameObject> pool { get; set; }

    /// <summary>
    /// Ǯ���� ������Ʈ�� ���� ������ �� ������ �޼���
    /// </summary>
    /// <param name="pool">�Ҵ�� Ǯ</param>
    public abstract void Create(Queue<GameObject> pool);
    public abstract void ReturnObject();
}