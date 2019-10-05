using System;
using System.Linq;

class C
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var s = new int[n].Select(_ => Console.ReadLine().Replace("AB", "!")).ToArray();

		var c1 = s.Sum(x => x.Count(c => c == '!'));
		var c2 = Math.Min(s.Count(x => x[0] == 'B'), s.Count(x => x.Last() == 'A'));
		if (c2 == n) c2--;
		Console.WriteLine(c1 + c2);
	}
}
