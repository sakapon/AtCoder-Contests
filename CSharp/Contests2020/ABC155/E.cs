using System;

class E
{
	static void Main()
	{
		var n = Array.ConvertAll($"0{Console.ReadLine()}".ToCharArray(), x => x - '0');
		var r = 0;
		for (int i = n.Length - 1; i >= 0; i--)
			if (n[i] < 5 || n[i] == 5 && n[i - 1] < 5)
			{
				r += n[i];
			}
			else
			{
				r += 10 - n[i];
				n[i - 1]++;
			}
		Console.WriteLine(r);
	}
}
