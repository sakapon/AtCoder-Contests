class B
{
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var nx = Console.ReadLine().Split();
		var n = int.Parse(nx[0]);
		var x = nx[1][0] - 'A';
		var ss = Array.ConvertAll(new bool[n], _ => Console.ReadLine());

		return ss.Any(s => s[x] == 'o');
	}
}
