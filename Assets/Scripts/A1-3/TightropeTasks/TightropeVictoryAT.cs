using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class TightropeVictoryAT : ActionTask
    {
		public BBParameter<float> walkSpeed;

        Blackboard agentBlackboard;

        Vector3 victoryPosition;
		Vector3 velocity;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit()
        {
            agentBlackboard = agent.GetComponent<Blackboard>();
            if (!agentBlackboard)
                return $"{agent.name} does not have a Blackboard attached!";

            

            return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
            Transform victoryPositionTransform = agentBlackboard.GetVariableValue<Transform>("playerEndVictory");
            victoryPosition = victoryPositionTransform.position;

            velocity = (victoryPosition - agent.transform.position).normalized * walkSpeed.value;

            // Reset player rotation
            agent.transform.eulerAngles = Vector3.zero;
        }

		//Called once per frame while the action is active.
		protected override void OnUpdate()
        {
            agent.transform.position += velocity * Time.deltaTime;

            bool arrived = Vector3.Dot(velocity, victoryPosition - agent.transform.position) < 0;

            if (arrived)
            {
                agent.transform.position = victoryPosition;
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