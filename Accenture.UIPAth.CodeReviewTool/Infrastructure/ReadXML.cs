using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;


namespace Accenture.UIPAth.CodeReviewTool.Infrastructure
{
    public  class ReadXML
    {
        public void ReadDataFromXML(DataGridView dg,string filename)
        {
            int nestedLevelAllowed = 2;
            int currentDepth = 0;

            XmlTextReader reader = new XmlTextReader(filename);
            reader.Read();
            // Escape parsing document before Sequence Element.
            // reader.ReadToFollowing("Sequence");

            while (reader.Read())
            {
                // Check for type of attribute of current node

                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // The node is an element.
                        string elementName = reader.Name;
                        //Console.WriteLine("Tag Name: " + reader.Name);

                        // Read the attributes.
                        while (reader.MoveToNextAttribute())
                        {
                            string attributeName = reader.Name;
                            string attributeValue = reader.Value;

                            //Console.WriteLine(" Attribute Name:" + attributeName);
                            //Console.WriteLine(" Attribute Value'" + attributeValue + "'");

                            // Start: Check for Casing here
                            if (elementName == "OutArgument")
                            {
                                dg.Rows.Add(attributeValue, "ARGUMENT", "HIGH");
                                dg.Refresh();
                            }
                            if (elementName == "Variable" && attributeName == "Name" && !String.IsNullOrEmpty(attributeValue))
                            {
                                //check if attribute value is all in UPPER case
                                if (attributeValue == attributeValue.ToUpperInvariant())
                                {
                                    dg.Rows.Add(attributeValue, "Attribute value is ALL in UPPER case", "HIGH");
                                    dg.Refresh();
                                }
                                else
                                {
                                    Boolean typecamel = false;
                                    Boolean typepascal = false;
                                    string camel = attributeValue.ToPascalCase();
                                    string pascal = attributeValue.ToCamelCase();

                                    if (attributeValue == camel)
                                    {
                                        typecamel = true;

                                    }

                                    if (attributeValue == pascal)
                                    {
                                        typepascal = true;
                                    }
                                    if (typecamel == true || typepascal == true)
                                    {
                                        dg.Rows.Add(attributeValue, "Naming Convention Not Followed", "HIGH");
                                        dg.Refresh();

                                    }
                                }


                            }
                            // End: Check for casing
                        }

                        // Start: Check for Nested Ifs
                        if (elementName == "If")
                        {
                            currentDepth++;

                            if (currentDepth > nestedLevelAllowed)
                            {

                                dg.Rows.Add(elementName, "Nested If not allowed", "HIGH");
                                dg.Refresh();
                            }
                        }

                        // End: Check for Nested Ifs

                        break;
                    //case XmlNodeType.Text: //Display the text in each element.
                    //    Console.WriteLine(reader.Value);
                    //    break;
                    case XmlNodeType.EndElement: //Display the end of the element.
                        string endElementName = reader.Name;

                        //Console.Write("</" + reader.Name);
                        //Console.WriteLine(">");
                        if (endElementName == "If")
                        {
                            if (currentDepth > 0)
                            {
                                currentDepth--;
                            }
                        }
                        break;
                }
            }
        }
    }
}
