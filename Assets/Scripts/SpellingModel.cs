using Finegamedesign.Utils;

namespace Finegamedesign.CityOfWords
{
	[System.Serializable]
	public sealed class SpellingModel
	{
		public SpellingView view;
		public string[][] table;
		public int contentIndex = 0;
		public string empty = "";
		public string[] letterButtonTexts;
		public int letterMax = 8;
		public int lettersColumn = 1;
		public int promptColumn = 2;
		public int promptMax = 4;
		public int score = 2000;
		public PromptModel[] promptAndAnswers;
		public PromptModel selected;
		public bool[] isLetterSelects;
		public int[] letterButtonsSelected;
		public bool isExitNow = false;
		public bool isAnswerAllNow = false;
		public int answerCount = 0;
		private int tableIndex = 1;

		public void Setup()
		{
			letterButtonTexts = new string[letterMax];
			promptAndAnswers = new PromptModel[promptMax];
			selected = new PromptModel();
			selected.answerTexts = new string[letterMax];
		}

		public void Populate()
		{
			tableIndex = contentIndex + 1;
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
			answerCount = 0;
			for (int p = 0; p < DataUtil.Length(promptAndAnswers); p++)
			{
				var prompt = new PromptModel();
				int index = p * 2 + promptColumn;
				if (index < DataUtil.Length(row))
				{
					prompt.promptText = row[index];
					string answer = row[index + 1];
					prompt.PopulateAnswer(answer, letterMax, empty);
				}
				promptAndAnswers[p] = prompt;
			}
		}

		public void UpdateAnswer()
		{
			string answer = selected.answerText;
			int index;
			bool isAnswerNow = false;
			PromptModel prompt;
			for (index = 0; index < DataUtil.Length(promptAndAnswers); index++)
			{
				prompt = promptAndAnswers[index];
				if (answer == prompt.answerText && answer != empty)
				{
					isAnswerNow = true;
					prompt.RevealAnswer(empty);
					ClearSelected();
				}
				else if (prompt.isAnswerVisibleNow)
				{
					isAnswerNow = true;
					prompt.isAnswerVisibleNow = false;
				}
				else
				{
					prompt.isAnswerVisibleNow = false;
				}
			}
			if (isAnswerNow)
			{
				answerCount = 0;
				isAnswerAllNow = true;
				for (index = 0; index < DataUtil.Length(promptAndAnswers); index++)
				{
					prompt = promptAndAnswers[index];
					if (!prompt.isAnswerVisible && empty != prompt.answerText)
					{
						isAnswerAllNow = false;
					}
					else if (prompt.isAnswerVisible)
					{
						answerCount++;
					}
				}
			}
		}

		public void Update()
		{
			UpdateAnswer();
		}

		private void ClearSelected()
		{
			DataUtil.Clear(selected.answerTexts);
			isLetterSelects = new bool[letterMax];
			letterButtonsSelected = new int[letterMax];
			for (int index = 0; index < DataUtil.Length(letterButtonsSelected); index++)
			{
				isLetterSelects[index] = false;
				letterButtonsSelected[index] = -1;
			}
			selected.answerText = "";
		}

		public bool Toggle(int letterButtonIndex)
		{
			bool isSelectedNext = !(isLetterSelects[letterButtonIndex]);
			string letter = letterButtonTexts[letterButtonIndex];
			int length = DataUtil.Length(selected.answerText);
			if (isSelectedNext)
			{
				selected.answerText += letter;
				selected.answerTexts[length] = letter;
				letterButtonsSelected[length] = letterButtonIndex;
				score--;
				if (score < 0)
				{
					score = 0;
				}
			}
			else
			{
				selected.answerTexts[length] = empty;
				letterButtonsSelected[length] = -1;
				int last = DataUtil.LastIndexOf(selected.answerText, letter);
				if (0 <= last)
				{
					for (int after = last; after < letterMax; after++)
					{
						int select = letterButtonsSelected[after];
						if (-1 != select)
						{
							isLetterSelects[select] = false;
						}
						letterButtonsSelected[after] = -1;
					}
					selected.answerText = StringUtil.Remove(selected.answerText, last);
				}
				else
				{
					DebugUtil.Log("SpellingModel.Toggle: Did not expect <" 
						+ letter + "> was not in <" + selected.answerText + ">");
				}
			}
			isLetterSelects[letterButtonIndex] = isSelectedNext;
			return isSelectedNext;
		}

		public void Hint()
		{
		}

		public void Exit()
		{
			isExitNow = true;
		}
	}
}
