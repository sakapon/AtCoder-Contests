using System;

class A
{
	static (string, string) ReadS2() { var a = Console.ReadLine().Split(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (m1, n1) = ReadS2();
		var (m2, n2) = ReadS2();
		return m1 == m2 || n1 == n2;
	}
}
