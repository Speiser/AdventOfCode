using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2015.Solutions.Shared;

namespace AdventOfCode2015.Solutions
{
    public class Day07 : ISolution
    {
        public void Solve()
        {
            var input = Input.Lines(nameof(Day07));
            Console.WriteLine(this.Puzzle1(input));
            Console.WriteLine(this.Puzzle2(input));
        }

        private uint Puzzle1(string[] input) => GetWireValue(Parse(input), "a");

        private uint Puzzle2(string[] input)
        {
            var instructions = Parse(input);
            var copy = instructions.ToList();

            var a = GetWireValue(instructions, "a");
            (copy.Single(x => x.OutputGate == "b") as UnaryInstruction).Value = a.ToString();
            return GetWireValue(copy, "a");
        }

        private class Instruction
        {
            public string OutputGate { get; set; }
            public string Operator { get; set; }
        }

        private class UnaryInstruction : Instruction
        {
            public string Value { get; set; }
        }

        private class BinaryInstruction : Instruction
        {
            public string Left { get; set; }
            public string Right { get; set; }
        }

        private static bool TryAddValue(Instruction current, Dictionary<string, uint> wires)
        {
            switch (current)
            {
                case UnaryInstruction unary:
                    if (!wires.TryGetValue(unary.Value, out var value))
                    {
                        if (!uint.TryParse(unary.Value, out value))
                        {
                            return false;
                        }
                    }

                    wires.Add(unary.OutputGate, current.Operator switch
                    {
                        "_" => value,
                        "NOT" => ~value,
                        _ => throw new InvalidProgramException()
                    });
                    return true;

                case BinaryInstruction binary:
                    if (!wires.TryGetValue(binary.Left, out var left))
                    {
                        if (!uint.TryParse(binary.Left, out left))
                        {
                            return false;
                        }
                    }
                    if (!wires.TryGetValue(binary.Right, out var right))
                    {
                        if (!uint.TryParse(binary.Right, out right))
                        {
                            return false;
                        }
                    }

                    wires.Add(binary.OutputGate, current.Operator switch
                    {
                        "AND" => left & right,
                        "OR" => left | right,
                        "LSHIFT" => left << (int)right,
                        "RSHIFT" => left >> (int)right,
                        _ => throw new InvalidProgramException()
                    });
                    return true;
            }
            throw new InvalidProgramException();
        }

        private static List<Instruction> Parse(string[] input)
        {
            var ins = new List<Instruction>();
            foreach (var line in input)
            {
                var split = line.Split(" -> ");
                var leftSplit = split[0].Split(" ");

                if (leftSplit.Length == 1)
                {
                    ins.Add(new UnaryInstruction
                    {
                        Operator = "_",
                        OutputGate = split[1],
                        Value = leftSplit[0]
                    });
                    continue;
                }

                if (leftSplit[0] == "NOT")
                {
                    ins.Add(new UnaryInstruction
                    {
                        Operator = "NOT",
                        OutputGate = split[1],
                        Value = leftSplit[1]
                    });
                    continue;
                }

                ins.Add(new BinaryInstruction { Operator = leftSplit[1], OutputGate = split[1], Left = leftSplit[0], Right = leftSplit[2] });
            }

            return ins;
        }
        
        private static uint GetWireValue(List<Instruction> instructions, string wire)
        {
            var wires = new Dictionary<string, uint>();

            while (true)
            {
                var evalAgain = new List<Instruction>();

                foreach (var instruction in instructions)
                {
                    if (!TryAddValue(instruction, wires))
                        evalAgain.Add(instruction);

                    if (wires.TryGetValue(wire, out var value))
                        return value;
                }

                instructions = evalAgain;
            }
        }
    }
}
