using NodeCanvas.Framework;
using ParadoxNotion.Services;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class EnemySeekAT : ActionTask
    {
        private GameObject player;
        private RigidbodyController rbController;
        private EnemyData stats;

        private MeshRenderer meshRenderer;
        private Material baseMaterial;

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

            meshRenderer = agent.GetComponent<MeshRenderer>();
            if (!meshRenderer) return "No MeshRenderer component is attached!";

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

            baseMaterial = meshRenderer.sharedMaterial;
            meshRenderer.sharedMaterial = stats.seekingMaterial;
        }

        protected override void OnUpdate()
        {
            if (elapsedTime > stats.seekingTime) EndAction(true);
        }

        private void FixedUpdate()
        {
            Vector3 direction = player.transform.position - agent.transform.position;
            direction.y = 0;
            rbController.MoveBody(direction);
        }

        //Called when the task is disabled.
        protected override void OnStop()
        {
            MonoManager.current.onFixedUpdate -= FixedUpdate;
            meshRenderer.sharedMaterial = baseMaterial;
        }
    }
}