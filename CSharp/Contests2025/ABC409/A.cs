class A
{
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var t = Console.ReadLine();
		var a = Console.ReadLine();

		return t.Zip(a, (x, y) => x == 'o' && y == 'o').Any(b => b);
	}
}
