using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public struct Mentor
{
	public Texture image;
	public String name;

	public Mentor(Texture t, String s){
		image = t;
		name = s;
	}
}



public class RandomPopScript : MonoBehaviour {

	public GameObject correct;
	public GameObject incorrect;

	public Mentor[] mentordata;

	public Mentor[] mentor;

	public GameObject display;
	private RawImage ri;

	public Texture[] tex;
	public String[] menname;

	private int nownumber = 0;

	private bool roulette = false;

	public Text button;

	public InputField IF;

	private bool correct_on = false;

	private int returncount = 0;

	public Text quetnum_text;
	private int questnum;

	private GameObject scoremanager;
	private ScoreManagerScript SMS;




	// Use this for initialization
	void Start () {

		scoremanager = GameObject.Find ("ScoreManager");
		SMS = scoremanager.GetComponent<ScoreManagerScript> ();

		questnum = 1;

		correct.SetActive (false);
		incorrect.SetActive (false);

		ri = display.GetComponent<RawImage> ();

		button.text = "解答する";
		IF.interactable = true;
		IF.text = "";

		mentordata = new Mentor[tex.Length];
		for (int i = 0; i < tex.Length; i++) {
			mentordata [i] = new Mentor(tex[i], menname[i]);
		}

		mentor = mentordata.OrderBy(i => Guid.NewGuid()).ToArray();

		nownumber = 0;//UnityEngine.Random.Range (0,mentor.Length);
		ri.texture = mentor [nownumber].image;
	}
	
	// Update is called once per frame
	void Update () {

		if (correct_on == true) {
			incorrect.SetActive (false);
		}

		quetnum_text.text = "第" + questnum + "問";

		Debug.Log (SMS.score);

		if (roulette == true) {
			int i = UnityEngine.Random.Range (0, mentor.Length);
			ri.texture = mentor [i].image;
		}

		if (Input.GetKeyDown (KeyCode.Return)) {
			returncount += 1;
		}
			
		if (IF.text == mentor [nownumber].name) {// + System.Environment.NewLine) {
			SMS.Correct();
			IF.text = "";
				correct.SetActive (true);
				correct_on = true;
				roulette = true;
				Invoke ("Stop", 1.0f);
				Invoke ("PreStop", 0.5f);
				Invoke ("PreStop", 0.6f);
				Invoke ("PreStop", 0.8f);
		} else {
			if (returncount >= 2) {
				if (correct_on == false) {
					incorrect.SetActive (true);
					Invoke ("IncorrectStop", 0.4f);
					IF.text = "";
					returncount = 0;
				}
			}
		}
	}

	public void Roulette(){
		if (IF.text == mentor[nownumber].name){// + System.Environment.NewLine) {
			if (nownumber < mentor.Length) {
				SMS.Correct ();
				IF.text = "";
				correct.SetActive (true);
				roulette = true;
				Invoke ("Stop", 1.0f);
				Invoke ("PreStop", 0.6f);
				Invoke ("PreStop", 0.8f);
				Invoke ("PreStop", 0.5f);
				Debug.Log ("正解");
			} else {
				//Application.Quit ();
			}
		} else {
			incorrect.SetActive (true);
			Invoke ("IncorrectStop",0.3f);
		}
	}

	void IncorrectStop(){
		incorrect.SetActive (false);
	}

	void PreStop(){
		incorrect.SetActive (false);
		int i = UnityEngine.Random.Range (0, mentor.Length);
		ri.texture = mentor[i].image;
		roulette = false;
		if (questnum == 10) {
			SceneManager.LoadScene ("Result");
		}
	}

	void Stop(){
		if (nownumber <= mentor.Length) {
			nownumber += 1;
			if (questnum == 11) {
				SceneManager.LoadScene ("Result");
			}
		} 
		incorrect.SetActive (false);
		correct.SetActive (false);
		Invoke ("NotZannen", 2.0f);
		if (nownumber == mentor.Length) {
			//シーン移動
		} 
		ri.texture = mentor[nownumber].image;
		IF.text = "";
		questnum += 1;
	}

	void NotZannen(){
		correct_on = false;
	}

	public void Skip(){
		incorrect.SetActive (true);
		Invoke ("IncorrectStop",0.3f);
		Invoke ("Stop", 1.0f);
		Invoke ("PreStop", 0.6f);
		Invoke ("PreStop", 0.8f);
		Invoke ("PreStop", 0.5f);
		nownumber += 1;
	}
}
