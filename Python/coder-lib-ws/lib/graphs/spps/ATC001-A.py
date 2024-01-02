# https://atcoder.jp/contests/atc001/tasks/dfs_a

from unweightedgraph import CharUnweightedGrid

h, w = map(int, input().split())
s = [input() for _ in range(h)]

grid = CharUnweightedGrid(s)
sv = grid.find_vertex_id('s')
ev = grid.find_vertex_id('g')

r = grid.shortest_by_bfs(sv, ev)
print("Yes" if r[ev] < 1 << 30 else "No")
