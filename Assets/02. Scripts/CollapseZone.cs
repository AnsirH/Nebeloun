using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollapseZone : MonoBehaviour, IPoolable
{
    [Tooltip("�ر��� �ʴ� ������")]
    public float increment;
    [Tooltip("�ر��� ����"), Range(1f, 10f)]
    public float zoneRange;
    public float stablity { get; private set; }
    public Stack<GameObject> pool { get; set; }

    private Spirit spirit;
    private PlayableCtrl player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayableCtrl>();
        spirit = FindObjectOfType<Spirit>();
        spirit.collapseZone = this;
    }

    private void Update()
    {
        if (spirit == null)
            return;
        

        float value = 0;

        if (spirit.spiritState == SpiritState.OCCUPY)
        {
            if ((transform.position - spirit.transform.position).magnitude <= zoneRange)
                value += increment;
            if (player != null)
            {
                if ((transform.position - player.transform.position).magnitude <= zoneRange)
                    value += increment;
            }
        }

        stablity = Mathf.Clamp(stablity + (value * Time.deltaTime), 0, 100f);
        if (stablity >= 100)
        {
            Debug.Log("���� ����");
            spirit.SetBlessType();
            Push();
        }
        else
        {
            Debug.Log($"������ / {Mathf.Round(stablity)}");
        }
    }


    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, zoneRange);
        }
    }

    public void Create(Stack<GameObject> pool)
    {
        this.pool = pool;
    }

    public void Push()
    {
        gameObject.SetActive(false);

        pool?.Push(gameObject);
    }
}
