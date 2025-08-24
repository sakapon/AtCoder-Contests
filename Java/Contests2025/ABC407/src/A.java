import java.util.Scanner;

public class A {
	static Scanner sc = new Scanner(System.in);

	public static void main(String[] args) {
		System.out.println(solve());
	}

	static Object solve() {
		var a = sc.nextDouble();
		var b = sc.nextInt();

		return Math.round(a / b);
	}
}
