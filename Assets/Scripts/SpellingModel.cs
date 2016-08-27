using Finegamedesign.Utils;

namespace Finegamedesign.CityOfWords
{
	public sealed class SpellingModel
	{
		public SpellingView view;
		public string[][] table;
		public int tableIndex = 1;
		public string empty = "";
		public string[] letterButtonTexts;
		public int letterMax = 8;
		public int lettersColumn = 1;

		public void Setup()
		{
			letterButtonTexts = new string[letterMax];
		}

		public void Populate()
		{
			string letters = table[tableIndex][lettersColumn];
			for (int index = 0; index < DataUtil.Length(letterButtonTexts); index++)
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
	}
}
