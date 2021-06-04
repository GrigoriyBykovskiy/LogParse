using System.Windows.Forms;

namespace LogParse
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.richTextBoxParseResult = new System.Windows.Forms.RichTextBox();
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.labelSignatureName = new System.Windows.Forms.Label();
            this.comboBoxSignature = new System.Windows.Forms.ComboBox();
            this.buttonGetLog = new System.Windows.Forms.Button();
            this.buttonHighlightSignature = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBoxParseResult
            // 
            this.richTextBoxParseResult.Location = new System.Drawing.Point(12, 224);
            this.richTextBoxParseResult.Name = "richTextBoxParseResult";
            this.richTextBoxParseResult.Size = new System.Drawing.Size(758, 150);
            this.richTextBoxParseResult.TabIndex = 0;
            this.richTextBoxParseResult.Text = "";
            // 
            // richTextBoxLog
            // 
            this.richTextBoxLog.Location = new System.Drawing.Point(12, 391);
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.Size = new System.Drawing.Size(758, 150);
            this.richTextBoxLog.TabIndex = 1;
            this.richTextBoxLog.Text = "";
            // 
            // labelSignatureName
            // 
            this.labelSignatureName.Location = new System.Drawing.Point(12, 9);
            this.labelSignatureName.Name = "labelSignatureName";
            this.labelSignatureName.Size = new System.Drawing.Size(100, 30);
            this.labelSignatureName.TabIndex = 2;
            this.labelSignatureName.Text = "Сигнатура:";
            // 
            // comboBoxSignature
            // 
            this.comboBoxSignature.FormattingEnabled = true;
            this.comboBoxSignature.Location = new System.Drawing.Point(12, 42);
            this.comboBoxSignature.Name = "comboBoxSignature";
            this.comboBoxSignature.Size = new System.Drawing.Size(250, 24);
            this.comboBoxSignature.TabIndex = 3;
            this.comboBoxSignature.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBoxSignature_KeyDown);
            // 
            // buttonGetLog
            // 
            this.buttonGetLog.Location = new System.Drawing.Point(12, 173);
            this.buttonGetLog.Name = "buttonGetLog";
            this.buttonGetLog.Size = new System.Drawing.Size(120, 45);
            this.buttonGetLog.TabIndex = 4;
            this.buttonGetLog.Text = "открыть лог-журнал";
            this.buttonGetLog.UseVisualStyleBackColor = true;
            this.buttonGetLog.Click += new System.EventHandler(this.buttonGetLog_Click);
            // 
            // buttonHighlightSignature
            // 
            this.buttonHighlightSignature.Location = new System.Drawing.Point(142, 173);
            this.buttonHighlightSignature.Name = "buttonHighlightSignature";
            this.buttonHighlightSignature.Size = new System.Drawing.Size(120, 45);
            this.buttonHighlightSignature.TabIndex = 5;
            this.buttonHighlightSignature.Text = "выделить сигнатуру";
            this.buttonHighlightSignature.UseVisualStyleBackColor = true;
            this.buttonHighlightSignature.Click += new System.EventHandler(this.buttonHighlightSignature_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 565);
            this.Controls.Add(this.buttonHighlightSignature);
            this.Controls.Add(this.buttonGetLog);
            this.Controls.Add(this.comboBoxSignature);
            this.Controls.Add(this.labelSignatureName);
            this.Controls.Add(this.richTextBoxLog);
            this.Controls.Add(this.richTextBoxParseResult);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximumSize = new System.Drawing.Size(800, 600);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LogParse";
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.ComboBox comboBoxSignature;
        private System.Windows.Forms.Button buttonGetLog;
        private System.Windows.Forms.Button buttonHighlightSignature;

        private System.Windows.Forms.Label labelSignatureName;
        private System.Windows.Forms.RichTextBox richTextBoxLog;
        private System.Windows.Forms.RichTextBox richTextBoxParseResult;

        #endregion
    }
}