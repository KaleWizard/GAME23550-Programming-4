using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class ToTightropeAT : ActionTask
    {
		public BBParameter<float> turnSpeed;
		public BBParameter<float> walkSpeed;

		public BBParameter<float> tolerance;

        Blackboard agentBlackboard;

		bool turned = false;

		Vector3 ropeStart;
		Vector3 velocity;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit()
        {
            agentBlackboard = agent.GetComponent<Blackboard>();
            if (!agentBlackboard)
                return $"{agent.name} does not have a Blackboard attached!";

			Transform ropeStartTransform = agentBlackboard.GetVariableValue<Transform>("ropeStart");
			ropeStart = ropeStartTransform.position;

			velocity = (ropeStart - agent.transform.position).normalized * walkSpeed.value;

            return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			turned = false;
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			if (!turned) // Turn
			{
				Vector3 rotation = agent.transform.eulerAngles;
				if (rotation.y > 180)
				{
					rotation.y += turnSpeed.value * Time.deltaTime;
                } else
				{
                    rotation.y -= turnSpeed.value * Time.deltaTime;
                }
				agent.transform.eulerAngles = rotation;

				if (Mathf.Abs(rotation.y) < tolerance.value)
				{
					agent.transform.eulerAngles = Vector3.zero;
					turned = true;
				}

			} else // Walk
			{
				agent.transform.position += velocity * Time.deltaTime;

				bool arrived = Vector3.Dot(velocity, ropeStart - agent.transform.position) < 0;

				if (arrived)
				{
					agent.transform.position = ropeStart;
                    EndAction(true);
                }
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