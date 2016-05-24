using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClassButton : MonoBehaviour
{
	
	public static int EDGE_L = 1;
	public static int EDGE_U = 2;
	public static int EDGE_R = 3;
	public static int EDGE_D = 4;
	public static int CONER_UL = 5;
	public static int CONER_UR = 6;
	public static int CONER_DL = 7;
	public static int CONER_DR = 8;
	public static int CENTER = 9;

	public int Row;
	public int Col;
	public int PositionState;

	public int LinkU;
	public int LinkD;
	public int LinkL;
	public int LinkR;
	public GameObject attachedGO;
	// Use this for initialization
	void Start ()
	{
		
		attachedGO = transform.gameObject;
	}
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void Rotate ()
	{
		//transform.Rotate (Vector3.forward * -90);
		int previousLinkU = LinkU;
		int previousLinkD = LinkD;
		int previousLinkL = LinkL;
		int previousLinkR = LinkR;
		LinkU = previousLinkL;
		LinkD = previousLinkR;
		LinkL = previousLinkD;
		LinkR = previousLinkU;

	}
}
