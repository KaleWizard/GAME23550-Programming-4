using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class WindProcessAT : ActionTask
    {
        public BBParameter<float> amplitude;
        public BBParameter<float> frequency;


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

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			float noise = (Mathf.PerlinNoise1D(frequency.value * Time.time) - 1) * 2;
            speedVar.value = amplitude.value * noise * noise;
		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}