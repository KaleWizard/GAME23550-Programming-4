using NodeCanvas.Framework;
using ParadoxNotion.Design;


namespace NodeCanvas.Tasks.Conditions {

	public class DogNoticedCT : ConditionTask
    {

        ChickenProperties chicken;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit()
        {
            chicken = agent.GetComponent<ChickenProperties>();
            if (!chicken)
                return $"Agent {agent.name} does not have a ChickenProperties script attached!";

            return null;
        }

        //Called whenever the condition gets enabled.
        protected override void OnEnable() {
			
		}

		//Called whenever the condition gets disabled.
		protected override void OnDisable() {
			
		}

		//Called once per frame while the condition is active.
		//Return whether the condition is success or failure.
		protected override bool OnCheck() {
			return true;
		}
	}
}