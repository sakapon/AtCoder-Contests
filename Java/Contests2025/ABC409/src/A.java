import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;

public class A {

	static BufferedReader in = new BufferedReader(new InputStreamReader(System.in));

	static String readLine() {
		try {
			return in.readLine();
		} catch (IOException ex) {
			return "";
		}
	}

	public static void main(String[] args) {
		System.out.println(solve() ? "Yes" : "No");
	}

	static boolean solve() {
		var n = Integer.parseInt(readLine());
		var t = readLine();
		var a = readLine();

		for (int i = 0; i < n; i++)
			if (t.charAt(i) == 'o' && a.charAt(i) == 'o')
				return true;
		return false;
	}
}
