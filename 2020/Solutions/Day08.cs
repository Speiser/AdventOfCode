using System;
using System.Collections.Generic;
using AdventOfCode2020.Solutions.Shared;
using NUnit.Framework;

namespace AdventOfCode2020.Solutions
{
    public class Day08 : ISolution
    {
        public void Solve()
        {
            var input = Input.Lines(nameof(Day08));
            Console.WriteLine(this.Puzzle1(input));
            Console.WriteLine(this.Puzzle2(input));
        }

        private int Puzzle1(string[] operations)
        {
            var console = new GameConsole().LoadProgram(operations);
            console.Run();
            return console.Acc;
        }

        private int Puzzle2(string[] operations)
        {
            var original = new string[operations.Length];
            Array.Copy(operations, original, operations.Length);

            var changedLine = 0;
            var console = new GameConsole().LoadProgram(operations);

            while (!console.Run())
            {
                Array.Copy(original, operations, operations.Length);
                for (; changedLine < operations.Length; changedLine++)
                {
                    var op = operations[changedLine].Split(" ");
                    if (op[0] == "nop")
                    {
                        if (op[1] == "+0")
                        {
                            continue;
                        }
                        operations[changedLine] = operations[changedLine].Replace("nop", "jmp");
                        changedLine++;
                        break;
                    }
                    else if (op[0] == "jmp")
                    {
                        operations[changedLine] = operations[changedLine].Replace("jmp", "nop");
                        changedLine++;
                        break;
                    }
                }
                console.LoadProgram(operations);
            }

            return console.Acc;
        }

        private class GameConsole
        {
            private readonly List<int> executedStatements = new List<int>();

            public int Acc { get; set; }
            public int Ip { get; set; }
            public string[] Operations { get; set; }

            public bool Run()
            {
                for (; this.Ip < this.Operations.Length; this.Ip++)
                {
                    if (this.executedStatements.Contains(this.Ip))
                    {
                        return false;
                    }

                    this.executedStatements.Add(this.Ip);
                    var operation = this.Operations[this.Ip].Split(" ");
                    switch (operation[0])
                    {
                        case "nop":
                            break;
                        case "acc":
                            this.Acc += int.Parse(operation[1]);
                            break;
                        case "jmp":
                            this.Ip += int.Parse(operation[1]) - 1;
                            break;
                    }
                }

                return true;
            }

            public GameConsole LoadProgram(string[] operations)
            {
                this.Operations = operations;
                this.executedStatements.Clear();
                this.Acc = 0;
                this.Ip = 0;
                return this;
            }
        }

        private class Tests
        {
            [Test]
            public void Puzzle1()
            {
                var actual = new Day08().Puzzle1(TestInput);
                Assert.AreEqual(5, actual);
            }

            [Test]
            public void Puzzle2()
            {
                var actual = new Day08().Puzzle2(TestInput);
                Assert.AreEqual(8, actual);
            }

            private readonly string[] TestInput = @"nop +0
acc +1
jmp +4
acc +3
jmp -3
acc -99
acc +1
jmp -4
acc +6".Split("\r\n");
        }
    }
}
