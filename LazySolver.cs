using DotNet.Core;
using DotNet.models;
using System;
using System.Linq;

namespace DotNet
{
	public class LazySolver : SolverBase
	{
		private PointPackage _previous;

		public override PointPackage GetNext(GameState state)
		{
			var query = state.GetRemainingPackages(x => 0 - x.WeightClass, x => 0 - (x.Width * x.Length * x.Height)).ToList();
			foreach (var pkg in query)
			{
				var prev = _previous?.AsBox() ?? new Box();

				var aboveLast = pkg.Place(prev.Min.X+1, prev.Min.Y, prev.Max.Z);
				var rightOfLast = pkg.Place(prev.Max.X+1, prev.Min.Y, prev.Min.Z);
				var leftMostOfLastOnNextRow = pkg.Place(0, prev.Max.Y +1, prev.Min.Z);
				var inFrontOfLast = pkg.Place(prev.Min.X, prev.Max.Y +1, prev.Min.Z);


				var alternatives = new[]
				{
					//pkg.Place(_xStart, _yStart, _zStart),
					//pkg.Place(_xStart, _yEnd, _zEnd),
					//pkg.Place(_xEnd, _yStart, _zStart),
					//pkg.Place(_xEnd, _yEnd, _zStart),
					//pkg.Place(_xEnd, _yStart, _zEnd),
					//pkg.Place(_xEnd, _yEnd, _zEnd),

					aboveLast,
					rightOfLast,
					leftMostOfLastOnNextRow,
					inFrontOfLast,
				};

				// Validate
				alternatives = alternatives.Where(p => state.IsCollisionFree(p)).ToArray();

				// todo: randomize order?

				// todo: prioritize weight at bottom

				// todo: prioritize OrderClass (for last packages)

				var chosen = alternatives.FirstOrDefault();
				if (chosen != null)
				{
					_previous = chosen;
					return chosen;
				}
			}

			Console.WriteLine("SOMETHING WENT TERRIBLY WRONG!! ABORT MISSION");
			return null;
		}
	}
}
