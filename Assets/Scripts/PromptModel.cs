using Finegamedesign.Utils;

namespace Finegamedesign.CityOfWords
{
	public sealed class PromptModel
	{
		public string[] answerTexts = new string[0];
		public string answerText = "";
		public bool isAnswerVisible = false;
		public bool isAnswerVisibleNow = false;
		public string promptText = "";

		public void PopulateAnswer(string answer, int letterMax, string empty)
		{
			answerTexts = new string[letterMax];
			answerText = answer;
			int letter;
			for (letter = 0; letter < DataUtil.Length(answer); letter++)
			{
				answerTexts[letter] = empty;
			}
			for (; letter < letterMax; letter++)
			{
				answerTexts[letter] = empty;
			}
			isAnswerVisible = false;
			isAnswerVisibleNow = false;
		}

		public void RevealAnswer(string empty)
		{
			isAnswerVisible = true;
			isAnswerVisibleNow = true;
			string answer = answerText;
			int letter;
			for (letter = 0; letter < DataUtil.Length(answer); letter++)
			{
				answerTexts[letter] = answer[letter].ToString();
			}
			for (; letter < DataUtil.Length(answerTexts); letter++)
			{
				answerTexts[letter] = empty;
			}
		}
	}
}
