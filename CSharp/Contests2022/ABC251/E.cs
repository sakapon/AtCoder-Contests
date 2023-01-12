using System;

class E
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();

		// 0: なし、1: あり
		var dp0 = new long[n];
		var dp1 = new long[n];

		// a[n-1]: なし
		dp0[0] = 1L << 50;
		dp1[0] = a[0];
		Run();
		var r = dp1[n - 2];

		// a[n-1]: あり
		Array.Clear(dp0, 0, n);
		Array.Clear(dp1, 0, n);
		dp0[0] = a[n - 1];
		dp1[0] = a[0] + a[n - 1];
		Run();
		ChFirstMin(ref r, dp0[n - 2]);
		ChFirstMin(ref r, dp1[n - 2]);

		return r;

		void Run()
		{
			for (int i = 1; i < n - 1; i++)
			{
				dp0[i] = dp1[i - 1];
				dp1[i] = FirstMin(dp0[i - 1], dp1[i - 1]) + a[i];
			}
		}
	}

	public static T FirstMin<T>(T o1, T o2) where T : IComparable<T> => o1.CompareTo(o2) > 0 ? o2 : o1;
	public static void ChFirstMin<T>(ref T o1, T o2) where T : IComparable<T> { if (o1.CompareTo(o2) > 0) o1 = o2; }
}
