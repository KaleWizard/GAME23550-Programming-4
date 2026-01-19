using NodeCanvas.Framework;
using ParadoxNotion.Design;


namespace NodeCanvas.Tasks.Conditions {

	public class TightropeFallCT : ConditionTask {

		Variable<float> stabilityVar;

		Blackboard agentBlackboard;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit(){
			agentBlackboard = agent.GetComponent<Blackboard>();
			if (!agentBlackboard)
				return $"{agent.name} does not have a Blackboard attached!";

			stabilityVar = blackboard.GetVariable<float>("stability");
			if (stabilityVar == null)
				return $"No float 'stability' was found on entity {agent.name}";

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
			return stabilityVar.value > 1 || stabilityVar.value < -1;
		}
	}
}