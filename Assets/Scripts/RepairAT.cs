using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class RepairAT : ActionTask {

		public BBParameter<Transform> lightTowerTransformBBP;
        public BBParameter<float> initialScanRadiusBBP;
        public BBParameter<float> scanRadiusBBP;

		public float repairRate = 25;
		public float repairThreshold = 100;

		private Blackboard lightTowerBB;
		private float repairValue;

		protected override void OnExecute() {
			lightTowerBB = lightTowerTransformBBP.value.GetComponentInParent<Blackboard>();
			repairValue = lightTowerBB.GetVariableValue<float>("repairValue");
		}

		protected override void OnUpdate() {
			repairValue += repairRate * Time.deltaTime;
			if (repairValue >= repairThreshold) EndAction(true);
		}

		protected override void OnStop()
		{
			scanRadiusBBP.value = initialScanRadiusBBP.value;
			lightTowerBB.SetVariableValue("repairValue", repairValue);
		}
        
	}
}