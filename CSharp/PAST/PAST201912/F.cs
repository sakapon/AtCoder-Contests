using System;
using System.Linq;

class F
{
	static void Main()
	{
		var s = Console.ReadLine();
		var u = s.Select((c, i) => new { c, i }).Where(_ => char.IsUpper(_.c)).ToArray();
		Console.WriteLine(string.Join("", Enumerable.Range(0, u.Length / 2).Select(k => s.Substring(u[2 * k].i, u[2 * k + 1].i - u[2 * k].i + 1)).OrderBy(x => x)));
	}
}
