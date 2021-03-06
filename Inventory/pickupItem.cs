// (c) Eric Vander Wal, 2017 All rights reserved.
// Custom Action by DumbGameDev
// www.dumbgamedev.com

using UnityEngine;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace Opsive.ThirdPersonController.ThirdParty.PlayMaker
{
	[ActionCategory("Third Person Controller Inventory")]
	[Tooltip("Pickup an item using the item ID.")]

	public class  pickupItem : FsmStateAction
	{

		[RequiredField]
		[CheckForComponent(typeof(Inventory))]
		[Tooltip("Select the current character setup with the third person controller.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Check this box to preform this action every frame.")]
		public FsmBool everyFrame;

		[Tooltip("ID of item to pickupo.")]
		public FsmInt itemID;
		[Tooltip("Amount of item to pickup.")]
		public FsmInt amount;
		[Tooltip("Should the item be equipt.")]
		public FsmBool shouldEquipt;
		[Tooltip("Only applies to primary item types. If flase the item will be added with an animation.")]
		public FsmBool activateImmediately;

		Inventory invScript;

		public override void Reset()
		{

			itemID = null;
			amount = null;
			shouldEquipt = false;
			activateImmediately = false;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			invScript = go.GetComponent<Inventory>();

			if (!everyFrame.Value)
			{
				DoScript();
				Finish();
			}

		}

		public override void OnUpdate()
		{
			if (everyFrame.Value)
			{
				DoScript();
			}
		}

		void DoScript()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				return;
			}

			invScript.PickupItem(itemID.Value, amount.Value, shouldEquipt.Value, activateImmediately.Value);
		}

	}
}