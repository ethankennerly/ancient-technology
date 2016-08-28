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
			if (building.model.isSelectNow)
			{
				building.model.isSelectNow = false;
				spelling.model.contentIndex = building.model.selectedIndex;
				spelling.Populate();
			}
			else if (spelling.model.isExitNow)
			{
				if (1 <= spelling.model.answerCount)
				{
					building.model.UnlockAdjacent();
				}
				spelling.model.isExitNow = false;
				building.model.state = "spellingToBuilding";
			}
			else if (spelling.model.isAnswerAllNow)
			{
				spelling.model.isAnswerAllNow = false;
				building.model.state = "spellingToBuilding";
				building.model.Complete();
			}
			AnimationView.SetState(view.state, building.model.state);
		}
	}
}
