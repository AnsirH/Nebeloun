using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������Ʈ Ǯ���� ������ ������Ʈ
/// </summary>
public class Poolable : MonoBehaviour
{
    // �Ҵ�� ������Ʈ Ǯ
    protected ObjectPool pool;

    // ������ ȣ���� �޼���
    public virtual void Create(ObjectPool pool)
    {
        this.pool = pool;
        gameObject.SetActive(false);
    }

    // ������Ʈ Ǯ�� Push
    public virtual void Push()
    {
        pool.Push(this);
    }
}
