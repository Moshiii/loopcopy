using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AddBtn : MonoBehaviour
{


	[SerializeField]
	private Transform TileField;
	[SerializeField]
	private GameObject btn;

	// Use this for initialization
	void Awake ()
	{

		int N_cols = TileField.GetComponent<GridLayoutGroup> ().constraintCount;
		//Debug.Log ("Number of colums" + N_cols);
		for (int i = 0; i < 25; i++) {
			GameObject button = Instantiate (btn);
			//tile_row_col
			button.name = "tile_" + i / N_cols + "_" + i % N_cols;
			button.GetComponent<ClassButton> ().Row = i / N_cols;
			button.GetComponent<ClassButton> ().Col = i % N_cols;
			if (i / N_cols == 0) {
				
				if (i % N_cols == 0) {
					button.GetComponent<ClassButton> ().PositionState = ClassButton.CONER_UL;
				} else if (i % N_cols == N_cols - 1) {
					button.GetComponent<ClassButton> ().PositionState = ClassButton.CONER_UR;
				} else {
					button.GetComponent<ClassButton> ().PositionState = ClassButton.EDGE_U;
				}

			} else if (i / N_cols == N_cols - 1) {

				if (i % N_cols == 0) {
					button.GetComponent<ClassButton> ().PositionState = ClassButton.CONER_DL;
				} else if (i % N_cols == N_cols - 1) {
					button.GetComponent<ClassButton> ().PositionState = ClassButton.CONER_DR;
				} else {
					button.GetComponent<ClassButton> ().PositionState = ClassButton.EDGE_D;
				}

			} else {

				if (i % N_cols == 0) {
					button.GetComponent<ClassButton> ().PositionState = ClassButton.EDGE_L;
				} else if (i % N_cols == N_cols - 1) {
					button.GetComponent<ClassButton> ().PositionState = ClassButton.EDGE_R;
				} else {
					button.GetComponent<ClassButton> ().PositionState = ClassButton.CENTER;
				}

			}


			button.transform.SetParent (TileField, false);

		}
	}

	// Update is called once per frame
	void Update ()
	{

	}
}
