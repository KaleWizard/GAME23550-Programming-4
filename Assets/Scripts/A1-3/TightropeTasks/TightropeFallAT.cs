using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class TightropeFallAT : ActionTask
    {
		public BBParameter<float> fallSpeed;
		public BBParameter<float> rotationSpeed;

        Blackboard agentBlackboard;

		Variable<float> balanceVar;

		bool fallLeft;

		float speed;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit()
        {
            agentBlackboard = agent.GetComponent<Blackboard>();
            if (!agentBlackboard)
                return $"{agent.name} does not have a Blackboard attached!";

            balanceVar = blackboard.GetVariable<float>("balance");
            if (balanceVar == null)
                return $"No float 'balance' was found on entity {agent.name}";

            return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			fallLeft = balanceVar.value > 0;
			speed = 0;
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			// Move player down
			speed = Mathf.Clamp(speed - 0.25f * Time.deltaTime, -fallSpeed.value, 0);
			agent.transform.position += Vector3.up * speed;
			// Rotate player
			Vector3 rotation = agent.transform.eulerAngles;
			rotation.z += (fallLeft ? 1 : -1) * rotationSpeed.value * Time.deltaTime;
			rotation.y += rotationSpeed.value * 0.25f * Time.deltaTime;
			agent.transform.eulerAngles = rotation;

			if (elapsedTime > 10) EndAction(true);
		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}