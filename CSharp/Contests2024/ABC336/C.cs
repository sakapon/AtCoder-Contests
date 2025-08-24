using CoderLib8.Numerics;

class C
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = long.Parse(Console.ReadLine());

		n--;
		var s = n.ConvertAsString(5);
		return long.Parse(s) * 2;
	}
}
