using NodeCanvas.Framework;
using ParadoxNotion.Design;


namespace NodeCanvas.Tasks.Conditions {

	public class OutOfFoodCT : ConditionTask {

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

		protected override bool OnCheck() {
			return foodLeftVar.value == 0;
		}
	}
}