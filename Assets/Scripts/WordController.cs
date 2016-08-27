using Finegamedesign.Utils;

namespace Finegamedesign.CityOfWords
{
	public sealed class WordController
	{
		public string[][] Load()
		{
			string csv = StringUtil.Read("words.csv");
			return StringUtil.ParseCsv(csv);
		}
	}
}
