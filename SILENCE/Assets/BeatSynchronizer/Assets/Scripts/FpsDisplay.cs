using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UnityEngine.UI.Text))]
public class FpsDisplay : MonoBehaviour {

	public float updateInterval = 0.5f;

	private int frameCount;
	private float accumTime;
	private float timeLeft;

	
	void Start ()
	{
		timeLeft = updateInterval;
	}

	void Update ()
	{
		timeLeft -= Time.deltaTime;
		accumTime += Time.timeScale / Time.deltaTime;
		++frameCount;

		if (timeLeft <= 0f) {
			float fps = accumTime / frameCount;
			string fpsDisplay = System.String.Format("Current framerate: {0:F2}", fps);
			GetComponent<UnityEngine.UI.Text>().text = fpsDisplay;
			
			if (fps < 30)
				GetComponent<UnityEngine.UI.Text>().material.color = Color.yellow;
			else 
				if (fps < 10)
					GetComponent<UnityEngine.UI.Text>().material.color = Color.red;
			else
				GetComponent<UnityEngine.UI.Text>().material.color = Color.green;

			timeLeft = updateInterval;
			accumTime = 0f;
			frameCount = 0;
		}
	}

}
