//
// DataContractSerializerTest_DuplicateQName.cs
//
// Author:
//	David Ferguson <davecferguson@gmail.com>
//
// Copyright (C) 2012 Dell AppAssure http://www.appassure.com
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
//
// This test code contains tests for the DataContractSerializer
// concerning duplicate Qualified Names for the object graph and known types
//
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

using NUnit.Framework;

namespace MonoTests.System.Runtime.Serialization
{
	[TestFixture]
	public class DataContractSerializerTest_NamespaceOrder
	{
		[DataContract(Name = "name", Namespace = "http://somecompany.com/function/api/2010/05")]
        [Serializable]
        public class CustomNamespaceDC
        {
            [DataMember(Name = "name")]
            public double Test1;
        }
	    
        [Test]
		public void TestNamespaceOrderWithCustomNamespace ()
		{
			var serializer = new DataContractSerializer (typeof (CustomNamespaceDC));

			var d4 = new CustomNamespaceDC ();
            var xml = string.Empty;
            using (var ms = new MemoryStream ())
            {
                serializer.WriteObject (ms, d4);
                xml = Encoding.ASCII.GetString (ms.GetBuffer());
                Console.WriteLine (xml);
            }

            var companyNsStart = xml.IndexOf (@"http://somecompany.com", StringComparison.Ordinal);
            var xmlSchemaNsStart = xml.IndexOf (@"http://www.w3.org", StringComparison.Ordinal);

            Assert.Less (companyNsStart, xmlSchemaNsStart, @"A custom namespace comes before the xml schema namespace");
        }
	}
}