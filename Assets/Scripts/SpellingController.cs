using Finegamedesign.Utils;

namespace Finegamedesign.CityOfWords
{
	public sealed class SpellingController
	{
		public SpellingModel model = new SpellingModel();
		public SpellingView view;

		public void Setup()
		{
			model.table = Load();
			model.Setup();
			view = (SpellingView) SceneNodeView.FindObjectOfType(typeof(SpellingView));
		}

		public string[][] Load()
		{
			string csv = StringUtil.Read("words.csv");
			return StringUtil.ParseCsv(csv);
		}

		public void Populate()
		{
			model.Populate();
			UpdateView();
		}

		public void Update()
		{
			UpdateView();
		}

		private void UpdateView()
		{
			for (int index = 0; index < DataUtil.Length(model.letterButtonTexts); index++)
			{
				string letter = model.letterButtonTexts[index];
				var letterView = view.letterButtonTexts[index];
				TextView.SetText(letterView, letter);
				SceneNodeView.SetVisible(view.letterButtons[index], model.empty != letter);
			}
		}
	}
}
