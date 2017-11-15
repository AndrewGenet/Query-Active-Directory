using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.DirectoryServices;


namespace Active_Directory_Tool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            groupBox1.Dock = DockStyle.Left;
            groupBox2.Dock = DockStyle.Fill;
            propertyCb.Items.Add("employeeid");
            propertyCb.SelectedItem = "employeeid";

            // LDAP path
            pathTxt.Text = "LDAP://DC=,DC=,DC=,DC=";
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            resultsLb.Items.Clear();
            DirectoryEntry entry = new DirectoryEntry(pathTxt.Text);
            DirectorySearcher search = new DirectorySearcher(entry);

            //search by employeeID
            search.Filter = "(" + propertyCb.SelectedItem + "=" + textBox1.Text.ToString() + ")";
            
            try
            {
                SearchResult result = search.FindOne();
                foreach(string name in result.Properties.PropertyNames)
                {
                    //add items to filter combo box
                    comboBox1.Items.Add(name.ToString());
                    //add items to the results pane
                    resultsLb.Items.Add(name + " : " + result.Properties[name.ToString()][0].ToString());
                }
            }
            catch (Exception ex)
            {
                resultsLb.Items.Add(ex.ToString());
            }
        }


        // basically the same thing with a clever way of adding only one entry to the listBox
        private void button2_Click(object sender, EventArgs e)
        {
            resultsLb.Items.Clear();
            DirectoryEntry entry = new DirectoryEntry(pathTxt.Text);
            DirectorySearcher search = new DirectorySearcher(entry);

            search.Filter = "(" + propertyCb.SelectedItem + "=" + textBox1.Text.ToString() + ")";

            try
            {
                SearchResult result = search.FindOne();
                foreach (string name in result.Properties.PropertyNames)
                {
                    if (comboBox1.SelectedItem.ToString() == name)
                    {
                        resultsLb.Items.Add(name + " : " + result.Properties[name.ToString()][0].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                resultsLb.Items.Add(ex.ToString());
            }
        }
    }
}
