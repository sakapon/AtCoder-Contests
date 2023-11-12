using System;
using System.Text;

class D2
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine();

		var sb = new StringBuilder();

		foreach (var c in s)
		{
			if (c == 'C' && sb.Length >= 2 && sb[^1] == 'B' && sb[^2] == 'A')
			{
				sb.Remove(sb.Length - 2, 2);
			}
			else
			{
				sb.Append(c);
			}
		}
		return sb.ToString();
	}
}
