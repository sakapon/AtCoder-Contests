using System.Text.RegularExpressions;

class C
{
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var s = Console.ReadLine();
		var t = Console.ReadLine().ToLower();

		var p2 = $@"\w*{t[0]}\w*{t[1]}\w*";
		var p3 = $@"\w*{t[0]}\w*{t[1]}\w*{t[2]}\w*";
		return Regex.IsMatch(s, p3) || t[2] == 'x' && Regex.IsMatch(s, p2);
	}
}
