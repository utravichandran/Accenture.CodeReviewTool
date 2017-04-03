using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Accenture.UIPAth.CodeReviewTool.Infrastructure;

namespace Accenture.UIPAth.CodeReviewTool
{
    public partial class AccentureUIPathCodeReviewForm : Form
    {
        public AccentureUIPathCodeReviewForm()
        {
           
            InitializeComponent();

            this.dtgCodeReview.AutoResizeColumns();
            this.dtgCodeReview.ClipboardCopyMode =
                DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
        }
        private void folderBrowserDialog2_HelpRequest(object sender, EventArgs e)
        {

        }



        private void lbl_Click(object sender, EventArgs e)
        {

        }
        private void btnCloseApplication_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure To Close ?", "Exit", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                System.Windows.Forms.Application.Exit();
            }


        }
        private void btnBrowseFile_Click_1(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtFilePath.Visible = true;
                this.txtFilePath.Text = openFileDialog1.FileName;
            }


        }
        private void AccentureUIPathCodeReviewForm_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
           
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string path = txtFilePath.Text;
            string filename = path;
            //ReadXML xml = new ReadXML();
            //xml.ReadXMLData(filename);
            int nestedLevelAllowed = 2;
            int currentDepth = 0;

            XmlTextReader reader = new XmlTextReader(filename);
            reader.Read();
            // Escape parsing document before Sequence Element.
            reader.ReadToFollowing("Sequence");
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
                                dtgCodeReview.Rows.Add(attributeValue, "ARGUMENT", "HIGH");
                            }
                            if (elementName == "Variable" && attributeName == "Name" && !String.IsNullOrEmpty(attributeValue))
                            {
                                //check if attribute value is all in UPPER case
                                if (attributeValue == attributeValue.ToUpperInvariant())
                                {
                                    dtgCodeReview.Rows.Add(attributeValue, "Attribute value is ALL in UPPER case", "HIGH");

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
                                        dtgCodeReview.Rows.Add(attributeValue, "Naming Convention Not Followed", "HIGH");
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
                                dtgCodeReview.Rows.Add(elementName, "Nested If not allowed", "HIGH");

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
            Console.Read();
        }
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

       
        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
