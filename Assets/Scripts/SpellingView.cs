using UnityEngine;
using Finegamedesign.Utils;

namespace Finegamedesign.CityOfWords
{
	public sealed class SpellingView : MonoBehaviour
	{
		public GameObject helpText;
		public GameObject topicText;
		public GameObject exitButton;
		public GameObject[] letterButtons;
		public GameObject[] letterButtonTexts;
		public PromptView[] promptAndAnswers;
		public PromptView selected;
	}
}
