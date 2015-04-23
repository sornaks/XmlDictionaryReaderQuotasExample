using System;
using System.Runtime.Serialization;
using System.Xml;
using System.Text;
using System.IO;

namespace XmlDictionaryReaderQuotasExample
{
	[DataContract(Name = "DummyClass", Namespace = "")]
    public class DummyClass
    {
        [DataMember]
        public int SampleInt { get; set; }

        [DataMember]
        public string SampleString { get; set; }
    }

	public class Program
	{
		public static void Main()
		{
			var dictionaryQuotas = new XmlDictionaryReaderQuotas();
			dictionaryQuotas.MaxDepth = 1;
			var input = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                "<DummyClass><SampleInt>10</SampleInt>" +
                "<SampleString>HelloWorld</SampleString></DummyClass>";
			var encodedInput = Encoding.UTF8.GetBytes(input);
			var reader = XmlDictionaryReader.CreateTextReader(
				new MemoryStream(encodedInput),
				Encoding.UTF8,
				dictionaryQuotas,
				onClose: null);

			var serializer = new DataContractSerializer(typeof(DummyClass));
			var obj = serializer.ReadObject(reader);

			Console.WriteLine((obj as DummyClass).SampleInt);
			Console.WriteLine((obj as DummyClass).SampleString);
		}
	}
}
