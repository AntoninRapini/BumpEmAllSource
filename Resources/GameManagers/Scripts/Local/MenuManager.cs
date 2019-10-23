using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
	public Camera mainCamera;
	public static MenuManager menuManager;
    public GameObject mainMenu, options, onlineMode;
    public Canvas canvas;
	//private bool isLerping;

	public GameObject currentScreen;

	void Awake()
	{
		if(menuManager == null)
		{
			menuManager = this;
		}
		else
		{
			Destroy (this);
		}
	}
    void Start()
	{
		mainMenu.SetActive(true);
		options.SetActive (false);
		onlineMode.SetActive(false);
		currentScreen = mainMenu;
    }

	public void ChangeScreen(GameObject screen)
	{
		currentScreen.SetActive (false);
		screen.SetActive (true);
		currentScreen = screen;
	}

	public void EnterLocalLobby()
	{
		SceneManager.LoadScene (1);
	}

    public void QuitGame()
    {
        Application.Quit();
    }



	/*IEnumerator MoveCamera(Vector3 targetRotation, GameObject toSetActive)
	{
		isLerping = true;
		Vector3 cameraStartRotation = mainCamera.transform.localRotation.eulerAngles;
		Vector3 cameraCurrentRotation = cameraStartRotation;
		float timeOfLerp = 0.5f;
		float timer = 0;

		while(timer < timeOfLerp)
		{
			cameraCurrentRotation = Vector3.Lerp (cameraStartRotation, targetRotation, timer / timeOfLerp);
			mainCamera.transform.localRotation = Quaternion.Euler(cameraCurrentRotation.x, cameraCurrentRotation.y, cameraCurrentRotation.z);
			timer += Time.deltaTime;
			yield return null;
		}

		if(toSetActive != null)
		{
			toSetActive.SetActive (true);
		}
		isLerping = false;
	}*/


}


