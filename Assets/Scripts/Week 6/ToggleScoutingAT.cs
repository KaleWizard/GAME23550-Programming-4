using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class ToggleScoutingAT : ActionTask {

		public BBParameter<bool> scoutingBBP;
		public AudioSource audio;
		public AudioClip clip;

		protected override void OnExecute() {
			scoutingBBP.value = !scoutingBBP.value;
			AudioManager.Instance.PlaySoundEffect(clip, audio);
			EndAction(true);
		}
	}
}