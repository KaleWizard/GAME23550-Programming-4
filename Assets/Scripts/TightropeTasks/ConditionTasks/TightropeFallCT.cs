using NodeCanvas.Framework;
using ParadoxNotion.Design;


namespace NodeCanvas.Tasks.Conditions {

	public class TightropeFallCT : ConditionTask {

		Variable<float> balanceVar;

		Blackboard agentBlackboard;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit(){
			agentBlackboard = agent.GetComponent<Blackboard>();
			if (!agentBlackboard)
				return $"{agent.name} does not have a Blackboard attached!";

            balanceVar = blackboard.GetVariable<float>("balance");
			if (balanceVar == null)
				return $"No float 'balance' was found on entity {agent.name}";

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
			return balanceVar.value > 1 || balanceVar.value < -1;
		}
	}
}