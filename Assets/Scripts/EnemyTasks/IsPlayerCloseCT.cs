using NodeCanvas.Framework;
using UnityEngine;


namespace NodeCanvas.Tasks.Conditions {

	public class IsPlayerCloseCT : ConditionTask {

        private GameObject player;
        private RigidbodyController rbController;
        private EnemyData stats;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit()
        {
            player = blackboard.GetVariableValue<GameObject>("player");
            if (!player) return "No 'player' assigned in the blackboard!";

            rbController = blackboard.GetVariableValue<RigidbodyController>("rbController");
            if (!rbController) return "No RigidBodyController component on blackboard!";

            stats = blackboard.GetVariableValue<EnemyData>("enemyData");
            if (!stats) return "No 'enemyData' assigned on blackboard!";

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
			float distance = Vector3.Distance(agent.transform.position, player.transform.position);
			return distance < stats.killRange;
		}
	}
}