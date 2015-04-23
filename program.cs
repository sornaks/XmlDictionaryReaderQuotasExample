using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace SimpleXMLReader
{
    [DataContract(Name = "DummyClass", Namespace = "")]
    public class DummyClass
    {
        [DataMember]
        public int SampleInt { get; set; }

        [DataMember]
        public SomethingElse SomeClass { get; set; }
    }

    [DataContract(Name = "SomethingElse", Namespace = "")]
    public class SomethingElse
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
                "<DummyClass><SampleInt>10</SampleInt><SomeClass><SampleInt>100</SampleInt>" +
                "<SampleString>HelloWorld</SampleString></SomeClass></DummyClass>";
            var encodedInput = Encoding.UTF8.GetBytes(input);
            using (var reader = XmlDictionaryReader.CreateTextReader(
                new MemoryStream(encodedInput),
                Encoding.UTF8,
                dictionaryQuotas,
                onClose: null))
            {
                var serializer = new DataContractSerializer(typeof(DummyClass));
                var obj = serializer.ReadObject(reader) as DummyClass;

                Console.WriteLine(dictionaryQuotas.MaxDepth);
                Console.WriteLine(obj.SampleInt);
                Console.WriteLine(obj.SomeClass.SampleInt);
                Console.WriteLine(obj.SomeClass.SampleString);
            }
        }
    }
}
