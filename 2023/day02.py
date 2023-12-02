lines = [l.strip() for l in open('Input/day02.txt')]


def p1():
    mr = 12
    mg = 13
    mb = 14
    res = 0
    for line in lines:
        game, content = line.split(": ")
        possible = True
        for turn in content.split("; "):
            possible = True
            for draw in turn.split(", "):
                if (r := get_amount('red', draw)) > -1:
                    if r > mr:
                        possible = False
                        break
                    continue
                if (g := get_amount('green', draw)) > -1:
                    if g > mg:
                        possible = False
                        break
                    continue
                if (b := get_amount('blue', draw)) > -1:
                    if b > mb:
                        possible = False
                        break
                    continue
            if not possible:
                break
        if possible:
            res += int(game.replace('Game ', ''))
    print(res)


def p2():
    res = 0
    for line in lines:
        mr = 0
        mg = 0
        mb = 0
        _, content = line.split(": ")
        for turn in content.split("; "):
            for draw in turn.split(", "):
                if (r := get_amount('red', draw)) > -1:
                    if r > mr:
                        mr = r
                    continue
                if (g := get_amount('green', draw)) > -1:
                    if g > mg:
                        mg = g
                    continue
                if (b := get_amount('blue', draw)) > -1:
                    if b > mb:
                        mb = b
                    continue
        res += mr * mg * mb
    print(res)


def get_amount(color: str, s: str) -> int:
    color = f' {color}'
    if not s.endswith(color):
        return -1

    return int(s.replace(color, ''))


p1()
p2()
