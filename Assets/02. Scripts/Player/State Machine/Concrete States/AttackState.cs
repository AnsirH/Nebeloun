using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : PlayerAttackState
{
    private float timer;

    private float exitTimer;
    private float timeTillAttackExit = 1f;

    public AttackState(PlayerAttack player, PlayerAttackStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void AnimationTriggerEvent()
    {
        base.AnimationTriggerEvent();
    }

    public override void EnterState()
    {
        timer = 0;
        exitTimer = 0;
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        // ������Ʈ �������� 100 �̻��� �� OverHit State�� ��ȯ
        if (player.OverHitGauge >= 100f)
        {
            player.AttackStateMachine.ChangeState(player.OverHitState);
            Debug.Log($"<color=red>Over Hit!</color> {player.overHitTime}�ʰ� Over Hit ���� ����.");
        }

        // ������ ����� ���� ��
        if (player.GetNearestTarget() != null)
        {
            timer += Time.deltaTime;

            if (timer > player.attackDelay)
            {
                timer = 0;

                Vector3 direction = player.GetNearestTarget().position - player.transform.position;

                GameObject.Instantiate<Rigidbody>(player.BulletPrefab, player.transform.position, Quaternion.identity).velocity = direction * 10f;

                player.OverHitGauge += 5f;
                Debug.Log($"<color=red>Over Hit Gauge</color> : {player.OverHitGauge}");
            }
        }

        // ������ ����� ���� ��
        else
        {
            exitTimer += Time.deltaTime;

            // ���� �ð��� ������ Idle State�� ��ȯ
            if (exitTimer > timeTillAttackExit)
            {
                exitTimer = 0;
                player.AttackStateMachine.ChangeState(player.IdleState);
            }
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
