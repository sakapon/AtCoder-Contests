using System;

class B
{
	static void Main() => Console.WriteLine(Solve() ? "correct" : "incorrect");
	static bool Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Array.ConvertAll(new bool[n], _ => Console.ReadLine());

		var cor = new[] { "WL", "LW", "DD" };

		for (int i = 0; i < n; i++)
		{
			for (int j = i + 1; j < n; j++)
			{
				var s = $"{a[i][j]}{a[j][i]}";
				if (Array.IndexOf(cor, s) == -1) return false;
			}
		}
		return true;
	}
}
