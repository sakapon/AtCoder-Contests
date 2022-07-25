using System;
using System.Text;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, c) = Read2();
		var sb = new StringBuilder();

		// 0: なし
		// 1: 0 に変更
		// 2: 1 に変更
		// 3: 逆転
		var op = new int[30];

		while (n-- > 0)
		{
			var (t, a) = Read2();

			for (int i = 0; i < 30; i++)
			{
				var f = (a & (1 << i)) != 0;

				if (t == 1)
				{
					if (!f) op[i] = 1;
				}
				else if (t == 2)
				{
					if (f) op[i] = 2;
				}
				else
				{
					if (f) op[i] ^= 3;
				}

				if (op[i] == 1)
				{
					c &= ~(1 << i);
				}
				else if (op[i] == 2)
				{
					c |= 1 << i;
				}
				else if (op[i] == 3)
				{
					c ^= 1 << i;
				}
			}

			sb.Append(c).AppendLine();
		}
		Console.Write(sb);
	}
}
