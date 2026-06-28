n, x = input().split()
n = int(n)
x = "ABCDE".find(x[0])
ss = [input() for _ in range(n)]

r = any(s[x] == 'o' for s in ss)
print("Yes" if r else "No")
