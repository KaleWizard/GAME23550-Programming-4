using NodeCanvas.Framework;
using ParadoxNotion.Design;
using ParadoxNotion.Services;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class PlayerMovementAT : ActionTask {

		private RigidbodyController rbController;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			rbController = blackboard.GetVariableValue<RigidbodyController>("rbController");
			if (!rbController) return "No RigidBodyController component on blackboard!";

			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute()
        {
            ////////////////////////////////////////////////////////////////////////////////////////////////
            /// This method of using MonoManager.current.onFixedUpdate was found via the following link: ///
			/// https://nodecanvas.paradoxnotion.com/documentation/?section=using-fixedupdate-and-ongui  ///
            ////////////////////////////////////////////////////////////////////////////////////////////////
            MonoManager.current.onFixedUpdate += FixedUpdate;
        }

		private void FixedUpdate()
		{
			Vector3 input = Vector3.zero;
            input.x = Input.GetAxisRaw("Horizontal");
            input.z = Input.GetAxisRaw("Vertical");

			rbController.MoveBody(input);
        }

		//Called when the task is disabled.
		protected override void OnStop()
        {
            MonoManager.current.onFixedUpdate -= FixedUpdate;
        }

		//Called when the task is paused.
		protected override void OnPause()
        {
            MonoManager.current.onFixedUpdate -= FixedUpdate;
        }
	}
}