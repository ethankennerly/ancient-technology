using Finegamedesign.Utils;

namespace Finegamedesign.CityOfWords
{
	public sealed class SpellingController
	{
		public SpellingView view;

		public void Setup()
		{
			view = (SpellingView) SceneNodeView.FindObjectOfType(typeof(SpellingView));
		}
	}
}
