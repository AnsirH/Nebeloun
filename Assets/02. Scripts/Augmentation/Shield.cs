using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Augmentation
{
	public int maxShield = 1;
	public int curShield = 0;
	public bool isHit = true;
	public GameObject shield;

	public Shield(int level, AugmentationEventType eventType) : base(level, eventType)
	{
		// 1. �ǵ� �̹��� �޾ƿ���
		// 2. ���ʹ� ���� ���� �޾ƿ���
		
	}

	public override void AugmentationEffect(Entity sender, AugEventArgs e) 
	{
		CoroutineHandler.StartCoroutine(NumberOfShields());

		if(shield == null)
		{
			e.eventTr.Find("PlayerCanvas").Find("Shield");
		}

		if (isHit) // �ǰ� ��
		{
			curShield -= 1;
			// ���� �߰�
			e.target.AddEffect(new Invincble(Time.deltaTime));
			if (curShield == 0)
			{
				shield.gameObject.SetActive(false);
			}
		}
	}

	private IEnumerator NumberOfShields()
	{
		yield return new WaitForSeconds(60f);

		switch (level)
		{
			case 1:
				maxShield = 1;
				break;
			case 2:
				maxShield = 2;
				break;
			case 3:
				maxShield = 3;
				break;
			case 4:
				maxShield = 4;
				break;
			case 5:
				maxShield = 5;
				break;
		}

		curShield++;

		if (curShield >= maxShield)
			curShield = maxShield;
	}

}
