using System;
using System.Linq;

class C
{
	static void Main()
	{
		Console.WriteLine(13);
		var s = NewArray2<char>(13, 13);

		for (int k = 0; k < 7; k++)
		{
			var k2 = k * 2;
			var c = (char)('a' + k);
			for (int j = 0; k2 + j < 13; j++) s[k2 + j][j] = c;
		}
		for (int k = 1; k < 7; k++)
		{
			var k2 = k * 2;
			var c = (char)('m' + k);
			for (int i = 0; k2 + i < 13; i++) s[i][k2 + i] = c;
		}

		// 残りは手動で、C.txt
		Console.WriteLine(string.Join("\n", s.Select(t => new string(t))));
	}

	static T[] NewArrayF<T>(int n, Func<T> newItem)
	{
		var a = new T[n];
		for (int i = 0; i < n; ++i) a[i] = newItem();
		return a;
	}

	static T[][] NewArray2<T>(int n1, int n2) => NewArrayF(n1, () => new T[n2]);
}
