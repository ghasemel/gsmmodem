
namespace GsmTest
{
    partial class TestFormGsmModemModule
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
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtbxMessage = new System.Windows.Forms.TextBox();
            this.txtbxMobile = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.rdPDU = new System.Windows.Forms.RadioButton();
            this.rdText = new System.Windows.Forms.RadioButton();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.btnPort = new System.Windows.Forms.Button();
            this.cmbPort = new System.Windows.Forms.ComboBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnSendCommand = new System.Windows.Forms.Button();
            this.txtCommand = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnConvert = new System.Windows.Forms.Button();
            this.btnForm1 = new System.Windows.Forms.Button();
            this.txtbxPrefix = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 49);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 20);
            this.label3.TabIndex = 13;
            this.label3.Text = "Port:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 184);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 20);
            this.label2.TabIndex = 11;
            this.label2.Text = "Message:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 112);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "Mobile:";
            // 
            // txtbxMessage
            // 
            this.txtbxMessage.Location = new System.Drawing.Point(138, 177);
            this.txtbxMessage.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtbxMessage.Multiline = true;
            this.txtbxMessage.Name = "txtbxMessage";
            this.txtbxMessage.Size = new System.Drawing.Size(481, 50);
            this.txtbxMessage.TabIndex = 9;
            this.txtbxMessage.Text = "test";
            // 
            // txtbxMobile
            // 
            this.txtbxMobile.Location = new System.Drawing.Point(138, 105);
            this.txtbxMobile.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtbxMobile.Name = "txtbxMobile";
            this.txtbxMobile.Size = new System.Drawing.Size(374, 26);
            this.txtbxMobile.TabIndex = 8;
            this.txtbxMobile.Text = "989113215623";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(138, 234);
            this.btnSend.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(145, 36);
            this.btnSend.TabIndex = 7;
            this.btnSend.Text = "Send SMS";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // rdPDU
            // 
            this.rdPDU.AutoSize = true;
            this.rdPDU.Checked = true;
            this.rdPDU.Location = new System.Drawing.Point(484, 234);
            this.rdPDU.Name = "rdPDU";
            this.rdPDU.Size = new System.Drawing.Size(61, 24);
            this.rdPDU.TabIndex = 14;
            this.rdPDU.TabStop = true;
            this.rdPDU.Text = "PDU";
            this.rdPDU.UseVisualStyleBackColor = true;
            // 
            // rdText
            // 
            this.rdText.AutoSize = true;
            this.rdText.Location = new System.Drawing.Point(562, 234);
            this.rdText.Name = "rdText";
            this.rdText.Size = new System.Drawing.Size(57, 24);
            this.rdText.TabIndex = 15;
            this.rdText.Text = "Text";
            this.rdText.UseVisualStyleBackColor = true;
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(138, 361);
            this.txtResult.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResult.Size = new System.Drawing.Size(500, 172);
            this.txtResult.TabIndex = 16;
            // 
            // btnPort
            // 
            this.btnPort.Location = new System.Drawing.Point(524, 37);
            this.btnPort.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.btnPort.Name = "btnPort";
            this.btnPort.Size = new System.Drawing.Size(145, 46);
            this.btnPort.TabIndex = 17;
            this.btnPort.Text = "Get Port";
            this.btnPort.UseVisualStyleBackColor = true;
            this.btnPort.Click += new System.EventHandler(this.btnPort_Click);
            // 
            // cmbPort
            // 
            this.cmbPort.FormattingEnabled = true;
            this.cmbPort.Location = new System.Drawing.Point(138, 49);
            this.cmbPort.Name = "cmbPort";
            this.cmbPort.Size = new System.Drawing.Size(375, 28);
            this.cmbPort.TabIndex = 18;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(524, 105);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(145, 27);
            this.btnConnect.TabIndex = 19;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnSendCommand
            // 
            this.btnSendCommand.Location = new System.Drawing.Point(483, 289);
            this.btnSendCommand.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.btnSendCommand.Name = "btnSendCommand";
            this.btnSendCommand.Size = new System.Drawing.Size(145, 46);
            this.btnSendCommand.TabIndex = 20;
            this.btnSendCommand.Text = "Send Command";
            this.btnSendCommand.UseVisualStyleBackColor = true;
            this.btnSendCommand.Click += new System.EventHandler(this.btnSendCommand_Click);
            // 
            // txtCommand
            // 
            this.txtCommand.Location = new System.Drawing.Point(222, 299);
            this.txtCommand.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtCommand.Name = "txtCommand";
            this.txtCommand.Size = new System.Drawing.Size(245, 26);
            this.txtCommand.TabIndex = 21;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(134, 302);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 20);
            this.label4.TabIndex = 22;
            this.label4.Text = "Command:";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(524, 138);
            this.btnDisconnect.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(145, 27);
            this.btnDisconnect.TabIndex = 23;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(8, 234);
            this.btnConvert.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(96, 46);
            this.btnConvert.TabIndex = 24;
            this.btnConvert.Text = "Convert";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // btnForm1
            // 
            this.btnForm1.Location = new System.Drawing.Point(8, 299);
            this.btnForm1.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.btnForm1.Name = "btnForm1";
            this.btnForm1.Size = new System.Drawing.Size(96, 46);
            this.btnForm1.TabIndex = 25;
            this.btnForm1.Text = "Form 1";
            this.btnForm1.UseVisualStyleBackColor = true;
            this.btnForm1.Click += new System.EventHandler(this.btnForm1_Click);
            // 
            // txtbxPrefix
            // 
            this.txtbxPrefix.Location = new System.Drawing.Point(184, 14);
            this.txtbxPrefix.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.txtbxPrefix.Name = "txtbxPrefix";
            this.txtbxPrefix.Size = new System.Drawing.Size(62, 26);
            this.txtbxPrefix.TabIndex = 26;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(70, 16);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 20);
            this.label5.TabIndex = 27;
            this.label5.Text = "Country Prefix:";
            // 
            // TestFormGsmModemModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 570);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtbxPrefix);
            this.Controls.Add(this.btnForm1);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtCommand);
            this.Controls.Add(this.btnSendCommand);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.cmbPort);
            this.Controls.Add(this.btnPort);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.rdText);
            this.Controls.Add(this.rdPDU);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtbxMessage);
            this.Controls.Add(this.txtbxMobile);
            this.Controls.Add(this.btnSend);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "TestFormGsmModemModule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtbxMessage;
        private System.Windows.Forms.TextBox txtbxMobile;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.RadioButton rdPDU;
        private System.Windows.Forms.RadioButton rdText;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Button btnPort;
        private System.Windows.Forms.ComboBox cmbPort;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnSendCommand;
        private System.Windows.Forms.TextBox txtCommand;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.Button btnForm1;
        private System.Windows.Forms.TextBox txtbxPrefix;
        private System.Windows.Forms.Label label5;
    }
}

