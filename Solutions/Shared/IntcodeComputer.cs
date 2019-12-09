using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions.Shared
{
    internal class IntcodeComputer
    {
        private int relativeBase = 0;
        private int ip = 0;

        public IntcodeComputer(long[] memory, params long[] inputs)
        {
            foreach (var input in inputs)
            {
                this.InputStream.Enqueue(input);
            }

            // Extend program memory
            this.Memory = memory.Concat(new long[1000]).ToArray();
        }

        public ConcurrentQueue<long> InputStream { get; } = new ConcurrentQueue<long>();
        public long[] Memory { get; }

        public event Action<long> OnOutput;

        public static IntcodeComputer LoadProgramFromString(string input, params long[] inputs)
        {
            var program = input.Split(',').Select(long.Parse).ToArray();
            return new IntcodeComputer(program, inputs);
        }
        public static IntcodeComputer LoadProgramFromFile(string file, params long[] inputs)
        {
            var fileContent = File.ReadAllText(file);
            return LoadProgramFromString(fileContent, inputs);
        }

        public async Task EvaluateProgram()
        {
            while (true)
            {
                switch (this.FetchOpcode())
                {
                    case Opcode.Add:
                        this.Write(3, this.Read(1) + this.Read(2));
                        this.ip += 4;
                        break;
                    case Opcode.Multiply:
                        this.Write(3, this.Read(1) * this.Read(2));
                        this.ip += 4;
                        break;
                    case Opcode.Input:
                        this.Write(1, await this.GetInput());
                        this.ip += 2;
                        break;
                    case Opcode.Output:
                        this.OnOutput?.Invoke(this.Read(1));
                        this.ip += 2;
                        break;
                    case Opcode.JumpIfTrue:
                        this.ip = this.Read(1) != 0 ? (int)this.Read(2) : this.ip + 3;
                        break;
                    case Opcode.JumpIfFalse:
                        this.ip = this.Read(1) == 0 ? (int)this.Read(2) : this.ip + 3;
                        break;
                    case Opcode.LessThan:
                        this.Write(3, this.Read(1) < this.Read(2) ? 1 : 0);
                        this.ip += 4;
                        break;
                    case Opcode.Equals:
                        this.Write(3, this.Read(1) == this.Read(2) ? 1 : 0);
                        this.ip += 4;
                        break;
                    case Opcode.AdjustRelativeBase:
                        this.relativeBase += (int)this.Read(1);
                        this.ip += 2;
                        break;
                    case Opcode.Halt:
                        return;
                }
            }
        }

        private async Task<long> GetInput()
        {
            while (!this.InputStream.Any())
            {
                await Task.Delay(1);
            }

            if (!this.InputStream.TryDequeue(out var input))
            {
                throw new Exception("Threading...");
            }

            return input;
        }
        private Opcode FetchOpcode() => (Opcode)(this.Memory[this.ip] % 100);
        private long Read(int position) => this.Memory[this.GetIndex(position)];
        private void Write(int position, long value) => this.Memory[this.GetIndex(position)] = value;

        private int GetIndex(int position)
        {
            var mode = this.GetParameterMode(position);

            var index = this.Memory[this.ip + position];
            if (mode == 1)
            {
                index = this.ip + position;
            }
            else if (mode == 2)
            {
                index = this.relativeBase + this.Memory[this.ip + position];
            }

            return (int)index;
        }

        private int GetParameterMode(int position)
        {
            var mode = (int)this.Memory[this.ip] / 100;
            for (var i = 1; i < position; i++)
                mode /= 10;
            return mode % 10;
        }

        private enum Opcode
        {
            Add = 1,
            Multiply = 2,
            Input = 3,
            Output = 4,
            JumpIfTrue = 5,
            JumpIfFalse = 6,
            LessThan = 7,
            Equals = 8,
            AdjustRelativeBase = 9,
            Halt = 99
        }
    }
}
