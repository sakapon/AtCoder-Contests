import java.util.Scanner;

public class B {
	static Scanner sc = new Scanner(System.in);

	public static void main(String[] args) {
		System.out.println(solve());
	}

	static Object solve() {
		var x = sc.nextInt();
		var y = sc.nextInt();

		var c = 0;
		for (int i = 1; i <= 6; i++)
			for (int j = 1; j <= 6; j++)
				if (i + j >= x || Math.abs(i - j) >= y)
					c++;
		return c / 36.0;
	}
}
