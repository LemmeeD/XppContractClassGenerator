﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XppContractClassGenerator
{
    public partial class AppForm : Form
    {
        public AppForm()
        {
            InitializeComponent();
        }

        private void buttonJsonReset_Click(object sender, EventArgs e)
        {
            this.textBoxJson.Text = "";
        }

        private void SetApplicationOptions(ApplicationOptions options)
        {
            this.textBoxOutput.Text = options.OutputDirectoryPath;
            this.textBoxJson.Text = options.Json;
            this.textBoxBaseClassName.Text = options.BaseClassName;
            this.checkBoxValuesPresence.Checked = options.HandleValuesPresence;
            this.checkBoxGetDates.Checked = options.HandleDates;
            this.textBoxGetDates.Text = options.DateFormat;
            this.comboBoxCollection.SelectedItem = options.CollectionDataType;
        }

        private ApplicationOptions GetDefaultApplicationOptions()
        {
            ApplicationOptions options = new ApplicationOptions();
            options.OutputDirectoryPath = this.textBoxOutput.Text;
            options.Json = this.textBoxJson.Text;
            options.BaseClassName = this.textBoxBaseClassName.Text;
            options.HandleValuesPresence = this.checkBoxValuesPresence.Checked;
            options.HandleDates = this.checkBoxGetDates.Checked;
            options.DateFormat = this.textBoxGetDates.Text;
            options.CollectionDataType = (DataType) this.comboBoxCollection.SelectedItem;
            return options;
        }

        private void buttonExecute_Click(object sender, EventArgs e)
        {
            ApplicationOptions options = this.GetDefaultApplicationOptions();
            ApplicationRunner.RunApplication(options);
        }

        private void checkBoxGetDates_CheckedChanged(object sender, EventArgs e)
        {
            bool visibility = false;
            if (this.checkBoxGetDates.Checked)
            {
                visibility = true;
                this.textBoxGetDates.Visible = visibility;
                this.labelGetDates.Visible = visibility;
            }
            else
            {
                visibility = false;
                this.textBoxGetDates.Visible = visibility;
                this.labelGetDates.Visible = visibility;
            }
        }

        private void AppForm_Load(object sender, EventArgs e)
        {
            Array enumValues = Enum.GetValues(typeof(DataType));
            for (int i=0; i<enumValues.Length; i++)
            {
                DataType cursor = (DataType)enumValues.GetValue(i);
                if (DataTypeHelper.IsCollection(cursor))
                {
                    this.comboBoxCollection.Items.Add(cursor);
                }
            }

            ApplicationOptions options = ApplicationOptions.CreateDefault();
            this.SetApplicationOptions(options);
        }
    }
}
