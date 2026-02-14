using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine.AI;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions{

	public class ReturnActionTask : ActionTask{
		public BBParameter<Vector3> startingPosition;
        public float arrivalDistance;

        private NavMeshAgent navmeshAgent;
        
		//Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit(){
            navmeshAgent = agent.GetComponent<NavMeshAgent>();
            return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute(){
            NavMeshHit navMeshHit = new NavMeshHit();
            if (!NavMesh.SamplePosition(startingPosition.value, out navMeshHit, 2, NavMesh.AllAreas))
            {
                Debug.Log("Could not generate a path.");
            }
            else
            {
                //Set the path
                navmeshAgent.SetDestination(navMeshHit.position);
            }
        }

		//Called once per frame while the action is active.
		protected override void OnUpdate(){
            float distanceToTarget = Vector3.Distance(startingPosition.value, agent.transform.position);
            if (navmeshAgent.pathStatus == NavMeshPathStatus.PathComplete &&
                 distanceToTarget < arrivalDistance)
            { 
                EndAction(true);
            }
        }

		//Called when the task is disabled.
		protected override void OnStop(){
			
		}

		//Called when the task is paused.
		protected override void OnPause(){
			
		}
	}
}