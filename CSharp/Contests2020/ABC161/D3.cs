using System;
using System.Linq;

class D3
{
	static void Main()
	{
		var k = int.Parse(Console.ReadLine()) - 1;

		long[] Next(long[] a) => a.SelectMany(x => Enumerable.Range(0, 10).Where(d => Math.Abs(x % 10 - d) <= 1).Select(d => x * 10 + d)).ToArray();

		var lun = new long[10][];
		var lun0 = Enumerable.Range(1, 9).Select(i => (long)i).ToArray();
		Console.WriteLine(Enumerable.Range(0, 10).SelectMany(i => lun[i] = i > 0 ? Next(lun[i - 1]) : lun0).ElementAt(k));
	}
}
