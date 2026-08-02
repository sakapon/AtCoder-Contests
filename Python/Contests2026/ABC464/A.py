s = input()

w = sum(c == "W" for c in s)
r = 2 * w < len(s)
print("East" if r else "West")
