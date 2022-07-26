using System;
using System.Text;

class E2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, c) = Read2();
		var sb = new StringBuilder();

		// 00: 0 に変更
		// 10: 逆転
		// 01: なし
		// 11: 1 に変更
		var op = new int[30];
		Array.Fill(op, 2);
		static bool Act(int op, bool b) => ((b ? 2 : 1) & op) != 0;

		while (n-- > 0)
		{
			var (t, a) = Read2();

			for (int i = 0; i < 30; i++)
			{
				var f = 1 << i;
				var af = (a & f) != 0;

				if (t == 1)
				{
					if (!af) op[i] = 0;
				}
				else if (t == 2)
				{
					if (af) op[i] = 3;
				}
				else
				{
					if (af) op[i] ^= 3;
				}

				if (Act(op[i], (c & f) != 0))
				{
					c |= f;
				}
				else
				{
					c &= ~f;
				}
			}

			sb.Append(c).AppendLine();
		}
		Console.Write(sb);
	}
}
