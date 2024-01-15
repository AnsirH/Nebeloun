using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AugmentationEventType
{
    NONE,
    ON_START,       //���� ȹ�� ��
    ON_UPDATE,      //������Ʈ����
    ON_ATTACK,      //���� ��
    ON_HIT,         //������ ����� ��
    ON_DAMAGE,      //����� �޾��� ��
    ON_SPAWN_ENEMY,
    ON_UPDATE_ENEMY
}


/// <summary>
/// Delegate for Augmentation
/// </summary>
/// <param name="sender">
/// sender: event invoker(default: player)
/// </param>
/// 
/// <param name="e">
/// 
/// e: Classes that inherit EventArgs
/// e.target: target that invoke event
/// eventType == ON_START (e.target: player, e.eventTr: player.transform)
/// eventType == ON_UPDATE: (e.target: player, e.eventTr: player.transform)
/// eventType == ON_ATTACK: (e.target: player, e.eventTr: player.transform)
/// eventType == ON_HIT: (e.target: damaged Enemy, e.eventTr: enemy.transform)
/// 
/// 
/// </param> 
/// 
public delegate void AugmentationDelegate(Entity sender, AugEventArgs e);

public abstract class Augmentation
{
    public AugmentationEventType eventType { 
        get 
        { 
            return GetEventType();
        }
        private set { eventType = value; } }
    protected abstract AugmentationEventType GetEventType();

    public int level { get; private set; }
    public int maxLevel { get; private set; }

    public Augmentation(int level, int maxLevel)
    {
        this.level = level;
        this.maxLevel = maxLevel;
    }

    public virtual void AugmentationEffect(Entity sender, AugEventArgs e)
    {

    }

    public void SetAugmentationLevel(int level) => this.level = level;

}

