using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class WindBlowAT : ActionTask {

		Blackboard agentBlackboard;

		Variable<float> speedVar;

		Blackboard playerBlackboard;
		Variable<float> playerBalanceVar;

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


            GameObject player = agentBlackboard.GetVariableValue<GameObject>("player");
			if (!player)
				return $"No GameObject 'player' was found on entity {agent.name}";

			playerBlackboard = player.GetComponent<Blackboard>();
			if (!playerBlackboard)
				return $"{player.name} does not have a Blackboard attached!";

			playerBalanceVar = playerBlackboard.GetVariable<float>("balance");
			if (playerBalanceVar == null)
                return $"No float 'balance' was found on entity {player.name}";

            return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			playerBalanceVar.value += speedVar.value * Time.deltaTime;
		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}