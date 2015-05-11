using UnityEditor;
using UnityEngine;
using System.Collections;

// Creates an instance of a primitive depending on the option selected by the user.
public class ReorderLayers : EditorWindow 
{
	public string[] options = new string[] {"foreground1", "foreground2", "foreground3"};
	public int index = 0;

	[MenuItem("CustomOptions/ReorderLayers")]
	static void Init() 
	{
		EditorWindow window = GetWindow(typeof(ReorderLayers));
		window.Show();
	}


	void OnGUI() 
	{
		index = EditorGUILayout.Popup(index, options);
		if (GUILayout.Button("Execute"))
			Reorder();
		
	}


	void Reorder() 
	{
		SpriteRenderer[] renderers = FindObjectsOfType(typeof(SpriteRenderer)) as SpriteRenderer[];
		int i = 0;
		switch(index) 
		{
		case 0:  
			foreach(SpriteRenderer sr in renderers)
			{
				if(sr.sortingLayerName == "foreground1")
				{
					sr.sortingOrder = i;
					i++;
				}
					
			}
			break;
		case 1:
			foreach(SpriteRenderer sr in renderers)
			{
				if(sr.sortingLayerName == "foreground2")
				{
					sr.sortingOrder = i;
					i++;
				}
			}
			break;
		case 2:
			foreach(SpriteRenderer sr in renderers)
			{
				if(sr.sortingLayerName == "foreground3")
				{
					sr.sortingOrder = i;
					i++;
				}
			}
			break;
		default:
			Debug.LogError("Unrecognized Option");
			break;
		}
	}
}