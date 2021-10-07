﻿using DotNet.Core;
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
				var greedy = GetNext_Greedy(pkg, state);
				if (greedy != null)
					return greedy;

				var ex = GetNext_Exhaustive(pkg, state);
				if (ex != null)
					return ex;
			}

			Console.WriteLine("SOMETHING WENT TERRIBLY WRONG!! ABORT MISSION");
			return null;
		}

		private PointPackage GetNext_Greedy(Package pkg, GameState state)
		{
			var prev = _previous?.AsBox() ?? new Box();

			var aboveLast = pkg.Place(prev.Min.X + 1, prev.Min.Y, prev.Max.Z);
			var rightOfLast = pkg.Place(prev.Max.X + 1, prev.Min.Y, prev.Min.Z);
			var leftMostOfLastOnNextRow = pkg.Place(0, prev.Max.Y + 1, prev.Min.Z);
			var inFrontOfLast = pkg.Place(prev.Min.X, prev.Max.Y + 1, prev.Min.Z);


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
			alternatives = alternatives.Where(p => state.IsValidPlacement(p)).ToArray();

			// todo: randomize order?

			// todo: prioritize weight at bottom

			// todo: prioritize OrderClass (for last packages)

			var chosen = alternatives.FirstOrDefault();
			if (chosen != null)
			{
				_previous = chosen;
				return chosen;
			}
			else
			{
				Console.WriteLine("Could not find target (greedy)");
				return null;
			}
		}

		private PointPackage GetNext_Exhaustive(Package pkg, GameState state)
		{
			var maxX = state.Vehicle.Width - pkg.Width;
			var maxY = state.Vehicle.Length - pkg.Length;
			var maxZ = state.Vehicle.Height - pkg.Height;

			for (var y = 0; y < maxY; y++)
			{
				for (var x = 0; x < maxX; x++)
				{
					for (var z = 0; z < maxZ; z++)
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
