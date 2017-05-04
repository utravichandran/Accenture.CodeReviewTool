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
    public class ReadXML
    {
        public void ReadDataFromXML(DataGridView dg, string filename)
        {
            
            int nestedLevelAllowed = 4;
            int currentDepth = 0;
            int flowLevelAllowed = 4;
            int currentFlow = 0;
            string ElementName = string.Empty;
            string KeyName = string.Empty;
            string Value = String.Empty;
            XmlTextReader reader = new XmlTextReader(filename);
            
            reader.Read();
            // Escape parsing document before Sequence Element.

            
            while (reader.Read())
            {
                               
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // The node is an element.
                        string elementName = reader.Name;
                        //Console.WriteLine("Tag Name: " + reader.Name);

                        if (!string.IsNullOrEmpty(ElementName) && string.IsNullOrEmpty(Value))
                        {
                            dg.Rows.Add(KeyName , StringConstant.Defaultvalue, StringConstant.High);
                            dg.Refresh();
                        }

                        if (elementName == StringConstant.Inargument)
                        {
                            ElementName = elementName;
                            KeyName = string.Empty;
                            Value = string.Empty;
                        }


                        // Read the attributes.
                        while (reader.MoveToNextAttribute())
                        {
                            string attributeName = reader.Name;
                            string attributeValue = reader.Value;
                            //Console.WriteLine("Tag Name: " + reader.Name);
                            if (elementName == StringConstant.Inargument && attributeName == StringConstant.Xkey)
                            {
                                KeyName = attributeValue;
                            }
                            //Display name standard case
                            if (attributeName == StringConstant.Displayname)
                            {
                                bool standard = attributeValue.ToStandardCase();

                                if (standard == false)
                                {

                                    dg.Rows.Add(attributeValue, StringConstant.Namingnotstandard, StringConstant.High);

                                    dg.Refresh();
                                }
                            }
                           


                            // Start: Check for Casing here
                            if (elementName == StringConstant.Property && attributeName == StringConstant.Name)
                            {


                                //  Boolean typecamel = false;
                                Boolean typepascal = false;

                                string pascal = attributeValue.ToPascalCase();



                                if (attributeValue != pascal)
                                {
                                    typepascal = true;
                                    dg.Rows.Add(attributeValue, StringConstant.Namingargument, StringConstant.High);
                                    dg.Refresh();
                                }



                            }


                            if (elementName == StringConstant.Variable && attributeName == StringConstant.Name && !String.IsNullOrEmpty(attributeValue))
                            {
                                //check if attribute value is all in UPPER case
                                if (attributeValue == attributeValue.ToUpperInvariant())
                                {
                                    dg.Rows.Add(attributeValue, StringConstant.Uppercase, StringConstant.High);
                                    dg.Refresh();
                                }
                                else
                                {
                                    Boolean typecamel = false;

                                    string camel = attributeValue.ToCamelCase();


                                    if (attributeValue == camel)
                                    {
                                        typecamel = true;
                                        dg.Rows.Add(attributeValue, StringConstant.Namingvariable, StringConstant.High);
                                        dg.Refresh();

                                    }



                                }


                            }
                            // End: Check for casing
                        }

                        // Start: Check for Nested Ifs
                        if (elementName == StringConstant.If)
                        {
                            currentDepth++;

                            if (currentDepth > nestedLevelAllowed)
                            {

                                dg.Rows.Add(elementName, StringConstant.Ifrule, StringConstant.High);
                                dg.Refresh();
                            }
                        }

                        // End: Check for Nested Ifs
                        //Check for Flowstep
                        if (elementName == StringConstant.Flowstep)
                        {
                            currentFlow++;

                            if (currentFlow > flowLevelAllowed)
                            {

                                dg.Rows.Add(elementName, StringConstant.Ifrule, StringConstant.High);
                                dg.Refresh();
                            }
                        }

                        break;

                    case XmlNodeType.Text:
                        if (!string.IsNullOrEmpty(ElementName))
                        {
                            Value = reader.Value;
                        }

                        break;

                    case XmlNodeType.EndElement: //Display the end of the element.
                        string endElementName = reader.Name;

                        if (endElementName == StringConstant.Flowstep)
                        {
                            if (currentFlow > 0)
                            {
                                currentFlow--;
                            }
                        }

                        if (endElementName == StringConstant.If)
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


