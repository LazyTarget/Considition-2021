namespace DotNet.Core
{
	public struct Point3D
	{
		public Point3D(int x, int y, int z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		public int X { get; set; }
		public int Y { get; set; }
		public int Z { get; set; }

		public static Point3D operator +(Point3D a, Point3D b)
		{
			return new Point3D(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
		}

		public override string ToString()
		{
			return $"{{ {X}, {Y}, {Z} }}";
		}
	}
}
