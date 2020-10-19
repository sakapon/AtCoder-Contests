using System;
using System.Linq;

class A
{
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select(_ => Solve())));
	static int Solve()
	{
		var s = Console.ReadLine();
		if (s.All(c => c == 'a')) return -1;
		if ("atcoder".CompareTo(s) < 0) return 0;
		var si = Enumerable.Range(0, s.Length).First(i => s[i] != 'a');
		return s[si] < 'u' ? si : si - 1;
	}
}
