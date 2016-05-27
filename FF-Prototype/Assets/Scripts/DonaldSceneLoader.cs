using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class DonaldSceneLoader : MonoBehaviour 
{ 
 
	/// <summary>
	/// Description: This is a function
	/// 	public is the access modifier
	/// 	void is the return type
	/// 	DoSomething is the name
	/// 	() is the argument list or parameters
	///		(string scenename) is the name of the scene we want to go to
	/// Usage:Attach this to a gameobject in the Scene Heirarchy
	/// Assign the gameobject that this is attached to the On Click () event in the inspector for the BUTTON
	/// Choose the class this function is in which is DonaldSceneLoader
	/// Choose this function in the function drop down
	/// </summary>

	public void LoadScene (string scenename)
	{
		//the thing we want to do is change the level
		//you must add the scene you want to change to 
		//to the build
		SceneManager.LoadScene (scenename);

	}
 

}
