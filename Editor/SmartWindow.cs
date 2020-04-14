using UnityEditor;
using UnityEngine;

namespace BrightLibTools
{
	public class SmartWindow : EditorWindow 
	{
		public void DrawLabelBold(string text)
		{
			GUILayout.Label(text, EditorStyles.boldLabel); 
		}

		public bool DrawButton(string text, float width = 60, float height = 20)
		{
			return GUILayout.Button(text, GUILayout.Width(width), GUILayout.Height(height));
		}

		public bool DrawButton(string text, params GUILayoutOption[] options)
		{
			return GUILayout.Button(text, options);
		}

		public bool DrawToggle(bool boolean, string text, float width = 60, float height = 20)
		{
			return GUILayout.Toggle(boolean, text, GUILayout.MinWidth(width), GUILayout.MinHeight(height));
		}

		public string TextFieldArea(string label, string text, params GUILayoutOption[] options)
		{
			EditorGUILayout.LabelField(label);
			return EditorGUILayout.TextArea(text, options);
		}

		public void StartGreyedOutArea(bool toggle)
		{
			GUI.enabled = toggle;
		}

		public void EndGreyedOutArea()
		{
			GUI.enabled = true;
		}
	}

}