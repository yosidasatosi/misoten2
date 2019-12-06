using UnityEngine;
using System.Collections;

public class SlideShowAnimator : MonoBehaviour {

	[System.Serializable]
	public struct Anims {
		public string triggerName;
		public float time;
	}
	
	public Anims[] animations;
	int currentAnimIndex;
	string lastTrigger;

	Animator animator;


	// Use this for initialization
	void Start () {

		animator = GetComponent<Animator>();
		StartCoroutine(AnimateStuff());
	
	}

	IEnumerator AnimateStuff() {
		while (true) {

			animator.SetTrigger(animations[currentAnimIndex].triggerName);
			Debug.Log ("Playing " + animations[currentAnimIndex].triggerName);
			yield return new WaitForSeconds(animations[currentAnimIndex].time);
			//animator.SetTrigger(animations[currentAnimIndex].triggerName);
			//lastTrigger = animations[currentAnimIndex].triggerName;

			currentAnimIndex++;
			if (currentAnimIndex > animations.Length-1)
				currentAnimIndex = 0;

		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
