using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitShooting : Augmentation
{
	public int lvl;

	public SplitShooting(PlayableCtrl player, int level, AugmentationEventType eventType) : base(player, level, eventType)
	{
		// bullet ���͹� , bullet num
	}

	public override void AugmentationEffect(Entity sender, EventArgs e)
	{
		
	}

	public void Update()
	{

	}	

	private void Attack()
	{
		switch (lvl)
		{
			case 1:
				//GameObject bulletL = UnityEngine.Object.Instantiate()

				break;
			case 2:

				break;
			case 3:

				break;
			case 4:

				break;
			case 5:

				break;
		}
	}

}
