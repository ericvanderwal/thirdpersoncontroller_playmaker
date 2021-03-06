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
	[Tooltip("Get the current item or weapon gameobject and object.")]

	public class  getCurrentItemGameobject : FsmStateAction
	{

		public enum ItemType { Primary, Secondary, DualWield }

		[RequiredField]
		[CheckForComponent(typeof(Inventory))]
		[Tooltip("Select the current character setup with the third person controller.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The type of item to determine if it has any ammo")]
		[ObjectType(typeof(ItemType))]
		public FsmEnum itemType;

		public FsmGameObject currentItemGO;
		public FsmObject currentItemObject;

		[Tooltip("Check this box to preform this action every frame.")]
		public FsmBool everyFrame;

		private Item _currentItem;

		Inventory invScript;

		public override void Reset()
		{

			currentItemGO = null;
			currentItemObject = null;
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

			System.Type type = null;
			switch ((ItemType)itemType.Value) {
			case ItemType.Primary:
				type = typeof(PrimaryItemType);
				break;
			case ItemType.Secondary:
				type = typeof(SecondaryItemType);
				break;
			case ItemType.DualWield:
				type = typeof(DualWieldItemType);
				break;
			}

			_currentItem = invScript.GetCurrentItem(type);
			currentItemGO.Value = _currentItem.gameObject;
			currentItemObject.Value = _currentItem;
		}

	}
}