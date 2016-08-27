using Finegamedesign.Utils;

namespace Finegamedesign.CityOfWords
{
	public sealed class MainController
	{
		public SpellingController spelling = new SpellingController();

		public void Setup()
		{
			spelling.Setup();
		}

		public void Update()
		{
			spelling.Update();
		}
	}
}
