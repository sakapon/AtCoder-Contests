using System;
using System.Text.RegularExpressions;

class J
{
	static void Main()
	{
		var s = Console.ReadLine();

		while (s.Contains('('))
		{
			s = Regex.Replace(s, @"\(([a-z]*)\)", m =>
			{
				var v = m.Groups[1].Value;
				var r = v.ToCharArray();
				Array.Reverse(r);
				return v + new string(r);
			});
		}
		Console.WriteLine(s);
	}
}
