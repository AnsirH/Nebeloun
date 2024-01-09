using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackShot : Augmentation
{
    public float knockbackForce = 0.2f; // �˹� ��
    public int lvl = 0;

    public KnockbackShot(PlayableCtrl player, int level, AugmentationEventType eventType) : base(player, level, eventType)
	{

	}
	public override void AugmentationEffect(Entity sender, EventArgs e)
	{

	}


    public void Knockback(Vector3 direction)
    {
        Rigidbody enemyRigidbody = player.GetComponent<Rigidbody>();

        if (enemyRigidbody != null)
        {
            // �������� ���� ���� �˹��� �߻���Ŵ
            enemyRigidbody.AddForce(direction * knockbackForce, ForceMode.Impulse);
            // ������ ���� �� ���� ��� (���� ���, �÷��̾ �ٶ󺸴� ���� ��)
            Vector3 attackDirection = player.transform.forward;
        }

        switch (lvl)
		{
            case 1:
                knockbackForce = 0.2f; 
                break;
            case 2:
                knockbackForce = 0.4f;
                break;
            case 3:
                knockbackForce = 0.6f;
                break;
            case 4:
                knockbackForce = 0.8f;
                break;
            case 5:
                knockbackForce = 1f;
                break;
        }
    }

}
