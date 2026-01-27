using NodeCanvas.Framework;
using ParadoxNotion.Design;


namespace NodeCanvas.Tasks.Actions {

	public class UseFoodAT : ActionTask {

        public int foodCost = 1;
        private Variable<int> foodLeftVar;

        private Blackboard agentBlackboard;

        protected override string OnInit()
        {
            agentBlackboard = agent.GetComponent<Blackboard>();
            if (!agentBlackboard)
                return $"Entity '{agent.name}' does not have a Blackboard component!";

            foodLeftVar = agentBlackboard.GetVariable<int>("foodLeft");
            if (foodLeftVar == null)
                return $"int foodLeft on Blackboard on '{agent.name}' was expected!";

            return null;
        }

        protected override void OnExecute() {
            foodLeftVar.SetValue(foodLeftVar.value - foodCost);
			EndAction(true);
		}
	}
}