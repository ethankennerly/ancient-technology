using Finegamedesign.Utils;

namespace Finegamedesign.CityOfWords
{
	[System.Serializable]
	public sealed class MainController
	{
		public MainView view;
		public BuildingController building = new BuildingController();
		public SpellingController spelling = new SpellingController();

		public void Setup()
		{
			if (null == view)
			{
				view = (MainView) SceneNodeView.FindObjectOfType(typeof(MainView));
				view.Setup();
			}
			spelling.Setup();
			spelling.Populate();
			building.model.cellCount = DataUtil.Length(spelling.model.table) - 1;
			building.Setup();
		}

		public void Update()
		{
			spelling.Update();
			building.Update();
			AnimationView.SetState(view.state, building.model.state);
		}
	}
}
