namespace DotNet.Core
{
	public struct Point3D
	{
		public static readonly Point3D Zero = new Point3D(0, 0, 0);

		public static Point3D Diagonal(int diag) => new Point3D(diag, diag, diag);

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

		public static bool operator ==(Point3D a, Point3D b)
		{
			return a.X == b.X && a.Y == b.Y && a.Z == a.Z;
		}

		public static bool operator !=(Point3D a, Point3D b)
		{
			return !(a.X == b.X && a.Y == b.Y && a.Z == a.Z);
		}

		public override string ToString()
		{
			return $"{{ {X}, {Y}, {Z} }}";
		}
	}
}
