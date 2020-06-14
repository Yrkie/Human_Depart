namespace Human_Depart
{
    partial class LoginForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Login = new Bunifu.Framework.UI.BunifuFlatButton();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Password = new WindowsFormsControlLibrary1.BunifuCustomTextbox();
            this.Log = new WindowsFormsControlLibrary1.BunifuCustomTextbox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.Location = new System.Drawing.Point(22, 231);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(284, 1);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel2.Location = new System.Drawing.Point(22, 269);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(284, 1);
            this.panel2.TabIndex = 2;
            // 
            // Login
            // 
            this.Login.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.Login.BackColor = System.Drawing.SystemColors.HotTrack;
            this.Login.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Login.BorderRadius = 0;
            this.Login.ButtonText = "Вхід";
            this.Login.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Login.DisabledColor = System.Drawing.Color.Gray;
            this.Login.Iconcolor = System.Drawing.Color.Transparent;
            this.Login.Iconimage = null;
            this.Login.Iconimage_right = null;
            this.Login.Iconimage_right_Selected = null;
            this.Login.Iconimage_Selected = null;
            this.Login.IconMarginLeft = 0;
            this.Login.IconMarginRight = 0;
            this.Login.IconRightVisible = true;
            this.Login.IconRightZoom = 0D;
            this.Login.IconVisible = true;
            this.Login.IconZoom = 90D;
            this.Login.IsTab = false;
            this.Login.Location = new System.Drawing.Point(93, 281);
            this.Login.Name = "Login";
            this.Login.Normalcolor = System.Drawing.SystemColors.HotTrack;
            this.Login.OnHovercolor = System.Drawing.SystemColors.HotTrack;
            this.Login.OnHoverTextColor = System.Drawing.Color.White;
            this.Login.selected = false;
            this.Login.Size = new System.Drawing.Size(141, 48);
            this.Login.TabIndex = 3;
            this.Login.Text = "Вхід";
            this.Login.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Login.Textcolor = System.Drawing.Color.White;
            this.Login.TextFont = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Login.Click += new System.EventHandler(this.Login_Click);
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(317, -1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(18, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Х";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Human_Depart.Properties.Resources.technology__3_;
            this.pictureBox3.Location = new System.Drawing.Point(22, 238);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(33, 32);
            this.pictureBox3.TabIndex = 6;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Human_Depart.Properties.Resources.avatar__1_;
            this.pictureBox2.Location = new System.Drawing.Point(22, 200);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(33, 32);
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Human_Depart.Properties.Resources.destinations__1_;
            this.pictureBox1.Location = new System.Drawing.Point(93, 35);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(141, 136);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Password
            // 
            this.Password.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Password.BorderColor = System.Drawing.Color.SeaGreen;
            this.Password.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Password.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Password.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Password.Location = new System.Drawing.Point(61, 244);
            this.Password.Name = "Password";
            this.Password.PasswordChar = '*';
            this.Password.Size = new System.Drawing.Size(189, 26);
            this.Password.TabIndex = 8;
            this.Password.Text = "Пароль";
            this.Password.Click += new System.EventHandler(this.Password_Click);
            // 
            // Log
            // 
            this.Log.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Log.BorderColor = System.Drawing.Color.SeaGreen;
            this.Log.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Log.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Log.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Log.Location = new System.Drawing.Point(61, 200);
            this.Log.Name = "Log";
            this.Log.Size = new System.Drawing.Size(189, 26);
            this.Log.TabIndex = 9;
            this.Log.Text = "Логін";
            this.Log.Click += new System.EventHandler(this.Log_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(334, 341);
            this.Controls.Add(this.Log);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Login);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Password);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoginForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private Bunifu.Framework.UI.BunifuFlatButton Login;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private WindowsFormsControlLibrary1.BunifuCustomTextbox Password;
        private WindowsFormsControlLibrary1.BunifuCustomTextbox Log;
    }
}

