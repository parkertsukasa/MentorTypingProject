using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResuletManagerScript : MonoBehaviour {

	public Text score_text;
	private GameObject scoremanager;
	private ScoreManagerScript SMS;

	// Use this for initialization
	void Start () {

		scoremanager = GameObject.Find ("ScoreManager");
		SMS = scoremanager.GetComponent<ScoreManagerScript> ();

		score_text.text = SMS.score + "/10";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Restert(){
		Destroy (scoremanager);
		SceneManager.LoadScene ("main");
	}

	public void Exit(){
		Application.Quit ();
	}
}
