using DotNet.models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace DotNet.Core
{
	public abstract class SolverBase : ISolver
	{
		public virtual List<PointPackage> Solve(GameResponse game)
		{
			var solution = new List<PointPackage>();
			while (solution.Count != game.Dimensions.Count)
			{
				var pkg = GetNext(game, solution);
				if (pkg == null)
				{
					throw new ApplicationException("Missing next package!");
				}
				if (solution.Any(x => x.Id == pkg.Id))
				{
					throw new ApplicationException("Package already added to solution!");
				}

				solution.Add(pkg);
			}
			return solution;
		}

		public abstract PointPackage GetNext(GameResponse game, List<PointPackage> state);
	}
}
