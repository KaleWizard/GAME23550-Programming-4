using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Conditions {

	public class SpeedThresholdCT : ConditionTask {

		public enum Type { GreaterThan, LessThan }
		
		public BBParameter<float> threshold;
		public BBParameter<Type> type;

		Blackboard agentBlackboard;

		Variable<float> speedVar;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit()
        {
            agentBlackboard = agent.GetComponent<Blackboard>();
            if (!agentBlackboard)
                return $"{agent.name} does not have a Blackboard attached!";

			speedVar = agentBlackboard.GetVariable<float>("speed");
            if (speedVar == null)
                return $"No float 'speed' was found on entity {agent.name}";

            return null;
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
			bool greater = Mathf.Abs(speedVar.value) > threshold.value;
			switch (type.value) {
				case Type.GreaterThan:
					return greater;
				case Type.LessThan:
					return !greater;
			}
			return false;
		}
	}
}