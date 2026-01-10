using NodeCanvas.Framework;
using NodeCanvas.StateMachines;
using ParadoxNotion.Design;
using ParadoxNotion.Services;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class EnemyKillAT : ActionTask
    {
        private GameObject player;
        private RigidbodyController rbController;
        private EnemyData stats;

        private bool didDeactivation = false;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit()
        {
            player = blackboard.GetVariableValue<GameObject>("player");
            if (!player) return "No 'player' assigned in the blackboard!";

            rbController = blackboard.GetVariableValue<RigidbodyController>("rbController");
            if (!rbController) return "No RigidBodyController component on blackboard!";

            stats = blackboard.GetVariableValue<EnemyData>("enemyData");
            if (!stats) return "No 'enemyData' assigned on blackboard!";

            return null;
        }

        //This is called once each time the task is enabled.
        //Call EndAction() to mark the action as finished, either in success or failure.
        //EndAction can be called from anywhere.
        protected override void OnExecute()
        {
            ////////////////////////////////////////////////////////////////////////////////////////////////
            /// This method of using MonoManager.current.onFixedUpdate was found via the following link: ///
			/// https://nodecanvas.paradoxnotion.com/documentation/?section=using-fixedupdate-and-ongui  ///
            ////////////////////////////////////////////////////////////////////////////////////////////////
            MonoManager.current.onFixedUpdate += FixedUpdate;

            // Disable player movement
            player.GetComponent<FSMOwner>().PauseBehaviour();
        }

        protected override void OnUpdate()
        {
            if (didDeactivation || elapsedTime < stats.timeToKill) return;

            player.SetActive(false);
            didDeactivation = true;
        }

        private void FixedUpdate()
        {
            if (elapsedTime < stats.timeToKill) return;
            // Move in circles (celebrating)
            float theta = elapsedTime * 5;
            Vector3 direction = new Vector3(Mathf.Sin(theta), 0, Mathf.Cos(theta));
            rbController.MoveBody(direction);
        }

        //Called when the task is disabled.
        protected override void OnStop()
        {
            MonoManager.current.onFixedUpdate -= FixedUpdate;
        }
    }
}
