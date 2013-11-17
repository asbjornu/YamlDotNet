//  This file is part of YamlDotNet - A .NET library for YAML.
//  Copyright (c) 2008, 2009, 2010, 2011, 2012, 2013 Antoine Aubry
    
//  Permission is hereby granted, free of charge, to any person obtaining a copy of
//  this software and associated documentation files (the "Software"), to deal in
//  the Software without restriction, including without limitation the rights to
//  use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
//  of the Software, and to permit persons to whom the Software is furnished to do
//  so, subject to the following conditions:
    
//  The above copyright notice and this permission notice shall be included in all
//  copies or substantial portions of the Software.
    
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//  SOFTWARE.

using Xunit;
using FluentAssertions;
using YamlDotNet.Core;
using YamlDotNet.Core.Tokens;

namespace YamlDotNet.Test.Core
{
	public class ScannerTests : TokenHelper
	{
		[Fact]
		public void VerifyTokensOnExample1()
		{
			AssertSequenceOfTokensFrom(ScannerFor("01-directives.yaml"),
				StreamStart,
				VersionDirective(1, 1),
				TagDirective("!", "!foo"),
				TagDirective("!yaml!", "tag:yaml.org,2002:"),
				DocumentStart,
				StreamEnd);
		}

		[Fact]
		public void VerifyTokensOnExample2()
		{
			AssertSequenceOfTokensFrom(ScannerFor("02-scalar-in-imp-doc.yaml"),
				StreamStart,
				SingleQuotedScalar("a scalar"),
				StreamEnd);
		}

		[Fact]
		public void VerifyTokensOnExample3()
		{
			Scanner scanner = ScannerFor("03-scalar-in-exp-doc.yaml");
			AssertSequenceOfTokensFrom(scanner,
				StreamStart,
				DocumentStart,
				SingleQuotedScalar("a scalar"),
				DocumentEnd,
				StreamEnd);
		}

		[Fact]
		public void VerifyTokensOnExample4()
		{
			AssertSequenceOfTokensFrom(ScannerFor("04-scalars-in-multi-docs.yaml"),
				StreamStart,
				SingleQuotedScalar("a scalar"),
				DocumentStart,
				SingleQuotedScalar("another scalar"),
				DocumentStart,
				SingleQuotedScalar("yet another scalar"),
				StreamEnd);
		}		
 		
		[Fact]
		public void VerifyTokensOnExample5()
		{
			AssertSequenceOfTokensFrom(ScannerFor("05-circular-sequence.yaml"),
				StreamStart,
				Anchor("A"),
				FlowSequenceStart,
				AnchorAlias("A"),
				FlowSequenceEnd,
				StreamEnd);
		}

		[Fact]
		public void VerifyTokensOnExample6()
		{
			AssertSequenceOfTokensFrom(ScannerFor("06-float-tag.yaml"),
				StreamStart,
				Tag("!!", "float"),
				DoubleQuotedScalar("3.14"),
				StreamEnd);
		}

		[Fact]
		public void VerifyTokensOnExample7()
		{
			AssertSequenceOfTokensFrom(ScannerFor("07-scalar-styles.yaml"),
				StreamStart,
				DocumentStart,
				DocumentStart,
				PlainScalar("a plain scalar"),
				DocumentStart,
				SingleQuotedScalar("a single-quoted scalar"),
				DocumentStart,
				DoubleQuotedScalar("a double-quoted scalar"),
				DocumentStart,
				LiteralScalar("a literal scalar"),
				DocumentStart,
				FoldedScalar("a folded scalar"),
				StreamEnd);
		}

		[Fact]
		public void VerifyTokensOnExample8()
		{
			AssertSequenceOfTokensFrom(ScannerFor("08-flow-sequence.yaml"),
				StreamStart,
				FlowSequenceStart,
				PlainScalar("item 1"),
				FlowEntry,
				PlainScalar("item 2"),
				FlowEntry,
				PlainScalar("item 3"),
				FlowSequenceEnd,
				StreamEnd);
		}

		[Fact]
		public void VerifyTokensOnExample9()
		{
			AssertSequenceOfTokensFrom(ScannerFor("09-flow-mapping.yaml"),
				StreamStart,
				FlowMappingStart,
				Key,
				PlainScalar("a simple key"),
				Value,
				PlainScalar("a value"),
				FlowEntry,
				Key,
				PlainScalar("a complex key"),
				Value,
				PlainScalar("another value"),
				FlowEntry,
				FlowMappingEnd,
				StreamEnd);
		}

		[Fact]
		public void VerifyTokensOnExample10()
		{
			AssertSequenceOfTokensFrom(ScannerFor("10-mixed-nodes-in-sequence.yaml"),
				StreamStart,
				BlockSequenceStart,
				BlockEntry,
				PlainScalar("item 1"),
				BlockEntry,
				PlainScalar("item 2"),
				BlockEntry,
				BlockSequenceStart,
				BlockEntry,
				PlainScalar("item 3.1"),
				BlockEntry,
				PlainScalar("item 3.2"),
				BlockEnd,
				BlockEntry,
				BlockMappingStart,
				Key,
				PlainScalar("key 1"),
				Value,
				PlainScalar("value 1"),
				Key,
				PlainScalar("key 2"),
				Value,
				PlainScalar("value 2"),
				BlockEnd,
				BlockEnd,
				StreamEnd);
		}

		[Fact]
		public void VerifyTokensOnExample11()
		{
			AssertSequenceOfTokensFrom(ScannerFor("11-mixed-nodes-in-mapping.yaml"),
				StreamStart,
				BlockMappingStart,
				Key,
				PlainScalar("a simple key"),
				Value,
				PlainScalar("a value"),
				Key,
				PlainScalar("a complex key"),
				Value,
				PlainScalar("another value"),
				Key,
				PlainScalar("a mapping"),
				Value,
				BlockMappingStart,
				Key,
				PlainScalar("key 1"),
				Value,
				PlainScalar("value 1"),
				Key,
				PlainScalar("key 2"),
				Value,
				PlainScalar("value 2"),
				BlockEnd,
				Key,
				PlainScalar("a sequence"),
				Value,
				BlockSequenceStart,
				BlockEntry,
				PlainScalar("item 1"),
				BlockEntry,
				PlainScalar("item 2"),
				BlockEnd,
				BlockEnd,
				StreamEnd);
		}
	
		[Fact]
		public void VerifyTokensOnExample12()
		{
			AssertSequenceOfTokensFrom(ScannerFor("12-compact-sequence.yaml"),
				StreamStart,
				BlockSequenceStart,
				BlockEntry,
				BlockSequenceStart,
				BlockEntry,
				PlainScalar("item 1"),
				BlockEntry,
				PlainScalar("item 2"),
				BlockEnd,
				BlockEntry,
				BlockMappingStart,
				Key,
				PlainScalar("key 1"),
				Value,
				PlainScalar("value 1"),
				Key,
				PlainScalar("key 2"),
				Value,
				PlainScalar("value 2"),
				BlockEnd,
				BlockEntry,
				BlockMappingStart,
				Key,
				PlainScalar("complex key"),
				Value,
				PlainScalar("complex value"),
				BlockEnd,
				BlockEnd,
				StreamEnd);
		}
			
		[Fact]
		public void VerifyTokensOnExample13()
		{
			AssertSequenceOfTokensFrom(ScannerFor("13-compact-mapping.yaml"),
				StreamStart,
				BlockMappingStart,
				Key,
				PlainScalar("a sequence"),
				Value,
				BlockSequenceStart,
				BlockEntry,
				PlainScalar("item 1"),
				BlockEntry,
				PlainScalar("item 2"),
				BlockEnd,
				Key,
				PlainScalar("a mapping"),
				Value,
				BlockMappingStart,
				Key,
				PlainScalar("key 1"),
				Value,
				PlainScalar("value 1"),
				Key,
				PlainScalar("key 2"),
				Value,
				PlainScalar("value 2"),
				BlockEnd,
				BlockEnd,
				StreamEnd);
		}
			
		[Fact]
		public void VerifyTokensOnExample14()
		{
			AssertSequenceOfTokensFrom(ScannerFor("14-mapping-wo-indent.yaml"),
				StreamStart,
				BlockMappingStart,
				Key,
				PlainScalar("key"),
				Value,
				BlockEntry,
				PlainScalar("item 1"),
				BlockEntry,
				PlainScalar("item 2"),
				BlockEnd,
				StreamEnd);
		}

		private Scanner ScannerFor(string name) {
			return new Scanner(Yaml.StreamFrom(name));
		}

		private void AssertSequenceOfTokensFrom(Scanner scanner, params Token[] tokens)
		{
			var tokenNumber = 1;
			foreach (var expected in tokens)
			{
				scanner.MoveNext().Should().BeTrue("Missing token number {0}", tokenNumber);
				AssertToken(expected, scanner.Current, tokenNumber);
				tokenNumber++;
			}
			scanner.MoveNext().Should().BeFalse("Found extra tokens");
		}

		private void AssertToken(Token expected, Token actual, int tokenNumber)
		{
			Dump.WriteLine(expected.GetType().Name);
			actual.Should().NotBeNull();
			actual.GetType().Should().Be(expected.GetType(), "Token {0} is not of the expected type", tokenNumber);

			foreach (var property in expected.GetType().GetProperties())
			{
				if (property.PropertyType != typeof(Mark) && property.CanRead)
				{
					var value = property.GetValue(actual, null);
					var expectedValue = property.GetValue(expected, null);
					Dump.WriteLine("\t{0} = {1}", property.Name, value);
					value.Should().Be(expectedValue, "Comparing property {0} in token {1}", property.Name, tokenNumber);
				}
			}
		}
	}
}