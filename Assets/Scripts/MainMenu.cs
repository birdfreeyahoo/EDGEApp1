using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	enum MenuPages
	{
		Main,
		Options,
	}

	MenuPages page;

	public Vector2 margin;
	int numButtons;

	int buttonWidth;
	int buttonHeight;

	// Use this for initialization
	void Start () 
	{
		page = MenuPages.Main;

		margin = new Vector2(10,10);
		numButtons = 3;
		buttonWidth = Screen.width - ( 2 * (int)margin.x );
		buttonHeight = (Screen.height - ( (numButtons + 1) * (int)margin.y )) / numButtons;
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			switch(page)
			{
			case MenuPages.Main:
				Application.Quit();
				break;
			case MenuPages.Options:
				page = MenuPages.Main;
				break;
			}
		}
	}

	void OnGUI()
	{
		GUIStyle btn = new GUIStyle("button");
		btn.fontSize = 50;

        GUIStyle tog = new GUIStyle("toggle");
        tog.fontSize = 35;

		switch(page)
		{ 
		case MenuPages.Main:
			if(GUI.Button(new Rect(margin.x,margin.y,buttonWidth,buttonHeight),"Start",btn))
			{
				Application.LoadLevel(1);
			}
			if(GUI.Button(new Rect(margin.x,2*margin.y + buttonHeight,buttonWidth,buttonHeight),"Options",btn))
			{
				page = MenuPages.Options;
			}
			if(GUI.Button(new Rect(margin.x,3*margin.y + 2 * buttonHeight,buttonWidth,buttonHeight),"Exit",btn))
			{
				Application.Quit();
			}
			break;
        case MenuPages.Options:
            GameOptions.invertYAxis = GUI.Toggle(new Rect(10, 10, 120, 35), GameOptions.invertYAxis, "Invert Y Axis",tog);
            break;

		
		}
	}
}
