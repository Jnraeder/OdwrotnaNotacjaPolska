using NUnit.Framework;
using System;
using RPNCalulator;

namespace RPNTest {
	[TestFixture]
	public class RPNTest {
		private RPN _sut;
		[SetUp]
		public void Setup() {
			_sut = new RPN();
		}


		//TESTY OGÓLNE


		[Test]
		public void CheckIfTestWorks() {
			Assert.Pass();
		}

		[Test]
		public void CheckIfCanCreateSut() {
			Assert.That(_sut, Is.Not.Null);
		}

		[Test]
		public void WrongDigit_ThrowsException()
		{
			Assert.Throws<InvalidOperationException>(() => _sut.EvalRPN("Q2"));
		}


		//TESTY BINARNE


		[Test]
		public void Binary_WrongDigit_ThrowsException()
		{
			Assert.Throws<InvalidOperationException>(() => _sut.EvalRPN("B2"));
		}

		[Test]
		public void Binary_SingleDigitOneInputOneReturn()
		{
			var result = _sut.EvalRPN("B1");

			Assert.That(result, Is.EqualTo(1));
		}

		[Test]
		public void Binary_TwoDigitsNumberInputNumberReturn()
		{
			var result = _sut.EvalRPN("B10");

			Assert.That(result, Is.EqualTo(2));
		}

		[Test]
		public void Binary_TwoNumbersGivenWithoutOperator_ThrowsExcepton()
		{
			Assert.Throws<InvalidOperationException>(() => _sut.EvalRPN("B1 B10"));
		}

		[Test]
		public void Binary_OperatorPlus_AddingTwoNumbers_ReturnCorrectResult()
		{
			var result = _sut.EvalRPN("B1 B10 +");

			Assert.That(result, Is.EqualTo(3));
		}

		[Test]
		public void Binary_OperatorTimes_AddingTwoNumbers_ReturnCorrectResult()
		{
			var result = _sut.EvalRPN("B10 B10 *");

			Assert.That(result, Is.EqualTo(4));
		}

		[Test]
		public void Binary_OperatorMinus_SubstractingTwoNumbers_ReturnCorrectResult()
		{
			var result = _sut.EvalRPN("B1 B10 -");

			Assert.That(result, Is.EqualTo(1));
		}

		[Test]
		public void Binary_OperatorSlash_SubstractingTwoNumbers_ReturnCorrectResult()
		{
			var result = _sut.EvalRPN("B10 B101 /");

			Assert.That(result, Is.EqualTo(2));
		}

		[Test]
		public void Binary_OperatorSlash_DividingByZero_ThrowException()
		{
			Assert.Throws<DivideByZeroException>(() => _sut.EvalRPN("B0 B1110 /"));
		}

		[Test]
		public void Binary_OperatorDivision_DividerBiggerThanDividend_ReturnCorrectResult()
		{
			var result = _sut.EvalRPN("B110 B10 /");

			Assert.That(result, Is.EqualTo(0));
		}

		[Test]
		public void Binary_OperatorSilnia_CalculatingTwoNumbers_ReturnCorrectResult()
		{
			var result = _sut.EvalRPN("B11 !");

			Assert.That(result, Is.EqualTo(6));
		}

		[Test]
		public void Binary_OperatorSilnia_CalculatingZero_ReturnCorrectResult()
		{
			var result = _sut.EvalRPN("B0 !");

			Assert.That(result, Is.EqualTo(1));
		}

		[Test]
		public void Binary_OperatorSilnia_CalculatingTwoNumbers_ThrowsException()
		{
			Assert.Throws<InvalidOperationException>(() => _sut.EvalRPN("B11 B110 !"));
		}

		[Test]
		public void Binary_ComplexExpression()
		{
			var result = _sut.EvalRPN("B1111 B111 B1 B1 + - / B11 * B10 B1 B1 + + -");

			Assert.That(result, Is.EqualTo(4));
		}


		//TESTY DZIESIĘTNE


		[Test]
		public void Decimal_WrongDigit_ThrowsException()
		{
			Assert.Throws<InvalidOperationException>(() => _sut.EvalRPN("DG"));
		}

		[Test]
		public void Decimal_SingleDigitOneInputOneReturn() {
			var result = _sut.EvalRPN("D1");

			Assert.That(result, Is.EqualTo(1));
		}

		[Test]
		public void Decimal_SingleDigitOtherThenOneInputNumberReturn() {
			var result = _sut.EvalRPN("D2");

			Assert.That(result, Is.EqualTo(2));
		}

		[Test]
		public void Decimal_TwoDigitsNumberInputNumberReturn() {
			var result = _sut.EvalRPN("D12");

			Assert.That(result, Is.EqualTo(12));
		}

		[Test]
		public void Decimal_TwoNumbersGivenWithoutOperator_ThrowsExcepton() {
			Assert.Throws<InvalidOperationException>(() => _sut.EvalRPN("D1 D2"));
		}

		[Test]
		public void Decimal_OperatorPlus_AddingTwoNumbers_ReturnCorrectResult() {
			var result = _sut.EvalRPN("D1 D2 +");

			Assert.That(result, Is.EqualTo(3));
		}

		[Test]
		public void Decimal_OperatorTimes_AddingTwoNumbers_ReturnCorrectResult() {
			var result = _sut.EvalRPN("D2 D2 *");

			Assert.That(result, Is.EqualTo(4));
		}

		[Test]
		public void Decimal_OperatorMinus_SubstractingTwoNumbers_ReturnCorrectResult() {
			var result = _sut.EvalRPN("D1 D2 -");

			Assert.That(result, Is.EqualTo(1));
		}

		[Test]
		public void Decimal_OperatorSlash_SubstractingTwoNumbers_ReturnCorrectResult()
		{
			var result = _sut.EvalRPN("D1 D2 /");

			Assert.That(result, Is.EqualTo(2));
		}

		[Test]
		public void Decimal_OperatorSlash_DividingByZero_ThrowException()
		{
			Assert.Throws<DivideByZeroException>(() => _sut.EvalRPN("D0 D14 /"));
		}

		[Test]
		public void Decimal_OperatorDivision_DividerBiggerThanDividend_ReturnCorrectResult()
		{
			var result = _sut.EvalRPN("D6 D2 /");

			Assert.That(result, Is.EqualTo(0));
		}

		[Test]
		public void Decimal_OperatorSilnia_CalculatingTwoNumbers_ReturnCorrectResult()
		{
			var result = _sut.EvalRPN("D3 !");

			Assert.That(result, Is.EqualTo(6));
		}

		[Test]
		public void Decimal_OperatorSilnia_CalculatingZero_ReturnCorrectResult()
		{
			var result = _sut.EvalRPN("D0 !");

			Assert.That(result, Is.EqualTo(1));
		}

		[Test]
		public void Decimal_OperatorSilnia_CalculatingTwoNumbers_ThrowsException()
		{
			Assert.Throws<InvalidOperationException>(() => _sut.EvalRPN("D3 D6 !"));
		}

		[Test]
		public void Decimal_ComplexExpression() {
			var result = _sut.EvalRPN("D15 D7 D1 D1 + - / D3 * D2 D1 D1 + + -");

			Assert.That(result, Is.EqualTo(4));
		}


		//TESTY SZESNASTKOWE


		[Test]
		public void HexaDecimal_WrongDigit_ThrowsException()
		{
			Assert.Throws<InvalidOperationException>(() => _sut.EvalRPN("#W"));
		}

		[Test]
		public void HexaDecimal_SingleDigitOneInputOneReturn()
		{
			var result = _sut.EvalRPN("#C");

			Assert.That(result, Is.EqualTo(12));
		}

		[Test]
		public void HexaDecimal_TwoDigitsNumberInputNumberReturn()
		{
			var result = _sut.EvalRPN("#11");

			Assert.That(result, Is.EqualTo(17));
		}

		[Test]
		public void HexaDecimal_TwoNumbersGivenWithoutOperator_ThrowsExcepton()
		{
			Assert.Throws<InvalidOperationException>(() => _sut.EvalRPN("#C #F"));
		}

		[Test]
		public void HexaDecimal_OperatorPlus_AddingTwoNumbers_ReturnCorrectResult()
		{
			var result = _sut.EvalRPN("#7 #D +");

			Assert.That(result, Is.EqualTo(20));
		}

		[Test]
		public void HexaDecimal_OperatorTimes_AddingTwoNumbers_ReturnCorrectResult()
		{
			var result = _sut.EvalRPN("#2 #C *");

			Assert.That(result, Is.EqualTo(24));
		}

		[Test]
		public void HexaDecimal_OperatorMinus_SubstractingTwoNumbers_ReturnCorrectResult()
		{
			var result = _sut.EvalRPN("#1 #A -");

			Assert.That(result, Is.EqualTo(9));
		}

		[Test]
		public void HexaDecimal_OperatorSlash_SubstractingTwoNumbers_ReturnCorrectResult()
		{
			var result = _sut.EvalRPN("#B #16 /");

			Assert.That(result, Is.EqualTo(2));
		}

		[Test]
		public void HexaDecimal_OperatorSlash_DividingByZero_ThrowException()
		{
			Assert.Throws<DivideByZeroException>(() => _sut.EvalRPN("#0 #F /"));
		}

		[Test]
		public void HexaDecimal_OperatorDivision_DividerBiggerThanDividend_ReturnCorrectResult()
		{
			var result = _sut.EvalRPN("#F #E /");

			Assert.That(result, Is.EqualTo(0));
		}

		[Test]
		public void HexaDecimal_OperatorSilnia_CalculatingTwoNumbers_ReturnCorrectResult()
		{
			var result = _sut.EvalRPN("#5 !");

			Assert.That(result, Is.EqualTo(120));
		}

		[Test]
		public void HexaDecimal_OperatorSilnia_CalculatingZero_ReturnCorrectResult()
		{
			var result = _sut.EvalRPN("#0 !");

			Assert.That(result, Is.EqualTo(1));
		}

		[Test]
		public void HexaDecimal_OperatorSilnia_CalculatingTwoNumbers_ThrowsException()
		{
			Assert.Throws<InvalidOperationException>(() => _sut.EvalRPN("@B @A0 !"));
		}

		[Test]
		public void HexaDecimal_ComplexExpression()
		{
			var result = _sut.EvalRPN("#F #7 #1 #1 + - / #3 * #2 #1 #1 + + -");

			Assert.That(result, Is.EqualTo(4));
		}


	}
}