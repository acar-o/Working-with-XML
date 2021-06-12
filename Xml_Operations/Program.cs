using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Xml_Operations
{
    class Program
    {
        //Xml Sample
        const string path = "Z://xmlFile.xml";
        static void Main(string[] args)
        {
            SerializeObjectToXmlAndWriteIt();
            DeserializeXmlToObject();
            LoadXmlAsDOM();
            ReadAndParseXml();
        }


        private static void SerializeObjectToXmlAndWriteIt()
        {
            //create an object of Person.
            //For serialization, Person class should be public and
            //it must have non-parameterized constructor

            Person person = new();
            person.Firstname = "Jack";
            person.Lastname = "Slaughter";

            XmlSerializer serializer = new(typeof(Person));

            //serialization can be performed with a stream
            using (StreamWriter streamWriter = new(path))
            {
                serializer.Serialize(streamWriter, person);
            }

        }
        private static void DeserializeXmlToObject()
        {
            //There is an existing XMl file in the driver.
            //We want to read it and create an object with it.
            //First, we need to declare an object to be initialized later.

            Person person;

            //Then, a serializer in type of Person
            XmlSerializer serializer = new(typeof(Person));

            //a reader
            using (XmlReader reader = XmlReader.Create(path))
            {
                person = (Person)serializer.Deserialize(reader);
            }

            //to check it
            Console.WriteLine(person.Firstname + " " + person.Lastname);
        }
        private static void LoadXmlAsDOM()
        {
            XmlDocument xmlDocument = new();

            //to get somewhere else
            xmlDocument.Load(path);

            //to create new
            /*
             * document.LoadXml("<?xml version='1.0' ?>" +
             * "<book genre='novel' ISBN='1-861001-57-5'>" +
             * "<title>Pride And Prejudice</title>" +
             * "</book>"); 
             */

            //to test 
            Console.WriteLine(xmlDocument.OuterXml);
        }
        private static void ReadAndParseXml()
        {
            //Create an object of XmlTextReader with path.
            XmlTextReader reader = new(path);

            //read till the end
            while (reader.Read())
            {
                //Element or attribute
                switch (reader.NodeType)
                {

                    case XmlNodeType.XmlDeclaration://it is for declaration tag
                        Console.WriteLine("<?" + reader.Name);
                        while (reader.MoveToNextAttribute())//Read the attributes if exist.
                        {
                            Console.WriteLine(" " + reader.Name + "='" + reader.Value + "'");
                        }
                        Console.WriteLine("?>");
                        break;

                    case XmlNodeType.Element://Nodes
                        Console.Write("<" + reader.Name);

                        while (reader.MoveToNextAttribute()) //Read the attributes if exist.
                        {
                            Console.Write(" " + reader.Name + "='" + reader.Value + "'");
                        }
                        Console.WriteLine(">");
                        break;

                    case XmlNodeType.Text://Text in each node.
                        Console.WriteLine(reader.Value);
                        break;

                    case XmlNodeType.EndElement://Display the end of the element.
                        Console.Write("</" + reader.Name);
                        Console.WriteLine(">");
                        break;
                }
            }

        }
    }
}
