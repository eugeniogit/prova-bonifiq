namespace ProvaPub.Services
{
	public class RandomService
	{
		int seed;
		public RandomService()
		{
			seed = Guid.NewGuid().GetHashCode();
		}
		public int GetRandom()
		{
			var seed = Guid.NewGuid().GetHashCode();
			return new Random(seed).Next(100);
		}

	}
}
