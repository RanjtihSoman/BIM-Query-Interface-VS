using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;

namespace BIM_Query_Interface
{
    public partial class BIM_Query_Interface : Form
    {
        private string filename;
        private TextBox TextBox_Filename;
        private TextBox textBox_Query;
        private TextBox textBox_output;
        private Button button_BrowseClick;
        private Button button_OpenFile;
        private Button button_SubmitQuery;
        private Label label_Output;
        private Label label_EnterQuery;
        private Label label_filename;
        private XBIM_IFC_Parser  model;
        
        
        //read the line into a list and then enrich the list containing IFCRELASSIGNS
        private void Open_Click(object sender, EventArgs e)
        {
            filename = TextBox_Filename.Text;
        Boolean flag = true;
            if (filename != null)
            {
                model = new XBIM_IFC_Parser(filename);

            }
            else
            {
                messageboxopen("You did not enter a file name. ", "No file Name Specified");

            }
            messageboxopen("end of file", "process complete");
        }

        

        private void button_SubmitQuery_Click(object sender, EventArgs e)
        {
            if (textBox_Query.Text != null)
            {
                String QueryString = textBox_Query.Text;
                string[] tempstring = QueryString.Split('/');
                DateTime selecteddate = new DateTime(Int32.Parse(tempstring[2]), Int32.Parse(tempstring[1]), Int32.Parse(tempstring[0]));
                string outputstring = null;
                string currentoutputstring = textBox_output.Text;
                IEnumerable<IIfcTask> Tasks = model.model.Instances.OfType<IIfcTask>();
                IEnumerable<IIfcTask> SelectedTasks = Tasks.Gettasksondate(selecteddate);


            }
            else { messageboxopen("Enter query", "No query found"); }
        }


        public BIM_Query_Interface()
        {
            InitializeComponent();
        }


        //save the filename to the string

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog_IFC = new OpenFileDialog();

            openFileDialog_IFC.InitialDirectory = @"F:\Code repo\BIM Query Interface";
            openFileDialog_IFC.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog_IFC.FilterIndex = 2;
            openFileDialog_IFC.RestoreDirectory = true;

            if (openFileDialog_IFC.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog_IFC.FileName;
                TextBox_Filename.Text = filename;
                TextBox_Filename.Update();

            }
        }

        private void messageboxopen(string message, string caption)
        {
            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
            DialogResult result;

            // Displays the MessageBox.

            result = MessageBox.Show(this, message, caption, buttons,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
                MessageBoxOptions.RightAlign);

        }
        private void InitializeComponent()
        {
            this.TextBox_Filename = new System.Windows.Forms.TextBox();
            this.textBox_Query = new System.Windows.Forms.TextBox();
            this.textBox_output = new System.Windows.Forms.TextBox();
            this.button_BrowseClick = new System.Windows.Forms.Button();
            this.button_OpenFile = new System.Windows.Forms.Button();
            this.button_SubmitQuery = new System.Windows.Forms.Button();
            this.label_Output = new System.Windows.Forms.Label();
            this.label_EnterQuery = new System.Windows.Forms.Label();
            this.label_filename = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TextBox_Filename
            // 
            this.TextBox_Filename.Location = new System.Drawing.Point(12, 38);
            this.TextBox_Filename.Name = "TextBox_Filename";
            this.TextBox_Filename.Size = new System.Drawing.Size(526, 20);
            this.TextBox_Filename.TabIndex = 1;
            this.TextBox_Filename.Text = "F:\\Code repo\\BIM Query Interface\\IFC Schependomlaan incl planningsdata.ifc";
            // 
            // textBox_Query
            // 
            this.textBox_Query.Location = new System.Drawing.Point(12, 75);
            this.textBox_Query.Name = "textBox_Query";
            this.textBox_Query.Size = new System.Drawing.Size(592, 20);
            this.textBox_Query.TabIndex = 2;
            // 
            // textBox_output
            // 
            this.textBox_output.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBox_output.Location = new System.Drawing.Point(0, 135);
            this.textBox_output.Multiline = true;
            this.textBox_output.Name = "textBox_output";
            this.textBox_output.Size = new System.Drawing.Size(755, 126);
            this.textBox_output.TabIndex = 3;
            // 
            // button_BrowseClick
            // 
            this.button_BrowseClick.Location = new System.Drawing.Point(544, 35);
            this.button_BrowseClick.Name = "button_BrowseClick";
            this.button_BrowseClick.Size = new System.Drawing.Size(102, 23);
            this.button_BrowseClick.TabIndex = 4;
            this.button_BrowseClick.Text = "Browse";
            this.button_BrowseClick.UseVisualStyleBackColor = true;
            this.button_BrowseClick.Click += new System.EventHandler(this.button1_Click);
            // 
            // button_OpenFile
            // 
            this.button_OpenFile.Location = new System.Drawing.Point(652, 35);
            this.button_OpenFile.Name = "button_OpenFile";
            this.button_OpenFile.Size = new System.Drawing.Size(75, 23);
            this.button_OpenFile.TabIndex = 5;
            this.button_OpenFile.Text = "Open File";
            this.button_OpenFile.UseVisualStyleBackColor = true;
            this.button_OpenFile.Click += new System.EventHandler(this.Open_Click);
            // 
            // button_SubmitQuery
            // 
            this.button_SubmitQuery.Location = new System.Drawing.Point(633, 75);
            this.button_SubmitQuery.Name = "button_SubmitQuery";
            this.button_SubmitQuery.Size = new System.Drawing.Size(94, 23);
            this.button_SubmitQuery.TabIndex = 6;
            this.button_SubmitQuery.Text = "Submit Query";
            this.button_SubmitQuery.UseVisualStyleBackColor = true;
            this.button_SubmitQuery.Click += new System.EventHandler(this.button_SubmitQuery_Click);
            // 
            // label_Output
            // 
            this.label_Output.AutoSize = true;
            this.label_Output.Location = new System.Drawing.Point(12, 119);
            this.label_Output.Name = "label_Output";
            this.label_Output.Size = new System.Drawing.Size(39, 13);
            this.label_Output.TabIndex = 7;
            this.label_Output.Text = "Output";
            // 
            // label_EnterQuery
            // 
            this.label_EnterQuery.AutoSize = true;
            this.label_EnterQuery.Location = new System.Drawing.Point(12, 61);
            this.label_EnterQuery.Name = "label_EnterQuery";
            this.label_EnterQuery.Size = new System.Drawing.Size(63, 13);
            this.label_EnterQuery.TabIndex = 8;
            this.label_EnterQuery.Text = "Enter Query";
            // 
            // label_filename
            // 
            this.label_filename.AutoSize = true;
            this.label_filename.Location = new System.Drawing.Point(12, 22);
            this.label_filename.Name = "label_filename";
            this.label_filename.Size = new System.Drawing.Size(151, 13);
            this.label_filename.TabIndex = 9;
            this.label_filename.Text = "Enter Filename or click browse";
            // 
            // BIM_Query_Interface
            // 
            this.ClientSize = new System.Drawing.Size(755, 261);
            this.Controls.Add(this.label_filename);
            this.Controls.Add(this.label_EnterQuery);
            this.Controls.Add(this.label_Output);
            this.Controls.Add(this.button_SubmitQuery);
            this.Controls.Add(this.button_OpenFile);
            this.Controls.Add(this.button_BrowseClick);
            this.Controls.Add(this.textBox_output);
            this.Controls.Add(this.textBox_Query);
            this.Controls.Add(this.TextBox_Filename);
            this.Name = "BIM_Query_Interface";
            this.Text = "BIM Query Interface";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }

}

