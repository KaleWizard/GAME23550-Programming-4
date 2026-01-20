using NodeCanvas.Framework;
using ParadoxNotion.Design;


namespace NodeCanvas.Tasks.Actions {

	public class BoostAT : ActionTask {

        public float boostValue;

        Blackboard agentBlackboard;

        Variable<float> scanRadiusVar;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit()
        {
            agentBlackboard = agent.GetComponent<Blackboard>();
            if (!agentBlackboard)
                return $"No Blackboard component found on entity {agent.name}";

            scanRadiusVar = agentBlackboard.GetVariable<float>("scanRadius");
            if (scanRadiusVar == null)
                return $"No float 'scanRadius' was found on Blackboard on entity {agent.name}";

            return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
            scanRadiusVar.value += boostValue;

			EndAction(true);
		}
	}
}