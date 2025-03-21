
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
            this.labelCollection = new System.Windows.Forms.Label();
            this.comboBoxCollection = new System.Windows.Forms.ComboBox();
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
            this.groupBoxParam.Controls.Add(this.labelCollection);
            this.groupBoxParam.Controls.Add(this.comboBoxCollection);
            this.groupBoxParam.Controls.Add(this.textBoxBaseClassName);
            this.groupBoxParam.Controls.Add(this.labelBaseClassName);
            this.groupBoxParam.Controls.Add(this.labelGetDates);
            this.groupBoxParam.Controls.Add(this.textBoxOutput);
            this.groupBoxParam.Controls.Add(this.textBoxGetDates);
            this.groupBoxParam.Controls.Add(this.labelOutput);
            this.groupBoxParam.Controls.Add(this.checkBoxGetDates);
            this.groupBoxParam.Controls.Add(this.checkBoxValuesPresence);
            this.groupBoxParam.Location = new System.Drawing.Point(12, 100);
            this.groupBoxParam.Name = "groupBoxParam";
            this.groupBoxParam.Size = new System.Drawing.Size(1502, 253);
            this.groupBoxParam.TabIndex = 0;
            this.groupBoxParam.TabStop = false;
            this.groupBoxParam.Text = "Parametri";
            // 
            // labelCollection
            // 
            this.labelCollection.AutoSize = true;
            this.labelCollection.Location = new System.Drawing.Point(6, 88);
            this.labelCollection.Name = "labelCollection";
            this.labelCollection.Size = new System.Drawing.Size(331, 20);
            this.labelCollection.TabIndex = 9;
            this.labelCollection.Text = "Tipo di collection per rappresentare un JArray:\r\n";
            // 
            // comboBoxCollection
            // 
            this.comboBoxCollection.FormattingEnabled = true;
            this.comboBoxCollection.Location = new System.Drawing.Point(343, 85);
            this.comboBoxCollection.Name = "comboBoxCollection";
            this.comboBoxCollection.Size = new System.Drawing.Size(278, 28);
            this.comboBoxCollection.TabIndex = 8;
            // 
            // textBoxBaseClassName
            // 
            this.textBoxBaseClassName.Location = new System.Drawing.Point(187, 53);
            this.textBoxBaseClassName.Name = "textBoxBaseClassName";
            this.textBoxBaseClassName.Size = new System.Drawing.Size(668, 26);
            this.textBoxBaseClassName.TabIndex = 7;
            // 
            // labelBaseClassName
            // 
            this.labelBaseClassName.AutoSize = true;
            this.labelBaseClassName.Location = new System.Drawing.Point(6, 56);
            this.labelBaseClassName.Name = "labelBaseClassName";
            this.labelBaseClassName.Size = new System.Drawing.Size(139, 20);
            this.labelBaseClassName.TabIndex = 6;
            this.labelBaseClassName.Text = "Nome classe base";
            // 
            // labelGetDates
            // 
            this.labelGetDates.AutoSize = true;
            this.labelGetDates.Location = new System.Drawing.Point(265, 187);
            this.labelGetDates.Name = "labelGetDates";
            this.labelGetDates.Size = new System.Drawing.Size(458, 20);
            this.labelGetDates.TabIndex = 3;
            this.labelGetDates.Text = "Pattern per distinguere le stringhe che sono effettivamente date";
            this.labelGetDates.Visible = false;
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.Location = new System.Drawing.Point(187, 19);
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.Size = new System.Drawing.Size(1309, 26);
            this.textBoxOutput.TabIndex = 5;
            // 
            // textBoxGetDates
            // 
            this.textBoxGetDates.Location = new System.Drawing.Point(39, 184);
            this.textBoxGetDates.Name = "textBoxGetDates";
            this.textBoxGetDates.Size = new System.Drawing.Size(220, 26);
            this.textBoxGetDates.TabIndex = 2;
            this.textBoxGetDates.Visible = false;
            // 
            // labelOutput
            // 
            this.labelOutput.AutoSize = true;
            this.labelOutput.Location = new System.Drawing.Point(6, 22);
            this.labelOutput.Name = "labelOutput";
            this.labelOutput.Size = new System.Drawing.Size(138, 20);
            this.labelOutput.TabIndex = 4;
            this.labelOutput.Text = "Percorso di output";
            // 
            // checkBoxGetDates
            // 
            this.checkBoxGetDates.AutoSize = true;
            this.checkBoxGetDates.Location = new System.Drawing.Point(10, 154);
            this.checkBoxGetDates.Name = "checkBoxGetDates";
            this.checkBoxGetDates.Size = new System.Drawing.Size(419, 24);
            this.checkBoxGetDates.TabIndex = 1;
            this.checkBoxGetDates.Text = "Genera metodi per trasformare stringhe in utcdatetime";
            this.checkBoxGetDates.UseVisualStyleBackColor = true;
            this.checkBoxGetDates.CheckedChanged += new System.EventHandler(this.checkBoxGetDates_CheckedChanged);
            // 
            // checkBoxValuesPresence
            // 
            this.checkBoxValuesPresence.AutoSize = true;
            this.checkBoxValuesPresence.Location = new System.Drawing.Point(10, 124);
            this.checkBoxValuesPresence.Name = "checkBoxValuesPresence";
            this.checkBoxValuesPresence.Size = new System.Drawing.Size(645, 24);
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
            this.groupBoxJson.Location = new System.Drawing.Point(12, 359);
            this.groupBoxJson.Name = "groupBoxJson";
            this.groupBoxJson.Size = new System.Drawing.Size(1502, 588);
            this.groupBoxJson.TabIndex = 1;
            this.groupBoxJson.TabStop = false;
            this.groupBoxJson.Text = "JSON";
            // 
            // buttonJsonReset
            // 
            this.buttonJsonReset.Location = new System.Drawing.Point(1348, 22);
            this.buttonJsonReset.Name = "buttonJsonReset";
            this.buttonJsonReset.Size = new System.Drawing.Size(148, 37);
            this.buttonJsonReset.TabIndex = 2;
            this.buttonJsonReset.Text = "Reset JSON";
            this.buttonJsonReset.UseVisualStyleBackColor = true;
            this.buttonJsonReset.Click += new System.EventHandler(this.buttonJsonReset_Click);
            // 
            // labelJson
            // 
            this.labelJson.AutoSize = true;
            this.labelJson.Location = new System.Drawing.Point(6, 30);
            this.labelJson.Name = "labelJson";
            this.labelJson.Size = new System.Drawing.Size(157, 20);
            this.labelJson.TabIndex = 1;
            this.labelJson.Text = "JSON da computare:";
            // 
            // textBoxJson
            // 
            this.textBoxJson.Location = new System.Drawing.Point(6, 65);
            this.textBoxJson.Multiline = true;
            this.textBoxJson.Name = "textBoxJson";
            this.textBoxJson.Size = new System.Drawing.Size(1490, 516);
            this.textBoxJson.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(748, 40);
            this.label1.TabIndex = 2;
            this.label1.Text = "Generatore di classi contract X++ partendo da un JSON. Personalizza la generazion" +
    "e coi parametri sotto.\r\nFinita l\'elaborazione verrà aperta la cartella contenent" +
    "e i file.";
            // 
            // buttonExecute
            // 
            this.buttonExecute.Location = new System.Drawing.Point(658, 953);
            this.buttonExecute.Name = "buttonExecute";
            this.buttonExecute.Size = new System.Drawing.Size(200, 45);
            this.buttonExecute.TabIndex = 6;
            this.buttonExecute.Text = "Esegui";
            this.buttonExecute.UseVisualStyleBackColor = true;
            this.buttonExecute.Click += new System.EventHandler(this.buttonExecute_Click);
            // 
            // AppForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1526, 1010);
            this.Controls.Add(this.buttonExecute);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBoxJson);
            this.Controls.Add(this.groupBoxParam);
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
        private System.Windows.Forms.Label labelCollection;
        private System.Windows.Forms.ComboBox comboBoxCollection;
    }
}

