using NodeCanvas.Framework;
using ParadoxNotion.Design;


namespace NodeCanvas.Tasks.Conditions {

	public class ClaimAvailableCT : ConditionTask
    {
        ChickenProperties chicken;

        BBParameter<ChickenProperties.ClaimTypes> type;

        protected override string OnInit()
        {
            chicken = agent.GetComponent<ChickenProperties>();
            if (!chicken)
                return $"Agent {agent.name} does not have a ChickenProperties script attached!";

            return null;
        }

        protected override bool OnCheck() {
			switch (type.value)
            {
                case ChickenProperties.ClaimTypes.Trough:
                    return TroughClaimManager.Instance.ClaimAvailable();

                case ChickenProperties.ClaimTypes.Nest:
                    return NestClaimManager.Instance.ClaimAvailable();

                default:
                    return false;
            }
		}
	}
}