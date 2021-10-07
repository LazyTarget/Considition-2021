using DotNet.Core;
using DotNet.models;
using System.Linq;

namespace DotNet
{
	public class LazySolver : SolverBase
	{
		private int _xStart;
		private int _xEnd;
		private int _yStart;
		private int _yEnd;
		private int _zStart;
		private int _zEnd;

		public override PointPackage GetNext(GameState state)
		{
			foreach (var pkg in state.GetRemainingPackages())
			{
				var alternatives = new[]
				{
					pkg.Place(_xStart, _yStart, _zStart),
					pkg.Place(_xStart, _yEnd, _zEnd),
					pkg.Place(_xEnd, _yStart, _zStart),
					pkg.Place(_xEnd, _yEnd, _zStart),
					pkg.Place(_xEnd, _yStart, _zEnd),
					pkg.Place(_xEnd, _yEnd, _zEnd),
				};

				// Validate
				alternatives = alternatives.Where(p => state.IsCollisionFree(p)).ToArray();

				// todo: randomize order?

				var chosen = alternatives.FirstOrDefault();
				if (chosen != null)
				{
					return chosen;
				}
			}
			return null;
		}
	}
}
