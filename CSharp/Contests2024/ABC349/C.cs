using System.Text.RegularExpressions;

class C
{
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var s = Console.ReadLine();
		var t = Console.ReadLine().ToLower();

		var p2 = $"{t[0]}.*{t[1]}";
		var p3 = $"{t[0]}.*{t[1]}.*{t[2]}";
		return Regex.IsMatch(s, p3) || t[2] == 'x' && Regex.IsMatch(s, p2);
	}
}
