using System.Numerics;

class B2
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		return BitOperations.Log2((uint)(n & -n));
	}
}
