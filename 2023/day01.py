lines = [l.strip() for l in open('Input/day01.txt')]


def p1():
    s = 0
    for line in lines:
        l = ''
        r = ''
        for i in range(len(line)):
            if line[i].isdigit():
                l = line[i]
                break
        for i in reversed(range(len(line))):
            if line[i].isdigit():
                r = line[i]
                break
        s += int(l + r)
    print(s)


def p2():
    rep = [
        ('one', 'one1one'),
        ('two', 'two2two'),
        ('three', 'three3three'),
        ('four', 'four4four'),
        ('five', 'five5five'),
        ('six', 'six6six'),
        ('seven', 'seven7seven'),
        ('eight', 'eight8eight'),
        ('nine', 'nine9nine'),
    ]
    s = 0
    for line in lines:
        for (f, r) in rep:
            line = line.replace(f, r)
        l = ''
        r = ''
        for i in range(len(line)):
            if line[i].isdigit():
                l = line[i]
                break
        for i in reversed(range(len(line))):
            if line[i].isdigit():
                r = line[i]
                break
        s += int(l + r)
    print(s)


p1()
p2()
