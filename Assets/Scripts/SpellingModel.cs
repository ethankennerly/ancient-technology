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

		public void Setup()
		{
			letterButtonTexts = new string[letterMax];
			promptAndAnswers = new PromptModel[promptMax];
		}

		public void Populate()
		{
			string[] row = table[tableIndex];
			PopulateLetterButtons(row);
			PopulatePrompts(row);
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
				prompt.answerTexts = new string[letterMax];
				string answer = row[index + 1];
				int letter;
				for (letter = 0; letter < DataUtil.Length(answer); letter++)
				{
					prompt.answerTexts[letter] = answer[letter].ToString();
				}
				for (; letter < letterMax; letter++)
				{
					prompt.answerTexts[letter] = empty;
				}
				int p = (int)((index - promptColumn) / 2);
				DebugUtil.Log(answer + " " + p);
				promptAndAnswers[p] = prompt;
			}
		}
	}
}
