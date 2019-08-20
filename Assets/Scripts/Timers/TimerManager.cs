using UnityEngine;
using System.Collections;

public class TimerManager : MonoBehaviour {
	private static ArrayList timers = new ArrayList (); 
	public static bool isPlaying = true; 
	void Update () {
		if (!isPlaying)
			return;
		foreach (Timer t in timers){
			t.Update(Time.deltaTime);
		}
	}
		
	public static void SetupTimer(Timer t){
		timers.Add (t);
	}

	public void Pause() {

	}
}