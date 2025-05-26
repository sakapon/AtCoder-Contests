import java.util.Scanner;

class A {

	static Scanner sc = new Scanner(System.in);

	public static void main(String[] args) {
		System.out.println(solve() ? "Yes" : "No");
	}

	static boolean solve() {
		var a = sc.nextInt();
		var b = sc.nextInt();
		var c = sc.nextInt();
		var d = sc.nextInt();
		return 60 * a + b > 60 * c + d;
	}

}
