using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions{

	public class AwaitActionTask : ActionTask{
		//public float maxWaitTime;
		//public float minWaitTime;
		public BBParameter<Vector3> startingPosition;

		//private float totalTimeToWait;
		//private float timeWaiting = 0f;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit(){
            startingPosition.value = agent.transform.position;
            return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		//protected override void OnExecute(){
		//	timeWaiting = 0f;
		//	totalTimeToWait = UnityEngine.Random.Range(minWaitTime, maxWaitTime);
			
		//}

		//Called once per frame while the action is active.
		//protected override void OnUpdate(){
		//	//Choose a random time between two values
		//	//Wait that amount of time
		//	timeWaiting += Time.deltaTime;
		//	if(timeWaiting > totalTimeToWait)
		//	{
		//		EndAction(true);
		//	}
  //      }
	}
}