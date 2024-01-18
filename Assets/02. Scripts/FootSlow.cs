using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using JetBrains.Annotations;

public class FootSlow : Augmentation
{
    private float lastActionTime;
    private float delay;

    private float duration;
    private float speedDownPercent = 0.2f;

    private Dictionary<Entity, float> changedEntities = new Dictionary<Entity, float>();
    
    public FootSlow(int level, int maxLevel) : base(level, maxLevel)
    {
        lastActionTime = Time.time; // ���� �ð� ���
        delay = 0f;     // ������ 0
        duration = 2.0f;   // ���ӽð� 2
    }

    protected override AugmentationEventType GetEventType()
    {
        return AugmentationEventType.ON_START;
    }

    public override void AugmentationEffect(Entity sender, AugEventArgs e)
    {
        Debug.Log("���� ����");


        //// �����̸�ŭ ���
        //if (lastActionTime + delay > Time.time)
        //{
        //    return;
        //}
        //// �ð� �ʱ�ȭ
        //lastActionTime = Time.time;

        // ���ο� �ۼ�Ʈ
        speedDownPercent = float.Parse(GameManager.instance.augTable[level]["FootSlow"].ToString());

        CoroutineHandler.StartCoroutine(Action(sender));
    }


    private IEnumerator Action(Entity sender)
    {
        float radius = 2f;
        float delayTimer = 0;
        Vector3 pos = sender.transform.position;
        
        for (float durationTimer = 0; durationTimer < duration; durationTimer += Time.deltaTime)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            Debug.Log("������");
            delayTimer += Time.deltaTime;
            if (delayTimer >= 1)
            {
                delayTimer = 0f;
                pos = sender.transform.position;
                foreach (var entity in changedEntities)
                {
                    entity.Key.stat.Add(StatType.MOVE_SPEED, entity.Value); 
                    entity.Key.stat.UpdateStat();
                }
                changedEntities.Clear();
            }
            
            Collider[] colliders;
            colliders = Physics.OverlapSphere(pos, radius, 1 << LayerMask.NameToLayer("ENEMY"));
            foreach (Collider collider in colliders)
            {
                Entity entity = collider.GetComponent<Entity>();
                if (entity)
                {
                    if (!changedEntities.ContainsKey(entity))
                    {
                        Stat eneityStat = entity.stat;
                        float value = eneityStat.Get(StatType.MOVE_SPEED) * speedDownPercent;
                        changedEntities.Add(entity, value);
                        eneityStat.Add(StatType.MOVE_SPEED, -value);
                        eneityStat.UpdateStat();
                    }
                }
            }
        }
    }
}