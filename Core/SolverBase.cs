using DotNet.models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace DotNet.Core
{
	public abstract class SolverBase : ISolver
	{
		public static Random Random = new Random();

		protected readonly IList<PointPackage> History = new List<PointPackage>();

		public virtual List<PointPackage> Solve(GameResponse game)
		{
			var solution = new List<PointPackage>();
			var state = new GameState
			{
				Game = game,
				Solution = solution,
			};
			while (solution.Count != game.Dimensions.Count)
			{
				var pkg = GetNext(state);
				if (pkg == null)
				{
					throw new ApplicationException("Missing next package!");
				}
				if (solution.Any(x => x.Id == pkg.Id))
				{
					throw new ApplicationException("Package already added to solution!");
				}

				// todo: validate that the package is placed on the floor or on another package...

				History.Add(pkg);
				solution.Add(pkg);
			}
			return solution;
		}

		public abstract PointPackage GetNext(GameState state);
	}
}
