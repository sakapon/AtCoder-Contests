# https://atcoder.jp/contests/atc002/tasks/abc007_3

from unweightedgraph import CharUnweightedGrid

h, w = map(int, input().split())
si, sj = map(int, input().split())
ei, ej = map(int, input().split())
s = [input() for _ in range(h)]

grid = CharUnweightedGrid(s)

si -= 1; sj -= 1
ei -= 1; ej -= 1
sv = grid.to_vertex_id(si, sj)
ev = grid.to_vertex_id(ei, ej)

r = grid.shortest_by_bfs(sv, ev)
print(r[ev])
