using System;

namespace BrightLib.FolderCreator.Editor
{
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

	
}