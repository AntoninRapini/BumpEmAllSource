using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Level  {

    public int levelNumber;
    public GameObject levelPreview;
	public GameObject levelPrefab;
    public Level(int _levelNumber, GameObject _levelPreview, GameObject _levelPrefab)
    {
        levelNumber = _levelNumber;
		levelPreview = _levelPreview;
		levelPrefab = _levelPrefab;
    }
}
