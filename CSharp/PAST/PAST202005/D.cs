using System;
using System.Linq;

class D
{
	const string Numbers = @"
.###..#..###.###.#.#.###.###.###.###.###
.#.#.##....#...#.#.#.#...#.....#.#.#.#.#
.#.#..#..###.###.###.###.###...#.###.###
.#.#..#..#.....#...#...#.#.#...#.#.#...#
.###.###.###.###...#.###.###...#.###.###";

	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var numbers = Numbers.Trim().Split(new[] { "\n", "\r\n" }, StringSplitOptions.None);
		var d = Enumerable.Range(0, 10).ToDictionary(i => GetShape(numbers, i), i => i.ToString()[0]);

		var n = int.Parse(Console.ReadLine());
		var s = new int[5].Select(_ => Console.ReadLine()).ToArray();
		Console.WriteLine(new string(Enumerable.Range(0, n).Select(i => d[GetShape(s, i)]).ToArray()));
	}

	static string GetShape(string[] target, int i) => string.Join("", target.Select(s => s.Substring(4 * i, 4)));
}
