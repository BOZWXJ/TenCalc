
namespace TenCalc
{
	partial class SettingForm
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
			if (disposing && (components != null)) {
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
			this.checkBoxSendResult = new System.Windows.Forms.CheckBox();
			this.checkBoxSendClose = new System.Windows.Forms.CheckBox();
			this.checkBoxSendEnter = new System.Windows.Forms.CheckBox();
			this.checkBoxSendSeparator = new System.Windows.Forms.CheckBox();
			this.checkBoxNumLock = new System.Windows.Forms.CheckBox();
			this.comboBoxSkin = new System.Windows.Forms.ComboBox();
			this.buttonOk = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.checkBoxSendIMEOff = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// checkBoxSendResult
			// 
			this.checkBoxSendResult.AutoSize = true;
			this.checkBoxSendResult.Location = new System.Drawing.Point(6, 22);
			this.checkBoxSendResult.Name = "checkBoxSendResult";
			this.checkBoxSendResult.Size = new System.Drawing.Size(158, 19);
			this.checkBoxSendResult.TabIndex = 0;
			this.checkBoxSendResult.Text = "計算後の Enter で送信する";
			this.checkBoxSendResult.UseVisualStyleBackColor = true;
			// 
			// checkBoxSendClose
			// 
			this.checkBoxSendClose.AutoSize = true;
			this.checkBoxSendClose.Location = new System.Drawing.Point(26, 47);
			this.checkBoxSendClose.Name = "checkBoxSendClose";
			this.checkBoxSendClose.Size = new System.Drawing.Size(101, 19);
			this.checkBoxSendClose.TabIndex = 1;
			this.checkBoxSendClose.Text = "送信後に閉じる";
			this.checkBoxSendClose.UseVisualStyleBackColor = true;
			// 
			// checkBoxSendEnter
			// 
			this.checkBoxSendEnter.AutoSize = true;
			this.checkBoxSendEnter.Location = new System.Drawing.Point(26, 72);
			this.checkBoxSendEnter.Name = "checkBoxSendEnter";
			this.checkBoxSendEnter.Size = new System.Drawing.Size(153, 19);
			this.checkBoxSendEnter.TabIndex = 2;
			this.checkBoxSendEnter.Text = "送信の最後に改行をつける";
			this.checkBoxSendEnter.UseVisualStyleBackColor = true;
			// 
			// checkBoxSendSeparator
			// 
			this.checkBoxSendSeparator.AutoSize = true;
			this.checkBoxSendSeparator.Location = new System.Drawing.Point(26, 97);
			this.checkBoxSendSeparator.Name = "checkBoxSendSeparator";
			this.checkBoxSendSeparator.Size = new System.Drawing.Size(156, 19);
			this.checkBoxSendSeparator.TabIndex = 3;
			this.checkBoxSendSeparator.Text = "送信時に桁のカンマをつける";
			this.checkBoxSendSeparator.UseVisualStyleBackColor = true;
			// 
			// checkBoxNumLock
			// 
			this.checkBoxNumLock.AutoSize = true;
			this.checkBoxNumLock.Location = new System.Drawing.Point(6, 51);
			this.checkBoxNumLock.Name = "checkBoxNumLock";
			this.checkBoxNumLock.Size = new System.Drawing.Size(175, 19);
			this.checkBoxNumLock.TabIndex = 4;
			this.checkBoxNumLock.Text = "NumLock ON の時に表示する";
			this.checkBoxNumLock.UseVisualStyleBackColor = true;
			// 
			// comboBoxSkin
			// 
			this.comboBoxSkin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxSkin.FormattingEnabled = true;
			this.comboBoxSkin.Location = new System.Drawing.Point(6, 22);
			this.comboBoxSkin.Name = "comboBoxSkin";
			this.comboBoxSkin.Size = new System.Drawing.Size(188, 23);
			this.comboBoxSkin.TabIndex = 5;
			// 
			// buttonOk
			// 
			this.buttonOk.Location = new System.Drawing.Point(12, 247);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Size = new System.Drawing.Size(75, 23);
			this.buttonOk.TabIndex = 6;
			this.buttonOk.Text = "OK";
			this.buttonOk.UseVisualStyleBackColor = true;
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Location = new System.Drawing.Point(137, 247);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 7;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.comboBoxSkin);
			this.groupBox1.Controls.Add(this.checkBoxNumLock);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(200, 76);
			this.groupBox1.TabIndex = 8;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "表示";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.checkBoxSendIMEOff);
			this.groupBox2.Controls.Add(this.checkBoxSendResult);
			this.groupBox2.Controls.Add(this.checkBoxSendClose);
			this.groupBox2.Controls.Add(this.checkBoxSendEnter);
			this.groupBox2.Controls.Add(this.checkBoxSendSeparator);
			this.groupBox2.Location = new System.Drawing.Point(12, 94);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(200, 147);
			this.groupBox2.TabIndex = 9;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "送信";
			// 
			// checkBoxSendIMEOff
			// 
			this.checkBoxSendIMEOff.AutoSize = true;
			this.checkBoxSendIMEOff.Location = new System.Drawing.Point(26, 122);
			this.checkBoxSendIMEOff.Name = "checkBoxSendIMEOff";
			this.checkBoxSendIMEOff.Size = new System.Drawing.Size(152, 19);
			this.checkBoxSendIMEOff.TabIndex = 10;
			this.checkBoxSendIMEOff.Text = "送信時に IME を OFF する";
			this.checkBoxSendIMEOff.UseVisualStyleBackColor = true;
			// 
			// SettingForm
			// 
			this.AcceptButton = this.buttonOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(224, 282);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOk);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SettingForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "設定";
			this.TopMost = true;
			this.Shown += new System.EventHandler(this.SettingForm_Shown);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.CheckBox checkBoxSendResult;
		private System.Windows.Forms.CheckBox checkBoxSendClose;
		private System.Windows.Forms.CheckBox checkBoxSendEnter;
		private System.Windows.Forms.CheckBox checkBoxSendSeparator;
		private System.Windows.Forms.CheckBox checkBoxNumLock;
		private System.Windows.Forms.ComboBox comboBoxSkin;
		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.CheckBox checkBoxSendIMEOff;
	}
}