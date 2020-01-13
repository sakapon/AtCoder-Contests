using System;
using System.Linq;

class A
{
	static void Main()
	{
		n = int.Parse(Console.ReadLine());
		var s = new int[n].Select(_ => Console.ReadLine()).ToArray();
		var t = new int[n].Select(_ => Console.ReadLine()).ToArray();

		var s1 = Rotate(s);
		var s2 = Rotate(s1);
		var s3 = Rotate(s2);

		var m = Diff(s, t);
		m = Math.Min(m, Diff(s1, t) + 1);
		m = Math.Min(m, Diff(s2, t) + 2);
		m = Math.Min(m, Diff(s3, t) + 1);
		Console.WriteLine(m);
	}

	static int n;
	static string[] Rotate(string[] s) => Enumerable.Range(0, n).Select(i => new string(Enumerable.Range(0, n).Select(j => s[n - 1 - j][i]).ToArray())).ToArray();
	static int Diff(string[] s, string[] t) => s.Zip(t, (x, y) => Enumerable.Range(0, n).Count(j => x[j] != y[j])).Sum();
}
