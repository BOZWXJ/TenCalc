
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
			this.CheckBoxSendResult = new System.Windows.Forms.CheckBox();
			this.CheckBoxSendClose = new System.Windows.Forms.CheckBox();
			this.CheckBoxSendEnter = new System.Windows.Forms.CheckBox();
			this.CheckBoxSendSeparator = new System.Windows.Forms.CheckBox();
			this.CheckBoxNumLock = new System.Windows.Forms.CheckBox();
			this.ComboBoxSkin = new System.Windows.Forms.ComboBox();
			this.ButtonOk = new System.Windows.Forms.Button();
			this.ButtonCancel = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.CheckBoxSendIMEOff = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// CheckBoxSendResult
			// 
			this.CheckBoxSendResult.AutoSize = true;
			this.CheckBoxSendResult.Location = new System.Drawing.Point(6, 22);
			this.CheckBoxSendResult.Name = "CheckBoxSendResult";
			this.CheckBoxSendResult.Size = new System.Drawing.Size(158, 19);
			this.CheckBoxSendResult.TabIndex = 0;
			this.CheckBoxSendResult.Text = "計算後の Enter で送信する";
			this.CheckBoxSendResult.UseVisualStyleBackColor = true;
			// 
			// CheckBoxSendClose
			// 
			this.CheckBoxSendClose.AutoSize = true;
			this.CheckBoxSendClose.Location = new System.Drawing.Point(26, 47);
			this.CheckBoxSendClose.Name = "CheckBoxSendClose";
			this.CheckBoxSendClose.Size = new System.Drawing.Size(101, 19);
			this.CheckBoxSendClose.TabIndex = 1;
			this.CheckBoxSendClose.Text = "送信後に閉じる";
			this.CheckBoxSendClose.UseVisualStyleBackColor = true;
			// 
			// CheckBoxSendEnter
			// 
			this.CheckBoxSendEnter.AutoSize = true;
			this.CheckBoxSendEnter.Location = new System.Drawing.Point(26, 72);
			this.CheckBoxSendEnter.Name = "CheckBoxSendEnter";
			this.CheckBoxSendEnter.Size = new System.Drawing.Size(153, 19);
			this.CheckBoxSendEnter.TabIndex = 2;
			this.CheckBoxSendEnter.Text = "送信の最後に改行をつける";
			this.CheckBoxSendEnter.UseVisualStyleBackColor = true;
			// 
			// CheckBoxSendSeparator
			// 
			this.CheckBoxSendSeparator.AutoSize = true;
			this.CheckBoxSendSeparator.Location = new System.Drawing.Point(26, 97);
			this.CheckBoxSendSeparator.Name = "CheckBoxSendSeparator";
			this.CheckBoxSendSeparator.Size = new System.Drawing.Size(156, 19);
			this.CheckBoxSendSeparator.TabIndex = 3;
			this.CheckBoxSendSeparator.Text = "送信時に桁のカンマをつける";
			this.CheckBoxSendSeparator.UseVisualStyleBackColor = true;
			// 
			// CheckBoxNumLock
			// 
			this.CheckBoxNumLock.AutoSize = true;
			this.CheckBoxNumLock.Location = new System.Drawing.Point(6, 51);
			this.CheckBoxNumLock.Name = "CheckBoxNumLock";
			this.CheckBoxNumLock.Size = new System.Drawing.Size(175, 19);
			this.CheckBoxNumLock.TabIndex = 4;
			this.CheckBoxNumLock.Text = "NumLock ON の時に表示する";
			this.CheckBoxNumLock.UseVisualStyleBackColor = true;
			// 
			// ComboBoxSkin
			// 
			this.ComboBoxSkin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ComboBoxSkin.FormattingEnabled = true;
			this.ComboBoxSkin.Location = new System.Drawing.Point(6, 22);
			this.ComboBoxSkin.Name = "ComboBoxSkin";
			this.ComboBoxSkin.Size = new System.Drawing.Size(188, 23);
			this.ComboBoxSkin.TabIndex = 5;
			// 
			// ButtonOk
			// 
			this.ButtonOk.Location = new System.Drawing.Point(12, 247);
			this.ButtonOk.Name = "ButtonOk";
			this.ButtonOk.Size = new System.Drawing.Size(75, 23);
			this.ButtonOk.TabIndex = 6;
			this.ButtonOk.Text = "OK";
			this.ButtonOk.UseVisualStyleBackColor = true;
			this.ButtonOk.Click += new System.EventHandler(this.ButtonOk_Click);
			// 
			// ButtonCancel
			// 
			this.ButtonCancel.Location = new System.Drawing.Point(137, 247);
			this.ButtonCancel.Name = "ButtonCancel";
			this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
			this.ButtonCancel.TabIndex = 7;
			this.ButtonCancel.Text = "Cancel";
			this.ButtonCancel.UseVisualStyleBackColor = true;
			this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.ComboBoxSkin);
			this.groupBox1.Controls.Add(this.CheckBoxNumLock);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(200, 76);
			this.groupBox1.TabIndex = 8;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "表示";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.CheckBoxSendIMEOff);
			this.groupBox2.Controls.Add(this.CheckBoxSendResult);
			this.groupBox2.Controls.Add(this.CheckBoxSendClose);
			this.groupBox2.Controls.Add(this.CheckBoxSendEnter);
			this.groupBox2.Controls.Add(this.CheckBoxSendSeparator);
			this.groupBox2.Location = new System.Drawing.Point(12, 94);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(200, 147);
			this.groupBox2.TabIndex = 9;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "送信";
			// 
			// CheckBoxSendIMEOff
			// 
			this.CheckBoxSendIMEOff.AutoSize = true;
			this.CheckBoxSendIMEOff.Location = new System.Drawing.Point(26, 122);
			this.CheckBoxSendIMEOff.Name = "CheckBoxSendIMEOff";
			this.CheckBoxSendIMEOff.Size = new System.Drawing.Size(161, 19);
			this.CheckBoxSendIMEOff.TabIndex = 10;
			this.CheckBoxSendIMEOff.Text = "送信時に IME を OFF にする";
			this.CheckBoxSendIMEOff.UseVisualStyleBackColor = true;
			// 
			// SettingForm
			// 
			this.AcceptButton = this.ButtonOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.ButtonCancel;
			this.ClientSize = new System.Drawing.Size(224, 282);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.ButtonCancel);
			this.Controls.Add(this.ButtonOk);
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

		private System.Windows.Forms.CheckBox CheckBoxSendResult;
		private System.Windows.Forms.CheckBox CheckBoxSendClose;
		private System.Windows.Forms.CheckBox CheckBoxSendEnter;
		private System.Windows.Forms.CheckBox CheckBoxSendSeparator;
		private System.Windows.Forms.CheckBox CheckBoxNumLock;
		private System.Windows.Forms.ComboBox ComboBoxSkin;
		private System.Windows.Forms.Button ButtonOk;
		private System.Windows.Forms.Button ButtonCancel;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.CheckBox CheckBoxSendIMEOff;
	}
}