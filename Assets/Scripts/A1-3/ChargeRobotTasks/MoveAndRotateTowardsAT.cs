using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class MoveAndRotateTowardsAT : ActionTask {

		public BBParameter<GameObject> target;

        private Variable<float> moveSpeed;
        private Variable<float> turnSpeed;
        private Variable<float> stoppingDistance;

		private Blackboard agentBlackboard;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			agentBlackboard = agent.GetComponent<Blackboard>();

			if (agentBlackboard == null)
				return $"MoveAndRotateTowardsAT - {agent.name}: Unable to get Blackboard reference!";

            moveSpeed = agentBlackboard.GetVariable<float>("moveSpeed");
            turnSpeed = agentBlackboard.GetVariable<float>("turnSpeed");
            stoppingDistance = agentBlackboard.GetVariable<float>("stoppingDistance");

            return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {

			Vector3 direction = target.value.transform.position - agent.transform.position;
			Quaternion rotation = Quaternion.LookRotation(direction);

			agent.transform.SetPositionAndRotation(
				agent.transform.position + moveSpeed.value * Time.deltaTime * agent.transform.forward,
				Quaternion.RotateTowards(agent.transform.rotation, rotation, turnSpeed.value * Time.deltaTime)
				);

			if (Vector3.Distance(agent.transform.position, target.value.transform.position) < stoppingDistance.value)
			{
				EndAction(true);
			}
		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}