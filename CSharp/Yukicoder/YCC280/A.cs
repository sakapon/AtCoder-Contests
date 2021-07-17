using System;

class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var a = Read();
		var b = Read();

		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				(a[i], b[j]) = (b[j], a[i]);
				if (IsKadomatsu(a) && IsKadomatsu(b)) return true;
				(a[i], b[j]) = (b[j], a[i]);
			}
		}
		return false;
	}

	static bool IsKadomatsu(int[] a) => a[0] != a[2] && (a[0] < a[1] && a[1] > a[2] || a[0] > a[1] && a[1] < a[2]);
}
