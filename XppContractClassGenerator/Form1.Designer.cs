
using System;

namespace XppContractClassGenerator
{
    partial class AppForm
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBoxParam = new System.Windows.Forms.GroupBox();
            this.checkBoxGetColl = new System.Windows.Forms.CheckBox();
            this.labelProgLanguage = new System.Windows.Forms.Label();
            this.comboBoxProgLanguage = new System.Windows.Forms.ComboBox();
            this.textBoxBaseClassName = new System.Windows.Forms.TextBox();
            this.labelBaseClassName = new System.Windows.Forms.Label();
            this.labelGetDates = new System.Windows.Forms.Label();
            this.textBoxOutput = new System.Windows.Forms.TextBox();
            this.textBoxGetDates = new System.Windows.Forms.TextBox();
            this.labelOutput = new System.Windows.Forms.Label();
            this.checkBoxGetDates = new System.Windows.Forms.CheckBox();
            this.checkBoxValuesPresence = new System.Windows.Forms.CheckBox();
            this.groupBoxJson = new System.Windows.Forms.GroupBox();
            this.buttonJsonReset = new System.Windows.Forms.Button();
            this.labelJson = new System.Windows.Forms.Label();
            this.textBoxJson = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonExecute = new System.Windows.Forms.Button();
            this.groupBoxParam.SuspendLayout();
            this.groupBoxJson.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxParam
            // 
            this.groupBoxParam.Controls.Add(this.checkBoxGetColl);
            this.groupBoxParam.Controls.Add(this.labelProgLanguage);
            this.groupBoxParam.Controls.Add(this.comboBoxProgLanguage);
            this.groupBoxParam.Controls.Add(this.textBoxBaseClassName);
            this.groupBoxParam.Controls.Add(this.labelBaseClassName);
            this.groupBoxParam.Controls.Add(this.labelGetDates);
            this.groupBoxParam.Controls.Add(this.textBoxOutput);
            this.groupBoxParam.Controls.Add(this.textBoxGetDates);
            this.groupBoxParam.Controls.Add(this.labelOutput);
            this.groupBoxParam.Controls.Add(this.checkBoxGetDates);
            this.groupBoxParam.Controls.Add(this.checkBoxValuesPresence);
            this.groupBoxParam.Location = new System.Drawing.Point(8, 65);
            this.groupBoxParam.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxParam.Name = "groupBoxParam";
            this.groupBoxParam.Padding = new System.Windows.Forms.Padding(2);
            this.groupBoxParam.Size = new System.Drawing.Size(1001, 164);
            this.groupBoxParam.TabIndex = 0;
            this.groupBoxParam.TabStop = false;
            this.groupBoxParam.Text = "Parametri";
            // 
            // checkBoxGetColl
            // 
            this.checkBoxGetColl.AutoSize = true;
            this.checkBoxGetColl.Location = new System.Drawing.Point(7, 99);
            this.checkBoxGetColl.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxGetColl.Name = "checkBoxGetColl";
            this.checkBoxGetColl.Size = new System.Drawing.Size(324, 17);
            this.checkBoxGetColl.TabIndex = 10;
            this.checkBoxGetColl.Text = "Genera metodo per leggere un elemento i-esimo della collection";
            this.checkBoxGetColl.UseVisualStyleBackColor = true;
            // 
            // labelProgLanguage
            // 
            this.labelProgLanguage.AutoSize = true;
            this.labelProgLanguage.Location = new System.Drawing.Point(4, 57);
            this.labelProgLanguage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelProgLanguage.Name = "labelProgLanguage";
            this.labelProgLanguage.Size = new System.Drawing.Size(113, 13);
            this.labelProgLanguage.TabIndex = 9;
            this.labelProgLanguage.Text = "Output tipologia classi:";
            // 
            // comboBoxProgLanguage
            // 
            this.comboBoxProgLanguage.FormattingEnabled = true;
            this.comboBoxProgLanguage.Location = new System.Drawing.Point(125, 54);
            this.comboBoxProgLanguage.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxProgLanguage.Name = "comboBoxProgLanguage";
            this.comboBoxProgLanguage.Size = new System.Drawing.Size(187, 21);
            this.comboBoxProgLanguage.TabIndex = 8;
            // 
            // textBoxBaseClassName
            // 
            this.textBoxBaseClassName.Location = new System.Drawing.Point(125, 34);
            this.textBoxBaseClassName.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxBaseClassName.Name = "textBoxBaseClassName";
            this.textBoxBaseClassName.Size = new System.Drawing.Size(447, 20);
            this.textBoxBaseClassName.TabIndex = 7;
            // 
            // labelBaseClassName
            // 
            this.labelBaseClassName.AutoSize = true;
            this.labelBaseClassName.Location = new System.Drawing.Point(4, 36);
            this.labelBaseClassName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelBaseClassName.Name = "labelBaseClassName";
            this.labelBaseClassName.Size = new System.Drawing.Size(94, 13);
            this.labelBaseClassName.TabIndex = 6;
            this.labelBaseClassName.Text = "Nome classe base";
            // 
            // labelGetDates
            // 
            this.labelGetDates.AutoSize = true;
            this.labelGetDates.Location = new System.Drawing.Point(178, 143);
            this.labelGetDates.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelGetDates.Name = "labelGetDates";
            this.labelGetDates.Size = new System.Drawing.Size(305, 13);
            this.labelGetDates.TabIndex = 3;
            this.labelGetDates.Text = "Pattern per distinguere le stringhe che sono effettivamente date";
            this.labelGetDates.Visible = false;
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.Location = new System.Drawing.Point(125, 12);
            this.textBoxOutput.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.Size = new System.Drawing.Size(874, 20);
            this.textBoxOutput.TabIndex = 5;
            // 
            // textBoxGetDates
            // 
            this.textBoxGetDates.Location = new System.Drawing.Point(26, 140);
            this.textBoxGetDates.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxGetDates.Name = "textBoxGetDates";
            this.textBoxGetDates.Size = new System.Drawing.Size(148, 20);
            this.textBoxGetDates.TabIndex = 2;
            this.textBoxGetDates.Visible = false;
            // 
            // labelOutput
            // 
            this.labelOutput.AutoSize = true;
            this.labelOutput.Location = new System.Drawing.Point(4, 14);
            this.labelOutput.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelOutput.Name = "labelOutput";
            this.labelOutput.Size = new System.Drawing.Size(93, 13);
            this.labelOutput.TabIndex = 4;
            this.labelOutput.Text = "Percorso di output";
            // 
            // checkBoxGetDates
            // 
            this.checkBoxGetDates.AutoSize = true;
            this.checkBoxGetDates.Location = new System.Drawing.Point(7, 119);
            this.checkBoxGetDates.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxGetDates.Name = "checkBoxGetDates";
            this.checkBoxGetDates.Size = new System.Drawing.Size(277, 17);
            this.checkBoxGetDates.TabIndex = 1;
            this.checkBoxGetDates.Text = "Genera metodi per trasformare stringhe in utcdatetime";
            this.checkBoxGetDates.UseVisualStyleBackColor = true;
            this.checkBoxGetDates.CheckedChanged += new System.EventHandler(this.checkBoxGetDates_CheckedChanged);
            // 
            // checkBoxValuesPresence
            // 
            this.checkBoxValuesPresence.AutoSize = true;
            this.checkBoxValuesPresence.Location = new System.Drawing.Point(7, 81);
            this.checkBoxValuesPresence.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxValuesPresence.Name = "checkBoxValuesPresence";
            this.checkBoxValuesPresence.Size = new System.Drawing.Size(431, 17);
            this.checkBoxValuesPresence.TabIndex = 0;
            this.checkBoxValuesPresence.Text = "Genera metodi booleani per computare l\'assenza o presenza di una property nel JSO" +
    "N";
            this.checkBoxValuesPresence.UseVisualStyleBackColor = true;
            // 
            // groupBoxJson
            // 
            this.groupBoxJson.Controls.Add(this.buttonJsonReset);
            this.groupBoxJson.Controls.Add(this.labelJson);
            this.groupBoxJson.Controls.Add(this.textBoxJson);
            this.groupBoxJson.Location = new System.Drawing.Point(8, 233);
            this.groupBoxJson.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxJson.Name = "groupBoxJson";
            this.groupBoxJson.Padding = new System.Windows.Forms.Padding(2);
            this.groupBoxJson.Size = new System.Drawing.Size(1001, 382);
            this.groupBoxJson.TabIndex = 1;
            this.groupBoxJson.TabStop = false;
            this.groupBoxJson.Text = "JSON";
            // 
            // buttonJsonReset
            // 
            this.buttonJsonReset.Location = new System.Drawing.Point(899, 14);
            this.buttonJsonReset.Margin = new System.Windows.Forms.Padding(2);
            this.buttonJsonReset.Name = "buttonJsonReset";
            this.buttonJsonReset.Size = new System.Drawing.Size(99, 24);
            this.buttonJsonReset.TabIndex = 2;
            this.buttonJsonReset.Text = "Reset JSON";
            this.buttonJsonReset.UseVisualStyleBackColor = true;
            this.buttonJsonReset.Click += new System.EventHandler(this.buttonJsonReset_Click);
            // 
            // labelJson
            // 
            this.labelJson.AutoSize = true;
            this.labelJson.Location = new System.Drawing.Point(4, 20);
            this.labelJson.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelJson.Name = "labelJson";
            this.labelJson.Size = new System.Drawing.Size(106, 13);
            this.labelJson.TabIndex = 1;
            this.labelJson.Text = "JSON da computare:";
            // 
            // textBoxJson
            // 
            this.textBoxJson.Location = new System.Drawing.Point(4, 42);
            this.textBoxJson.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxJson.MaxLength = 100000;
            this.textBoxJson.Multiline = true;
            this.textBoxJson.Name = "textBoxJson";
            this.textBoxJson.Size = new System.Drawing.Size(995, 337);
            this.textBoxJson.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(499, 26);
            this.label1.TabIndex = 2;
            this.label1.Text = "Generatore di classi contract X++ partendo da un JSON. Personalizza la generazion" +
    "e coi parametri sotto.\r\nFinita l\'elaborazione verrà aperta la cartella contenent" +
    "e i file.";
            // 
            // buttonExecute
            // 
            this.buttonExecute.Location = new System.Drawing.Point(439, 619);
            this.buttonExecute.Margin = new System.Windows.Forms.Padding(2);
            this.buttonExecute.Name = "buttonExecute";
            this.buttonExecute.Size = new System.Drawing.Size(133, 29);
            this.buttonExecute.TabIndex = 6;
            this.buttonExecute.Text = "Esegui";
            this.buttonExecute.UseVisualStyleBackColor = true;
            this.buttonExecute.Click += new System.EventHandler(this.buttonExecute_Click);
            // 
            // AppForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1017, 656);
            this.Controls.Add(this.buttonExecute);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBoxJson);
            this.Controls.Add(this.groupBoxParam);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "AppForm";
            this.Text = "Generatore classi Contract X++";
            this.Load += new System.EventHandler(this.AppForm_Load);
            this.groupBoxParam.ResumeLayout(false);
            this.groupBoxParam.PerformLayout();
            this.groupBoxJson.ResumeLayout(false);
            this.groupBoxJson.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxParam;
        private System.Windows.Forms.GroupBox groupBoxJson;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxValuesPresence;
        private System.Windows.Forms.Label labelGetDates;
        private System.Windows.Forms.TextBox textBoxGetDates;
        private System.Windows.Forms.CheckBox checkBoxGetDates;
        private System.Windows.Forms.Button buttonJsonReset;
        private System.Windows.Forms.Label labelJson;
        private System.Windows.Forms.TextBox textBoxJson;
        private System.Windows.Forms.Label labelOutput;
        private System.Windows.Forms.TextBox textBoxOutput;
        private System.Windows.Forms.Button buttonExecute;
        private System.Windows.Forms.TextBox textBoxBaseClassName;
        private System.Windows.Forms.Label labelBaseClassName;
        private System.Windows.Forms.Label labelProgLanguage;
        private System.Windows.Forms.ComboBox comboBoxProgLanguage;
        private System.Windows.Forms.CheckBox checkBoxGetColl;
    }
}

