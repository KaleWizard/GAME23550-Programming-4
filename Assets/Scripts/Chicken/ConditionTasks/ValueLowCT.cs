using NodeCanvas.Framework;
using ParadoxNotion.Design;


namespace NodeCanvas.Tasks.Conditions {

	public class ValueLowCT : ConditionTask {

		ChickenProperties chicken;

		BBParameter<ChickenProperties.HealhProperties> type;

		protected override string OnInit(){
			chicken = agent.GetComponent<ChickenProperties>();
			if (!chicken)
				return $"Agent {agent.name} does not have a ChickenProperties script attached!";

			return null;
		}

		protected override bool OnCheck() {
			switch (type.value)
			{
				case ChickenProperties.HealhProperties.Satiety:
					return chicken.satiety < chicken.minSatiety;

				case ChickenProperties.HealhProperties.Energy:
					return chicken.energy < chicken.minEnergy;

                case ChickenProperties.HealhProperties.EggTimer:
					return chicken.eggTimer < 0;

                default:
					return false;
            }
		}
	}
}