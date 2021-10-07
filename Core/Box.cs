namespace DotNet.Core
{
	public struct Box
	{
		public Point3D Min { get; set; }
		public Point3D Max { get; set; }

		public void MoveX(int x)
		{
			Min += new Point3D(x, 0, 0);
			Max += new Point3D(x, 0, 0);
		}

		public void MoveY(int y)
		{
			Min += new Point3D(0, y, 0);
			Max += new Point3D(0, y, 0);
		}
		public void MoveZ(int z)
		{
			Min += new Point3D(0, 0, z);
			Max += new Point3D(0, 0, z);
		}
	}
}
