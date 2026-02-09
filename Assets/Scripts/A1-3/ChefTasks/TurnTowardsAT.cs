using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


namespace NodeCanvas.Tasks.Actions {

	public class TurnTowardsAT : ActionTask {

        public BBParameter<Transform> targetBBP;

        public float tolerance = 2f;

		private Variable<float> turnSpeedVar;
        private float targetAngle;
        private int direction;

        private Blackboard agentBlackboard;

        protected override string OnInit()
        {
            agentBlackboard = agent.GetComponent<Blackboard>();
            if (!agentBlackboard)
                return $"Entity '{agent.name}' does not have a Blackboard component!";

            turnSpeedVar = agentBlackboard.GetVariable<float>("turnSpeed");
            if (turnSpeedVar == null)
                return $"float turnSpeed on Blackboard on '{agent.name}' was expected!";

            return null;
		}

        protected override void OnExecute()
        {
            Vector3 target = targetBBP.value.position - agent.transform.position;
            target.y = 0;
            Vector3 forward = agent.transform.forward;
            forward.y = 0;

            direction = Mathf.RoundToInt(Mathf.Sign(Vector3.Angle(forward, target)));
        }


		protected override void OnUpdate() {
            Vector3 rotation = agent.transform.eulerAngles;
            rotation.y += direction * turnSpeedVar.value * Time.deltaTime;
            agent.transform.eulerAngles = rotation;

            Vector3 target = targetBBP.value.position - agent.transform.position;
            target.y = 0;
            Vector3 forward = agent.transform.forward;
            forward.y = 0;
            if (Vector3.Angle(forward, target) < tolerance)
                EndAction(true);
            
        }
	}
}