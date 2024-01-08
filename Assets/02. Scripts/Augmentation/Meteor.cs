using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : Augmentation
{
    public float splashDamageRadius = 5f; // ���÷��� ���� �ݰ�
	public GameObject meteorPrefab; // ���׿� ������
    private Collider[] enemies; // ��� ���� ������ �迭

    public Meteor(PlayableCtrl player, int level, AugmentationEventType eventType) : base(player, level, eventType)
	{

	}

	public override void AugmentationEffect(Entity sender, EventArgs e)
	{
        
	}

	private IEnumerator AttackCoroutine(Vector3 targetPosition)
    {

        while (true)
        {
            yield return new WaitForSeconds(6f);


            Collider[] enemies = Physics.OverlapSphere(targetPosition, splashDamageRadius, 1 << LayerMask.NameToLayer("ENEMY"));

            if(enemies.Length > 0)
			{
                int tryCnt = 0;
				while (tryCnt < 100)
				{
                    int randomEnemyIdx = UnityEngine.Random.Range(0, enemies.Length);
                    if (Util.IsTargetInSight(Camera.main.transform, enemies[randomEnemyIdx].transform, Camera.main.fieldOfView))
                    {
                        LaunchMeteorAttack(enemies[randomEnemyIdx].transform.position);
                        break;
                    }
                    else
                        tryCnt++;
				}
			}
        }
    }

    private IEnumerator delay()
    {
        yield return new WaitForSeconds(2f);
    }

    void LaunchMeteorAttack(Vector3 targetPosition)
    {

        // ���׿� �������� �����Ͽ� Ÿ�� ��ġ�� ����߸�
        GameObject meteor = UnityEngine.Object.Instantiate(meteorPrefab, targetPosition, Quaternion.identity);

        

        // ���÷��� ���� �ݰ� ���� ������ ���ظ� ��
        Collider[] colliders = Physics.OverlapSphere(targetPosition, splashDamageRadius); 

        foreach (var enemy in UnityEngine.Object.FindObjectsOfType<EnemyCtrl>())
        {
            enemy.TakeDamage(enemy.hp);
        }

        // 2�� �Ŀ� ���׿� ����
        UnityEngine.Object.Destroy(meteor, 2f);
    }
}
