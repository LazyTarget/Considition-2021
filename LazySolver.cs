using DotNet.Core;
using DotNet.models;
using System;
using System.Linq;

namespace DotNet
{
	public class LazySolver : SolverBase
	{
		public override PointPackage GetNext(GameState state)
		{
			var prev = History.LastOrDefault();
			var prevY = prev?.AsBox().Min.Y ?? 0;
			var query = state.GetRemainingPackages(x => 0 - x.WeightClass, x => 0 - (x.Width * x.Length * x.Height)).ToList();
			foreach (var pkg in query)
			{
				var greedy = GetNext_GreedyBasedOnPrevious(pkg, prev, state);
				if (greedy != null)
					return greedy;

				// try with the second most previous, if not Zero
				PointPackage prePrev = null;
				if (History.Count > 1 && (prePrev = History.Reverse().Skip(1).FirstOrDefault()) != null)
				{
					if (prePrev.AsBox().Max != Point3D.Zero)
					{
						greedy = GetNext_GreedyBasedOnPrevious(pkg, prePrev, state);
						if (greedy != null)
							return greedy;
					}
				}

				var percent = 0.04d;
				while (percent > 0)
				{
					var ex = GetNext_Exhaustive(pkg, state, prePrev?.AsBox().Min.Y ?? prevY, percent);
					if (ex != null)
						return ex;
					else
					{
						percent -= 0.02d;
						Console.WriteLine($"Increasing exhaustive granularity to: {(1-percent):P0}");
					}
				}
			}


			// Last fallback!
			query = state.GetRemainingPackages(x => 0 - x.WeightClass, x => 0 - (x.Width * x.Length * x.Height)).ToList();
			var nextPkg = query.First();
			var nex = GetNext_Exhaustive(nextPkg, state, 0, 0.01d);
			if (nex != null)
				return nex;

			Console.WriteLine("SOMETHING WENT TERRIBLY WRONG!! ABORT MISSION");
			return null;
		}

		private PointPackage GetNext_GreedyBasedOnPrevious(Package pkg, PointPackage previous, GameState state)
		{
			var prev = previous?.AsBox() ?? new Box();

			var aboveLast = pkg.Place(prev.Min.X, prev.Min.Y, prev.Max.Z + 1);
			var newStackOnTheRight = pkg.Place(prev.Max.X + 1, prev.Min.Y, 0);
			var leftMostOfLastOnNextRow = pkg.Place(0, prev.Max.Y + 1, prev.Min.Z);
			var inFrontOfLast = pkg.Place(prev.Max.X + 1, prev.Max.Y + 1, 0);


			var alternatives = new[]
			{
					//pkg.Place(_xStart, _yStart, _zStart),
					//pkg.Place(_xStart, _yEnd, _zEnd),
					//pkg.Place(_xEnd, _yStart, _zStart),
					//pkg.Place(_xEnd, _yEnd, _zStart),
					//pkg.Place(_xEnd, _yStart, _zEnd),
					//pkg.Place(_xEnd, _yEnd, _zEnd),

					aboveLast,
					newStackOnTheRight,
					leftMostOfLastOnNextRow,
					inFrontOfLast,
				};

			// Validate
			alternatives = alternatives.Where(p => state.IsValidPlacement(p)).ToArray();

			// todo: randomize order?

			// todo: prioritize weight at bottom

			// todo: prioritize OrderClass (for last packages)

			var chosen = alternatives.FirstOrDefault();
			if (chosen != null)
			{
				return chosen;
			}
			else
			{
				//Console.WriteLine("Could not find target (greedy)");
				return null;
			}
		}

		private PointPackage GetNext_Exhaustive(Package pkg, GameState state, int prevY = 0, double percent = 0.05d)
		{
			Console.WriteLine($"GetNext_Exhaustive Package={pkg}");
			var maxX = state.Vehicle.Width - pkg.Width;
			var maxY = state.Vehicle.Length - pkg.Length;
			var maxZ = state.Vehicle.Height - pkg.Height;

			var incX = Math.Min(5, (int)Math.Round(maxX * percent));
			var incY = Math.Min(5, (int)Math.Round(maxX * percent));
			var incZ = Math.Min(5, (int)Math.Round(maxX * percent));

			for (var y = prevY; y < maxY; y += incY)
			{
				Console.WriteLine($"Exhaustive Y={y}, X={0}, Z={0}");
				for (var x = 0; x < maxX; x += incX)
				{
					for (var z = 0; z < maxZ; z += incZ)
					{
						var target = pkg.Place(x, y, z);
						var valid = state.IsValidPlacement(target);
						if (valid)
							return target;
					}
				}
			}

			Console.WriteLine("Could not find target (exhaustive)");
			return null;
		}
	}
}
