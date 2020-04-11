using UnityEngine;
using UnityEditor;
using System;
using System.IO;

namespace BrightLibTools
{
	public class FolderCreatorWindow : SmartWindow 
	{
		#region MenuItem

		private static string WINDOW_TITLE 	= "Folder Creator";
		private static float WINDOW_WIDTH 	= 200;
		private static float WINDOW_HEIGHT 	= 210;

		[MenuItem ("BrightTools/Folder Creator")]
		public static void ShowWindow ()  
		{
			EditorWindow.GetWindowWithRect<FolderCreatorWindow>(new Rect(0, 0, WINDOW_WIDTH, WINDOW_HEIGHT), true, WINDOW_TITLE);
		}

		#endregion

		#region JSON Template Data 
		[Serializable]
		public struct CreationTemplate
		{
			[Serializable]
			public struct CreationTemplateEntry
			{
				public string name;
				public bool chosen;
			}

			public string name;
			public CreationTemplateEntry[] entries;

			public void InjectEntries(string[] names, bool[] chosen)
			{
				if(names.Length != chosen.Length) throw new IndexOutOfRangeException();

				var length = names.Length;
				entries = new CreationTemplateEntry[length];

				for (int i = 0; i < length; i++) 
				{
					entries[i].name 	= names[i];
					entries[i].chosen 	= chosen[i];
				}
			}
		}
		#endregion

		private string[] DEFAULT_TEMPLATE_NAMES = {"Animation", "Art", "Audio", "Data", "Editor", "Materials", "Physics Materials", "Prefabs", "Resources", "Scenes", "Scripts", "Scripts-UnitTests", "Shaders", "Textures"};

		private static int DEF_ITEMS_VISIBLE = 8;

		private string[] folderNames;
		private bool[] folderChosen;
		private bool[] folderCreated;

		private Vector2 scrollPosition;

		private CreationTemplate templateDefault;

		void OnEnable()
		{
			if(!IsFolderCreated("Resources"))
				AssetDatabase.CreateFolder("Assets", "Resources");

			if(!DoesTemplateExist("FolderCreator-Template-Default"))
				CreateTemplate("FolderCreator-Template-Default", "Default", DEFAULT_TEMPLATE_NAMES);
			
			LoadTemplate();
			CheckFoldersCreated();
			for (int i = 0; i < folderNames.Length; i++) 
			{
				folderChosen[i] = !folderCreated[i] ? templateDefault.entries[i].chosen : false;
			}
		}

		private bool DoesTemplateExist(string fileName)
		{
			var jsonFile = Resources.Load<TextAsset>(fileName);
			return jsonFile != null;
		}

		private void CreateTemplate(string fileName, string templateName, string[] names)
		{
			CreationTemplate template = new CreationTemplate();
			template.name = templateName;
			template.InjectEntries(names, new bool[names.Length]);

			var jsonData 	= JsonUtility.ToJson(template ,prettyPrint: true);
			var folderPath 	= Application.dataPath + "/Resources/";
			var filePath 	= folderPath + fileName + ".txt";

			File.WriteAllText (filePath, jsonData);
			AssetDatabase.Refresh();
		}

		private void LoadTemplate()
		{
			var jsonFile = Resources.Load<TextAsset>("FolderCreator-Template-Default");
			templateDefault = JsonUtility.FromJson<CreationTemplate>(jsonFile.text);
			folderNames = new string[templateDefault.entries.Length];
			for (int i = 0; i < folderNames.Length; i++) 
			{
				folderNames[i] = templateDefault.entries[i].name;
			}
			folderChosen = new bool[folderNames.Length];
			folderCreated = new bool[folderNames.Length]; 
		}

		private void Refresh()
		{
			LoadTemplate();
			CheckFoldersCreated();
			for (int i = 0; i < folderNames.Length; i++) 
			{
				folderChosen[i] = !folderCreated[i] ? templateDefault.entries[i].chosen : false;
			}
		}

		void Update()
		{
			CheckFoldersCreated();
		}

		void OnGUI () 
		{
			DrawLabelBold("Choose Folders");
			DrawFolders();

			EditorGUILayout.Space();
			if(DrawButton("Create"))
			{
				if(!IsAnyFolderChosen())  return;
				CreateFolders();
			}
			if(DrawButton("Refresh"))
			{
				Refresh();
			}
		}

		private void DrawFolders()
		{
			var itemsVisible = Mathf.Min(DEF_ITEMS_VISIBLE, folderNames.Length - 1);

			var lastRect 	= GUILayoutUtility.GetLastRect();
			lastRect.y 		= lastRect.y + EditorGUIUtility.singleLineHeight * 1.25f;
			lastRect.width 	= EditorGUIUtility.currentViewWidth;
			lastRect.height = itemsVisible * EditorGUIUtility.singleLineHeight;
			GUI.Box(lastRect, Texture2D.blackTexture);

			scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(itemsVisible * EditorGUIUtility.singleLineHeight));
			for (int i = 0; i < folderNames.Length; i++) 
			{
				GUI.enabled = !folderCreated[i];
				folderChosen[i] = GUILayout.Toggle(folderChosen[i], folderNames[i]);
			}
			EditorGUILayout.EndScrollView();
			GUI.enabled = true;
		}

		private bool IsAnyFolderChosen()
		{
			for (int i = 0; i < folderChosen.Length; i++) 
			{
				if(folderChosen[i]) return true;
			}
			return false;
		}

		private void CheckFoldersCreated()
		{
			for (int i = 0; i < folderNames.Length; i++) 
			{
				folderCreated[i] = IsFolderCreated(folderNames[i]);
			}
		}

		private bool IsFolderCreated(string folderName)
		{
			return (AssetDatabase.IsValidFolder("Assets/" + folderName));
		}

		private void CreateFolders()
		{
			for (int i = 0; i < folderNames.Length; i++) 
			{
				if(!folderChosen[i]) continue;

				folderChosen[i] = false;
				AssetDatabase.CreateFolder("Assets", folderNames[i]);
			}
		}

	}
}