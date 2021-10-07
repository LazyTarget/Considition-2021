using System.Collections.Generic;

namespace DotNet.models
{
	public class GameState
	{
		public GameResponse Game { get; set; }
		public Vehicle Vehicle => Game.Vehicle;
		public List<PointPackage> Solution { get; set; }
	}
}
