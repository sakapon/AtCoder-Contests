using System;

class D2
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Array.ConvertAll(new bool[n], _ => Console.ReadLine());

		Array.Sort(s, (x, y) =>
		{
			var (x2, y2) = (x.TrimStart('0'), y.TrimStart('0'));
			if (x2.Length != y2.Length) return x2.Length < y2.Length ? -1 : 1;

			for (int i = 0; i < x2.Length; i++)
				if (x2[i] != y2[i]) return x2[i] < y2[i] ? -1 : 1;

			return -x.Length.CompareTo(y.Length);
		});
		Console.WriteLine(string.Join("\n", s));
	}
}
