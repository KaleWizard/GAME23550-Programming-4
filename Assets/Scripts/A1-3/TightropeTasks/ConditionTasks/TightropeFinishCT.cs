using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Conditions {

	public class TightropeFinishCT : ConditionTask {

		Blackboard agentBlackboard;

		float startPosZ;
		float totalDistanceZ;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit()
        {
            agentBlackboard = agent.GetComponent<Blackboard>();
            if (!agentBlackboard)
                return $"{agent.name} does not have a Blackboard attached!";

            Transform ropeStart = agentBlackboard.GetVariableValue<Transform>("ropeStart");
            Transform ropeEnd = agentBlackboard.GetVariableValue<Transform>("ropeEnd");

            DoSetup(ropeStart.position, ropeEnd.position);

            return null;
		}

		private void DoSetup(Vector3 ropeStart, Vector3 ropeEnd)
		{
			startPosZ = ropeStart.z;
			totalDistanceZ = Mathf.Abs(ropeEnd.z - ropeStart.z);
		}

		//Called whenever the condition gets enabled.
		protected override void OnEnable() {
			
		}

		//Called whenever the condition gets disabled.
		protected override void OnDisable() {
			
		}

		//Called once per frame while the condition is active.
		//Return whether the condition is success or failure.
		protected override bool OnCheck() {
			float progression = Mathf.Abs(agent.transform.position.z - startPosZ);

			return progression > totalDistanceZ;
		}
	}
}