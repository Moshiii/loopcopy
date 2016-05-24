using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

	[SerializeField]
	public  Sprite[] Tileskin;
	public int N_cols;
	public List<Button> btns = new List<Button> ();

	void Aweak ()
	{
		
	}
	// Use this for initialization
	void Start ()
	{
		
		GetButtons ();
		GeneratePuzzle ();
		AddListeners ();

	}

	void Update ()
	{

	}

	void GetButtons ()
	{
		GameObject[] objects = GameObject.FindGameObjectsWithTag ("PuzzleBtn");
	
		for (int i = 0; i < objects.Length; i++) {
			btns.Add (objects [i].GetComponent<Button> ());
		}

	
	}

	void OnMouseDown ()
	{
		//UpdateTileSkin ();
	}

	void AddListeners ()
	{
		foreach (Button btn in btns) {
			
			btn.onClick.AddListener (() => ClickTile ());
			ClassButton btnRef = btn.GetComponent<ClassButton> ();
			Debug.Log ("updating " + btn.name);
			//btn.onClick.AddListener (() => UpdateTileSkin ());
			btn.onClick.AddListener (() => CheckComplete ());
		}
	}

	public void ClickTile ()
	{
		GameObject currentSelectedGameObject = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

		string name = currentSelectedGameObject.name;
		currentSelectedGameObject.GetComponent<ClassButton> ().Rotate ();
		currentSelectedGameObject.transform.Rotate (Vector3.forward * -90);
	}

	void GeneratePuzzle ()
	{
		AddCompleteGraph ();
		for (int i = 0; i < 50; i++) {
			RemoveRandomEdges ();
		}
		for (int i = 0; i < 100; i++) {
			RotateRandomNodes ();
		}
		UpdateTileSkin ();

	}

	void UpdateTileSkin ()
	{
		foreach (Button btn in btns) {
			ClassButton btnRef = btn.GetComponent<ClassButton> ();
			PutTileSkin (btn, btnRef.LinkU, btnRef.LinkD, btnRef.LinkL, btnRef.LinkR);
		}
	}


	void AddCompleteGraph ()
	{
		foreach (Button btn in btns) {
			ClassButton btnRef = btn.GetComponent<ClassButton> ();
			int btnPos = btnRef.PositionState;

			if (btnPos == ClassButton.EDGE_L) {
				btnRef.LinkU = 1;
				btnRef.LinkD = 1;
				btnRef.LinkL = 0;
				btnRef.LinkR = 1;
			} else if (btnPos == ClassButton.EDGE_U) {
				btnRef.LinkU = 0;
				btnRef.LinkD = 1;
				btnRef.LinkL = 1;
				btnRef.LinkR = 1;
			} else if (btnPos == ClassButton.EDGE_D) {
				btnRef.LinkU = 1;
				btnRef.LinkD = 0;
				btnRef.LinkL = 1;
				btnRef.LinkR = 1;
			} else if (btnPos == ClassButton.EDGE_R) {
				btnRef.LinkU = 1;
				btnRef.LinkD = 1;
				btnRef.LinkL = 1;
				btnRef.LinkR = 0;
			} else if (btnPos == ClassButton.CONER_UL) {
				btnRef.LinkU = 0;
				btnRef.LinkD = 1;
				btnRef.LinkL = 0;
				btnRef.LinkR = 1;
			} else if (btnPos == ClassButton.CONER_UR) {
				btnRef.LinkU = 0;
				btnRef.LinkD = 1;
				btnRef.LinkL = 1;
				btnRef.LinkR = 0;
			} else if (btnPos == ClassButton.CONER_DR) {
				btnRef.LinkU = 1;
				btnRef.LinkD = 0;
				btnRef.LinkL = 1;
				btnRef.LinkR = 0;
			} else if (btnPos == ClassButton.CONER_DL) {
				btnRef.LinkU = 1;
				btnRef.LinkD = 0;
				btnRef.LinkL = 0;
				btnRef.LinkR = 1;
			} else {//center
				btnRef.LinkU = 1;
				btnRef.LinkD = 1;
				btnRef.LinkL = 1;
				btnRef.LinkR = 1;
			}
		}                

	}

	void PutTileSkin (Button btn, int LinkU, int LinkD, int LinkL, int LinkR)
	{
		if (LinkU == 0 && LinkD == 0 && LinkR == 0 && LinkL == 0) {
			//btn.image.sprite = Tileskin [0];
		} else if (LinkU == 0 && LinkD == 0 && LinkR == 0 && LinkL == 1) {
			btn.image.sprite = Tileskin [1];
			btn.transform.Rotate (Vector3.forward * -180);
		} else if (LinkU == 0 && LinkD == 0 && LinkR == 1 && LinkL == 0) {
			btn.image.sprite = Tileskin [1];
		} else if (LinkU == 0 && LinkD == 0 && LinkR == 1 && LinkL == 1) {
			btn.image.sprite = Tileskin [5];
		} else if (LinkU == 0 && LinkD == 1 && LinkR == 0 && LinkL == 0) {
			btn.image.sprite = Tileskin [1];
			btn.transform.Rotate (Vector3.forward * -90);
		} else if (LinkU == 0 && LinkD == 1 && LinkR == 0 && LinkL == 1) {
			btn.image.sprite = Tileskin [2];
			btn.transform.Rotate (Vector3.forward * -180);
		} else if (LinkU == 0 && LinkD == 1 && LinkR == 1 && LinkL == 0) {
			btn.image.sprite = Tileskin [2];
			btn.transform.Rotate (Vector3.forward * -90);
		} else if (LinkU == 0 && LinkD == 1 && LinkR == 1 && LinkL == 1) {
			btn.image.sprite = Tileskin [3];
			btn.transform.Rotate (Vector3.forward * -180);
		} else if (LinkU == 1 && LinkD == 0 && LinkR == 0 && LinkL == 0) {
			btn.image.sprite = Tileskin [1];
			btn.transform.Rotate (Vector3.forward * -270);
		} else if (LinkU == 1 && LinkD == 0 && LinkR == 0 && LinkL == 1) {
			btn.image.sprite = Tileskin [2];
			btn.transform.Rotate (Vector3.forward * -270);
		} else if (LinkU == 1 && LinkD == 0 && LinkR == 1 && LinkL == 0) {
			btn.image.sprite = Tileskin [2];
		} else if (LinkU == 1 && LinkD == 0 && LinkR == 1 && LinkL == 1) {
			btn.image.sprite = Tileskin [3];
		} else if (LinkU == 1 && LinkD == 1 && LinkR == 0 && LinkL == 0) {
			btn.image.sprite = Tileskin [5];
			btn.transform.Rotate (Vector3.forward * -90);
		} else if (LinkU == 1 && LinkD == 1 && LinkR == 0 && LinkL == 1) {
			btn.image.sprite = Tileskin [3];
			btn.transform.Rotate (Vector3.forward * -270);
		} else if (LinkU == 1 && LinkD == 1 && LinkR == 1 && LinkL == 0) {
			btn.image.sprite = Tileskin [3];
			btn.transform.Rotate (Vector3.forward * -90);
		} else {// (LinkU == 1 && LinkD == 1 && LinkR == 1 && LinkL == 1) 
			btn.image.sprite = Tileskin [4];
		} 
	}

	void RemoveRandomEdges ()
	{
		Button SelectedBtn = SelectRandomNode ();
		Button BtnNeighbour;

		int Direction = Random.Range (0, 4);
		if (Direction == 0) {//Up
			BtnNeighbour = SelectNeighbour (SelectedBtn, "U");
		} else if (Direction == 1) {//Right
			BtnNeighbour = SelectNeighbour (SelectedBtn, "R");
		} else if (Direction == 2) {//Down
			BtnNeighbour = SelectNeighbour (SelectedBtn, "D");
		} else {//Left
			BtnNeighbour = SelectNeighbour (SelectedBtn, "L");
		}

		//Debug.Log ("select btn" + SelectedBtn.name);


		if (BtnNeighbour != null) {
			//	Debug.Log ("select neighbour" + BtnNeighbour.name);
			RemovEedge (SelectedBtn, BtnNeighbour);
		}

	}

	Button SelectRandomNode ()
	{
		Button A;
		int index = Random.Range (0, btns.Count);
		A = btns [index];
		return A;
	}


	Button SelectNeighbour (Button SelectedBtn, string Direction)
	{
		
		ClassButton btnRef = SelectedBtn.GetComponent<ClassButton> ();

		Button B = null;

		if (Direction == "U") {//Up
			B = btns.Where (obj => obj.name == "tile_" + (btnRef.Row - 1) + "_" + btnRef.Col).SingleOrDefault ();
		} else if (Direction == "R") {//Right
			B = btns.Where (obj => obj.name == "tile_" + btnRef.Row + "_" + (btnRef.Col + 1)).SingleOrDefault ();
		} else if (Direction == "D") {//Down
			B = btns.Where (obj => obj.name == "tile_" + (btnRef.Row + 1) + "_" + btnRef.Col).SingleOrDefault ();
		} else if (Direction == "L") {//Left
			B = btns.Where (obj => obj.name == "tile_" + btnRef.Row + "_" + (btnRef.Col - 1)).SingleOrDefault ();
		}

		return B;
	}

	void RemovEedge (Button SelectedBtn, Button BtnNeighbour)
	{
		ClassButton btnARef = SelectedBtn.GetComponent<ClassButton> ();
		ClassButton btnBRef = BtnNeighbour.GetComponent<ClassButton> ();
		if (btnARef.Row == btnBRef.Row) {
			if (btnARef.Col == btnBRef.Col + 1) {
				btnARef.LinkL = 0;
				btnBRef.LinkR = 0;
			} else {
				btnARef.LinkR = 0;
				btnBRef.LinkL = 0;
			}
		} else if (btnARef.Row == btnBRef.Row + 1) {
			btnARef.LinkU = 0;
			btnBRef.LinkD = 0;

		} else {
			btnARef.LinkD = 0;
			btnBRef.LinkU = 0;
		}

	}

	bool CheckComplete ()
	{
		
		bool ifComplete = true;

		foreach (Button btn in btns) {
			ClassButton btnARef = btn.GetComponent<ClassButton> ();
			if (SelectNeighbour (btn, "R") != null) {
				//Debug.Log ("Right_N = " + SelectNeighbour (btn, "R").name);
				ClassButton btnRRef = SelectNeighbour (btn, "R").GetComponent<ClassButton> ();
				if (btnARef.LinkR != btnRRef.LinkL) {
					Debug.Log ("btn.name = " + btn.name + " btnARef.LinkR = " + btnARef.LinkR + " btnRRef.LinkL = " + btnRRef.LinkL);
					ifComplete = false;
				}
			}
			if (SelectNeighbour (btn, "D") != null) {
				//Debug.Log ("Down_N = " + SelectNeighbour (btn, "D").name);
				ClassButton btnDRef = SelectNeighbour (btn, "D").GetComponent<ClassButton> ();
				if (btnARef.LinkD != btnDRef.LinkU) {
					Debug.Log ("btn.name = " + btn.name + " btnARef.LinkD = " + btnARef.LinkD + " btnRRef.LinkU = " + btnDRef.LinkU);
					ifComplete = false;
				}
			}
		}
		Debug.Log ("ifComplete = " + ifComplete);
		return ifComplete;
	}

	void RotateRandomNodes ()
	{
		Button SelectedBtn = SelectRandomNode ();
		ClassButton btnARef = SelectedBtn.GetComponent<ClassButton> ();

		int R_times = Random.Range (0, 4);
		for (int i = 0; i < R_times; i++) {
			btnARef.Rotate ();
		}
	}


}
