using Finegamedesign.Utils;

namespace Finegamedesign.CityOfWords
{
	[System.Serializable]
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
			ViewLetterButtons();
			ViewPrompts();
			ViewPrompt(model.selected, view.selected);
		}

		private void ViewLetterButtons()
		{
			for (int index = 0; index < DataUtil.Length(model.letterButtonTexts); index++)
			{
				string letter = model.letterButtonTexts[index];
				var letterView = view.letterButtonTexts[index];
				TextView.SetText(letterView, letter);
				SceneNodeView.SetVisible(view.letterButtons[index], model.empty != letter);
			}
		}

		private void ViewPrompts()
		{
			for (int index = 0; index < DataUtil.Length(model.promptAndAnswers); index++)
			{
				var prompt = model.promptAndAnswers[index];
				var promptView = view.promptAndAnswers[index];
				ViewPrompt(prompt, promptView);
			}
		}

		private void ViewPrompt(PromptModel prompt, PromptView promptView)
		{
			TextView.SetText(promptView.promptText, prompt.promptText);
			for (int letter = 0; letter < DataUtil.Length(prompt.answerTexts); letter++)
			{
				string a = prompt.answerTexts[letter];
				TextView.SetText(promptView.answerTexts[letter], a);
				bool isVisible = letter < DataUtil.Length(prompt.answerText);
				SceneNodeView.SetVisible(promptView.answers[letter], isVisible); 
			}
		}
	}
}
