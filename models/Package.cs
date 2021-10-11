namespace DotNet.models
{
	public class Package
	{
		public int Id { get; set; }
		public int Length { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		public int WeightClass { get; set; }
		public int OrderClass { get; set; }

		public override string ToString()
		{
			return $"#{Id}  \t:: Width(X): {Width}, Length(Y): {Length}, Height(Z): {Height}, wc: {WeightClass}, oc: {OrderClass}";
		}
	}
}