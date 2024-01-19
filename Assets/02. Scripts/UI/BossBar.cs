using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBar : MonoBehaviour
{
    [Header("ü�¹�")]
    public GaugeBar hpBar;

    [Header("Ÿ��")]
    public Transform target;

    [Header("���̵� �ƿ� �ӵ�"), SerializeField]
    float fadeSpeed = 2f;

    // ������Ʈ�� ���� ��� �̹���
    Image[] images;

    RectTransform rectTrf;

    // ���̵� �ƿ� ȿ�� �ڷ�ƾ
    Coroutine fadeEffectCoroutine;

    private void Awake()
    {
        images = GetComponentsInChildren<Image>();
        rectTrf = GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        rectTrf.position = RectTransformUtility.WorldToScreenPoint(Camera.main, target.position);
    }

    public void AllocateTarget(Transform target)
    {
        this.target = target;
        ActivateGaugeBar();
    }

    public void ActivateGaugeBar()
    {
        if (fadeEffectCoroutine != null)
        {
            StopCoroutine(fadeEffectCoroutine);
        }
        fadeEffectCoroutine = StartCoroutine(FadeGaugeBarEffect());
    }

    IEnumerator FadeGaugeBarEffect()
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].color = Color.white;
        }
        yield return new WaitForSeconds(5f);
        for (int i = 0; i < images.Length; i++)
        {
            images[i].CrossFadeColor(Color.clear, fadeSpeed, false, true);
        }
    }
}