using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Conditions {

	public class InCoopCT : ConditionTask
    {
        ChickenProperties chicken;

        int coopWalkable;
        int coopNonWalkable;

        protected override string OnInit()
        {
            chicken = agent.GetComponent<ChickenProperties>();
            if (!chicken)
                return $"Agent {agent.name} does not have a ChickenProperties script attached!";

            coopWalkable = NavMesh.GetAreaFromName("Coop");
            coopNonWalkable = NavMesh.GetAreaFromName("Not Targetable");

            Debug.Log(coopWalkable);
            Debug.Log(coopNonWalkable);

            return null;
        }

        protected override bool OnCheck() {

            // Path sampling via https://discussions.unity.com/t/how-can-i-tell-which-area-is-a-navmeshagent-currently-standing-on/853407/7
            chicken.navAgent.SamplePathPosition(coopWalkable + coopNonWalkable, 1, out var hit);

			return hit.hit;
		}
	}
}