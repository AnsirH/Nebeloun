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

    /// <summary>
    /// Ǯ���� ������Ʈ�� ���� ������ �� ������ �޼���
    /// </summary>
    /// <param name="pool">�Ҵ�� Ǯ ������Ʈ</param>
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
