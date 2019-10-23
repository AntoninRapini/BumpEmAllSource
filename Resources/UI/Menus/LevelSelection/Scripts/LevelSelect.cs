using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


// Controls the level selection  in the couch party mode
public class LevelSelect : MonoBehaviour {

	public GameObject playerSelectScreen;
    private PlayerOneMenuInputController input;
    public GameObject levelPreviewContainer;
    public int placeInArray;
    public Level[] levelList = new Level[2];
    public Level currentLevel;
	private float lastInputTime = 0;
	private float minTimeBetweenInputs = 0.2f;
	bool hasStarted;


    void Start ()
    {
        input = transform.GetComponent<PlayerOneMenuInputController>();
        placeInArray = 0;
		currentLevel = levelList [placeInArray];
        SelectScene();
		hasStarted = true;
    }



	void OnEnable()
	{
		if(hasStarted)
		{
			placeInArray = 0;
			currentLevel = levelList [placeInArray];
			SelectScene();
		}
	}

	void Update () {

		if (Time.time > lastInputTime + minTimeBetweenInputs)
		{
			if(input.Current.Left)
			{
				MoveInArray(-1);
			}
			else if(input.Current.Right)
			{
				MoveInArray(1);
			}
			else if(input.Current.Validate || input.Current.Select)
			{
				GameInformations.gameInformations.levelPrefab = currentLevel.levelPrefab;
				LobbyManager_Local.lobbyManager.LaunchGame ();
			}
			else if(input.Current.Deselect)
			{
				playerSelectScreen.SetActive (true);
				DeselectScene ();
				gameObject.SetActive (false);
			}
		}
  

	
	}

    void MoveInArray(int direction)
    {
		DeselectScene ();
        placeInArray += direction;
        if(placeInArray < 0)
        {
            placeInArray = levelList.Length - 1;
        }
        else if(placeInArray == levelList.Length)
        {
            placeInArray = 0;
        }
		SelectScene ();
    }

    void SelectScene()
    {
		
        currentLevel = levelList[placeInArray];
		currentLevel.levelPreview.SetActive (true);
    }

	void DeselectScene()
	{
		currentLevel.levelPreview.SetActive (false);
	}
}
