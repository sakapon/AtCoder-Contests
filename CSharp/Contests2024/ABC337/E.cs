using System.Numerics;
using CoderLib8.Collections;

class E
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());

		var m = n;
		while (m != (m & -m)) m += m & -m;
		m = BitOperations.Log2((uint)m);

		var ls = Array.ConvertAll(new bool[m], _ => new List<uint>());

		for (uint i = 0; i < n; i++)
		{
			foreach (var j in i.ToElements(m))
			{
				ls[j].Add(i + 1);
			}
		}

		Console.WriteLine(m);
		foreach (var l in ls)
		{
			Console.WriteLine($"{l.Count} " + string.Join(" ", l));
		}

		var s = Console.ReadLine();
		s = new string(s.Reverse().ToArray());
		return Convert.ToInt32(s, 2) + 1;
	}
}
