using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class TightropeWalkAT : ActionTask
    {
		public BBParameter<float> walkSpeed;
        public BBParameter<float> walkInstability;
        public BBParameter<float> maxRotation;

		public BBParameter<float> maxInstabilityChange;

		public BBParameter<float> mouseBalanceForce;

        Variable<float> balanceVar;
		Variable<AnimationCurve> instabilityCurve;

        Blackboard agentBlackboard;

		Vector3 direction;

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

            instabilityCurve = blackboard.GetVariable<AnimationCurve>("instabilityCurve");
            if (balanceVar == null)
                return $"No AnimationCurve 'instabilityCurve' was found on entity {agent.name}";

            return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute()
        {
            Transform ropeStart = agentBlackboard.GetVariableValue<Transform>("ropeStart");
            Transform ropeEnd = agentBlackboard.GetVariableValue<Transform>("ropeEnd");

            direction = (ropeEnd.position - ropeStart.position).normalized;

			balanceVar.value = Random.Range(0f, 1f) > 0.5f ? 0.01f : -0.01f;
        }

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			Walk();
			Tilt();
		}

		private void Walk()
        {
            float movementInput = Input.GetAxis("Vertical");

            Vector3 movement = (movementInput * Time.deltaTime * walkSpeed.value) * direction;

            agent.transform.position += movement;
        }

		private void Tilt()
		{
			// Get instability value relative to current balance value
			float deltaBalance = instabilityCurve.value.Evaluate(balanceVar.value);
			// Increase by current movement input amount
			deltaBalance *= 1 + (Mathf.Abs(Input.GetAxis("Vertical")) * walkInstability.value + 1) / (walkInstability.value + 1);
            // Map to maximum change in instability
            deltaBalance *= maxInstabilityChange.value;
			// Ensure deltaBalance is smooth across framerates
			deltaBalance *= Time.deltaTime;
			// Finally update balance value
			balanceVar.value += Mathf.Sign(balanceVar.value) * deltaBalance;

            // Apply horizontal mouse input
            balanceVar.value -= Input.mousePositionDelta.x * mouseBalanceForce.value * Time.deltaTime;

            agent.transform.eulerAngles = Vector3.forward * maxRotation.value * balanceVar.value;
        }

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}