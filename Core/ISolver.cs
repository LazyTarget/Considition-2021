using DotNet.models;
using System.Collections.Generic;

namespace DotNet.Core
{
	public interface ISolver
	{
		List<PointPackage> Solve(GameResponse game);
	}
}
