import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;

public class D {

	static BufferedReader in = new BufferedReader(new InputStreamReader(System.in));

	static String readLine() {
		try {
			return in.readLine();
		} catch (IOException ex) {
			return "";
		}
	}

	public static void main(String[] args) {
		var t = Integer.parseInt(readLine());
		var r = new String[t];
		for (int i = 0; i < t; i++)
			r[i] = solve().toString();
		System.out.println(String.join("\n", r));
	}

	static Object solve() {
		var n = Integer.parseInt(readLine());
		var s = readLine().toCharArray();

		var i = 0;
		while (i < n - 1 && s[i] <= s[i + 1]) {
			i++;
		}
		while (i < n - 1 && s[i] >= s[i + 1]) {
			var t = s[i];
			s[i] = s[i + 1];
			s[i + 1] = t;
			i++;
		}
		return new String(s);
	}
}
