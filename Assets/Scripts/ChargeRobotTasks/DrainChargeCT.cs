using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Conditions {

	public class DrainChargeCT : ConditionTask
    {

        private Blackboard agentBlackboard;

        private Variable<float> currentCharge;
        private Variable<float> chargeLossRate;
        private Transform chargeStation;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit()
        {
            agentBlackboard = agent.GetComponent<Blackboard>();

            if (agentBlackboard == null)
                return $"LoseChargeAT - {agent.name}: Unable to get Blackboard reference!";

            currentCharge = agentBlackboard.GetVariable<float>("currentCharge");
            chargeLossRate = agentBlackboard.GetVariable<float>("chargeLossRate");
            
			return null;
		}

		//Called whenever the condition gets enabled.
		protected override void OnEnable() {
            chargeStation = agentBlackboard.GetVariableValue<Transform>("chargingStation");
        }

		//Called whenever the condition gets disabled.
		protected override void OnDisable() {
			
		}

		//Called once per frame while the condition is active.
		//Return whether the condition is success or failure.
		protected override bool OnCheck() {
            currentCharge.value -= chargeLossRate.value * Time.deltaTime;

            if (currentCharge.value <= 0)
            {
                agentBlackboard.SetVariableValue("moveTarget", chargeStation.gameObject);
                return true;
            }

            return false;
		}
	}
}