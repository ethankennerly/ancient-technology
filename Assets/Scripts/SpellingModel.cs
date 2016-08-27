using Finegamedesign.Utils;

namespace Finegamedesign.CityOfWords
{
	[System.Serializable]
	public sealed class SpellingModel
	{
		public SpellingView view;
		public string[][] table;
		public int tableIndex = 1;
		public string empty = "";
		public string[] letterButtonTexts;
		public int letterMax = 8;
		public int lettersColumn = 1;
		public int promptColumn = 2;
		public int promptMax = 4;
		public PromptModel[] promptAndAnswers;
		public PromptModel selected;

		public void Setup()
		{
			letterButtonTexts = new string[letterMax];
			promptAndAnswers = new PromptModel[promptMax];
			selected = new PromptModel();
			selected.answerTexts = new string[letterMax];
		}

		public void Populate()
		{
			string[] row = table[tableIndex];
			PopulateLetterButtons(row);
			PopulatePrompts(row);
			ClearSelected();
		}

		private void PopulateLetterButtons(string[] row)
		{
			string letters = row[lettersColumn];
			int index;
			for (index = 0; index < DataUtil.Length(letterButtonTexts); index++)
			{
				if (index < DataUtil.Length(letters))
				{
					letterButtonTexts[index] = letters[index].ToString();
				}
				else
				{
					letterButtonTexts[index] = empty;
				}
			}
		}

		private void PopulatePrompts(string[] row)
		{
			for (int index = promptColumn; index < DataUtil.Length(row); index += 2)
			{
				var prompt = new PromptModel();
				prompt.promptText = row[index];
				string answer = row[index + 1];
				prompt.PopulateAnswer(answer, letterMax, empty);
				int p = (int)((index - promptColumn) / 2);
				promptAndAnswers[p] = prompt;
			}
		}

		private void ClearSelected()
		{
			DataUtil.Clear(selected.answerTexts);
		}
	}
}
