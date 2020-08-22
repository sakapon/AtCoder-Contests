using System;
using System.Linq;

class D3
{
	static void Main()
	{
		var k = int.Parse(Console.ReadLine());

		long[] Next(long[] a) => a.SelectMany(l => Enumerable.Range((int)(l % 10) - 1, 3).Where(d => d >= 0 && d <= 9).Select(d => l * 10 + d)).ToArray();

		var lun = new long[10][];
		lun[0] = Enumerable.Range(1, 9).Select(i => (long)i).ToArray();
		Console.WriteLine(Enumerable.Range(0, 10).SelectMany(i => i > 0 ? lun[i] = Next(lun[i - 1]) : lun[i]).ElementAt(k - 1));
	}
}
