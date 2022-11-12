using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ss = Array.ConvertAll(new bool[n], _ => Console.ReadLine());

		const string C0 = "HDCS";
		const string C1 = "A23456789TJQK";

		return ss.All(s => C0.Contains(s[0]) && C1.Contains(s[1])) && ss.Distinct().Count() == n;
	}
}
