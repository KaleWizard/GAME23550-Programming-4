using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class TightropeIdleAT : ActionTask {

        Blackboard agentBlackboard;

		public BBParameter<float> lookSensitivity;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit()
        {
            agentBlackboard = agent.GetComponent<Blackboard>();
            if (!agentBlackboard)
                return $"{agent.name} does not have a Blackboard attached!";

			if (lookSensitivity == null)
			{
				lookSensitivity = new BBParameter<float>();
				lookSensitivity.value = 1f;
			}
            
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			float xInput = Input.mousePositionDelta.x;

			Vector3 rotation = agent.transform.eulerAngles;
			rotation.y += xInput * lookSensitivity.value;
			agent.transform.eulerAngles = rotation;
		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}