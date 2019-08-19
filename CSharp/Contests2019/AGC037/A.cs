using System;

class A
{
	static void Main()
	{
		var s = Console.ReadLine();

		var r = 0;
		var c = ' ';
		var is2 = false;
		var i = 0;
		for (; i < s.Length; i++)
		{
			if (is2 || c != s[i])
			{
				is2 = false;
				c = s[i];
			}
			else
			{
				is2 = true;
				i++;
			}
			r++;
		}
		if (i > s.Length) r--;
		Console.WriteLine(r);
	}
}
