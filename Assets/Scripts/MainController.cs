using Finegamedesign.Utils;

namespace Finegamedesign.CityOfWords
{
	[System.Serializable]
	public sealed class MainController
	{
		public SpellingController spelling = new SpellingController();

		public void Setup()
		{
			spelling.Setup();
			spelling.Populate();
		}

		public void Update()
		{
			spelling.Update();
		}
	}
}
