using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritBar : MonoBehaviour
{
    [Header("ü�¹�")]
    public GaugeBar hpBar;

    [Header("Ÿ��")]
    public Transform target;

    RectTransform rectTrf;

    private void Awake()
    {
        rectTrf = GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        rectTrf.position = RectTransformUtility.WorldToScreenPoint(Camera.main, target.position);
    }
}
