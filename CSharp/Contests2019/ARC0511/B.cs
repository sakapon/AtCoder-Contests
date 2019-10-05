using System;

class B
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

		var c = 0;
		var rM = h[3] / h[0];
		for (int r = 0; r <= rM; r++)
		{
			var n2 = h[3] - r * h[0];
			var gM = n2 / h[1];
			for (int g = 0; g <= gM; g++)
			{
				var n3 = n2 - g * h[1];
				if (n3 % h[2] == 0) c++;
			}
		}
		Console.WriteLine(c);
	}
}
