using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static void Main()
	{
		var s = Console.ReadLine();

		var forms = new Stack<int>();
		var ponds = new Stack<KeyValuePair<int, long>>();

		for (int i = 0; i < s.Length; i++)
		{
			if (s[i] == '\\')
			{
				forms.Push(i);
			}
			else if (s[i] == '/')
			{
				if (!forms.Any()) continue;

				var pi = forms.Pop();
				long sum = i - pi;
				while (ponds.Any() && pi < ponds.Peek().Key)
					sum += ponds.Pop().Value;
				ponds.Push(new KeyValuePair<int, long>(pi, sum));
			}
		}

		Console.WriteLine(ponds.Sum(p => p.Value));
		Console.WriteLine(ponds.Any() ? $"{ponds.Count} {string.Join(" ", ponds.Select(p => p.Value).Reverse())}" : "0");
	}
}
