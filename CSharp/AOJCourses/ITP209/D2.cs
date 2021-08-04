using System;

class D2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();
		var m = int.Parse(Console.ReadLine());
		var b = Read();

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		for (int i = 0, j = 0; i < n || j < m;)
		{
			if (j == m)
			{
				Console.WriteLine(a[i++]);
			}
			else if (i == n)
			{
				Console.WriteLine(b[j++]);
			}
			else
			{
				if (a[i] < b[j])
				{
					Console.WriteLine(a[i++]);
				}
				else if (a[i] > b[j])
				{
					Console.WriteLine(b[j++]);
				}
				else
				{
					i++;
					j++;
				}
			}
		}
		Console.Out.Flush();
	}
}
