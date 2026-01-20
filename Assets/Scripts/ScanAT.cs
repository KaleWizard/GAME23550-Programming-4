using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class ScanAT : ActionTask {
		public Color scanColour;
		public int numberOfScanCirclePoints;

		public BBParameter<Transform> lightTowerTargetBBP;

		public LayerMask scanLayer;
		public float scanDelay;

		Blackboard agentBlackboard;

		Variable<float> scanRadiusVar;
		float scanTimer;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
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
			scanTimer = 0;
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			scanTimer += Time.deltaTime;
			if (scanTimer > scanDelay)
			{
				Scan();
				DrawCircle(agent.transform.position, scanRadiusVar.value, Color.green, numberOfScanCirclePoints, 0.5f);
				scanTimer -= scanDelay;
			}
		}

		private void Scan()
		{
			Collider[] colliders = Physics.OverlapSphere(agent.transform.position, scanRadiusVar.value, scanLayer);
			float shortestDist = scanRadiusVar.value;
			Collider target = null;
			Blackboard targetBlackboard = null;
			for (int i = 0; i < colliders.Length; i++)
			{
				if (colliders[i] == null) continue;
                Blackboard currentBlackboard = colliders[i].GetComponentInParent<Blackboard>();
                float repairValue = currentBlackboard.GetVariableValue<float>("repairValue");
				if (repairValue > 15) continue;

                Vector3 displacement = colliders[i].transform.position - agent.transform.position;
				float distance = displacement.magnitude;
                if (distance < shortestDist)
				{
					shortestDist = distance;
					target = colliders[i];
					targetBlackboard = currentBlackboard;
				}
            }
            if (target != null)
			{
                lightTowerTargetBBP.value = targetBlackboard.GetVariableValue<Transform>("workpad");
            }
			EndAction(true);
        }

		private void DrawCircle(Vector3 center, float radius, Color colour, int numberOfPoints, float duration)
		{
			Vector3 startPoint, endPoint;
			int anglePerPoint = 360 / numberOfPoints;
			for (int i = 1; i <= numberOfPoints; i++)
			{
				startPoint = new Vector3(Mathf.Cos(Mathf.Deg2Rad * anglePerPoint * (i-1)), 0, Mathf.Sin(Mathf.Deg2Rad * anglePerPoint * (i-1)));
				startPoint = center + startPoint * radius;
				endPoint = new Vector3(Mathf.Cos(Mathf.Deg2Rad * anglePerPoint * i), 0, Mathf.Sin(Mathf.Deg2Rad * anglePerPoint * i));
				endPoint = center + endPoint * radius;
				Debug.DrawLine(startPoint, endPoint, colour, duration);
			}

			
		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}