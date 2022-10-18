using System;
using System.Collections.Generic;

namespace RPNCalulator {
	public class RPN {
		private Stack<int> _operators;
		Dictionary<string, Func<int, int, int>> _operationFunction;

		public int EvalRPN(string input) {
			_operationFunction = new Dictionary<string, Func<int, int, int>>
			{
				["+"] = (fst, snd) => (fst + snd),
				["-"] = (fst, snd) => (fst - snd),
				["*"] = (fst, snd) => (fst * snd),
				["/"] = (fst, snd) => (fst / snd),
				["!"] = (fst, snd) =>
				{
					int v = Factorial(snd);
					return v;
				}
			};
			_operators = new Stack<int>();

			var splitInput = input.Split(' ');
			foreach (var op in splitInput)
			{
				if (IsNumber(op))
				{
					if (op.Substring(0, 1) == "D")
					{
						_operators.Push(Int32.Parse(op.Substring(1, op.Length-1)));
					}
					if (op.Substring(0, 1) == "B")
					{
						int number = 0, c = (int)Math.Pow(2,op.Length - 2);
						foreach (char digit in op.Substring(1, op.Length-1))
						{
							switch (digit)
							{
								case '0':
								case '1':
									number += Int32.Parse(digit + "") * c;
									break;
							}
							c /= 2;
						}
						_operators.Push(number);
					}
					if (op.Substring(0, 1) == "#")
					{
						int number = 0, c = (int)Math.Pow(16, op.Length - 2);
						foreach (var digit in op.Substring(1, op.Length - 1))
						{
							switch (digit)
							{
								case '0':
								case '1':
								case '2':
								case '3':
								case '4':
								case '5':
								case '6':
								case '7':
								case '8':
								case '9':
									number += Int32.Parse(digit + "") * c;
									break;
								case 'A':
									number += 10 * c;
									break;
								case 'B':
									number += 11 * c;
									break;
								case 'C':
									number += 12 * c;
									break;
								case 'D':
									number += 13 * c;
									break;
								case 'E':
									number += 14 * c;
									break;
								case 'F':
									number += 15 * c;
									break;
							}
							c /= 16;
						}
						_operators.Push(number);
					}
				}
				else
				if (IsOperator(op))
				{
					var num1 = _operators.Pop();
					var num2 = _operators.Pop();
					_operators.Push(_operationFunction[op](num1, num2));
					//_operators.Push(Operation(op)(num1, num2));
				}
				else
				if (IsOperatorUnary(op))
				{
					var num1 = _operators.Pop();
					_operators.Push(_operationFunction[op](0, num1));
				}
			}

			var result = _operators.Pop();
			if (_operators.IsEmpty)
			{
				return result;
			}
			throw new InvalidOperationException();
		}
		private int Factorial(int f)
		{
			int fact = 1;
			for (int i = 1; i <= f; i++)
			{
				fact = fact * i;
			}
			return fact;
		}

		private bool IsNumber(String input) {
			if (input.Substring(0, 1) == "B") {
				foreach (var digit in input.Substring(1, input.Length - 1))
				{
					switch (digit)
					{
						case '0':
						case '1':
							return true;
						default:
							return false;
					}
				}
			}
			else if(input.Substring(0, 1) ==  "D")
			{
				foreach (var digit in input.Substring(1, input.Length - 1)) {
					switch (digit)
					{
						case '0':
						case '1':
						case '2':
						case '3':
						case '4':
						case '5':
						case '6':
						case '7':
						case '8':
						case '9':
							return true;
						default:
							return false;
					}
				}
			}
			else if(input.Substring(0, 1) == "#")
			{
				foreach (var digit in input.Substring(1, input.Length - 1))
				{
					switch (digit)
					{
						case '0':
						case '1':
						case '2':
						case '3':
						case '4':
						case '5':
						case '6':
						case '7':
						case '8':
						case '9':
						case 'A':
						case 'B':
						case 'C':
						case 'D':
						case 'E':
						case 'F':
							return true;
						default:
							return false;
					}
				}
			}
			return false;
		}

		private bool IsOperator(String input) =>
			input.Equals("+") || input.Equals("-") ||
			input.Equals("*") || input.Equals("/");

		private bool IsOperatorUnary(String input) =>
			input.Equals("!");

		private Func<int, int, int> Operation(String input) =>
			(x, y) =>
			(
				(input.Equals("+") ? x + y :
					(input.Equals("*") ? x * y : int.MinValue)
				)
			);

	}
}