using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class MoveToAT : ActionTask {

		public BBParameter<Transform> targetBBP;

		private Variable<float> speedVar;
		private Vector3 direction;

		private Blackboard agentBlackboard;

		protected override string OnInit() {
			agentBlackboard = agent.GetComponent<Blackboard>();
			if (!agentBlackboard)
				return $"Entity '{agent.name}' does not have a Blackboard component!";

			speedVar = agentBlackboard.GetVariable<float>("speed");
			if (speedVar == null)
				return $"float speed on Blackboard on '{agent.name}' was expected!";

			return null;
		}

		protected override void OnExecute() {
			direction = (targetBBP.value.position - agent.transform.position).normalized;
		}

		protected override void OnUpdate() {
			agent.transform.position += direction * speedVar.value * Time.deltaTime;
			// Check for arrival
			if (Vector3.Dot(targetBBP.value.position - agent.transform.position, direction) < 0)
			{
				agent.transform.position = targetBBP.value.position;
				EndAction(true);
			}
			
        }
	}
}