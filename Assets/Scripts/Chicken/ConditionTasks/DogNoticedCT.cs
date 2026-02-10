using NodeCanvas.Framework;
using ParadoxNotion.Design;


namespace NodeCanvas.Tasks.Conditions {

	public class DogNoticedCT : ConditionTask
    {
        ChickenProperties chicken;

        protected override string OnInit()
        {
            chicken = agent.GetComponent<ChickenProperties>();
            if (!chicken)
                return $"Agent {agent.name} does not have a ChickenProperties script attached!";

            return null;
        }

        protected override bool OnCheck() {
			return chicken.DogNear;
		}
	}
}