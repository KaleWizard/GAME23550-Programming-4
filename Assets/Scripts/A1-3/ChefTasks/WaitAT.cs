using NodeCanvas.Framework;
using ParadoxNotion.Design;


namespace NodeCanvas.Tasks.Actions {

	public class WaitAT : ActionTask {

		public BBParameter<float> waitTimeBBP;

		protected override void OnUpdate() {
            if (elapsedTime > waitTimeBBP.value) EndAction(true);
		}
	}
}