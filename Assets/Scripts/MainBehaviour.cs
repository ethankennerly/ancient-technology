using UnityEngine;

namespace Finegamedesign.CityOfWords
{
	public sealed class MainBehaviour : MonoBehaviour
	{
		public MainController controller = new MainController();

		public void Start()
		{
			controller.Setup();
		}

		public void Update()
		{
			controller.Update();
		}
	}
}
