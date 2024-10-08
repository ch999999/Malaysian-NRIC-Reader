namespace Malaysian_NRIC_Reader
{
    partial class MyKad_Reader_Form
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            imgHolder = new PictureBox();
            txtName = new TextBox();
            txtNric = new TextBox();
            txtRace = new TextBox();
            txtPostcode = new TextBox();
            txtStreet = new TextBox();
            lblName = new Label();
            lblGender = new Label();
            lblRace = new Label();
            lblDob = new Label();
            lblAge = new Label();
            lblNric = new Label();
            cmbGender = new ComboBox();
            dtpDob = new DateTimePicker();
            lblCity = new Label();
            lblPostcode = new Label();
            lblStreet = new Label();
            lblState = new Label();
            txtState = new TextBox();
            txtCity = new TextBox();
            btnRead = new Button();
            ((System.ComponentModel.ISupportInitialize)imgHolder).BeginInit();
            SuspendLayout();
            // 
            // imgHolder
            // 
            imgHolder.BorderStyle = BorderStyle.FixedSingle;
            imgHolder.Location = new Point(389, 9);
            imgHolder.Name = "imgHolder";
            imgHolder.Size = new Size(128, 146);
            imgHolder.SizeMode = PictureBoxSizeMode.AutoSize;
            imgHolder.TabIndex = 0;
            imgHolder.TabStop = false;
            // 
            // txtName
            // 
            txtName.Location = new Point(77, 9);
            txtName.Name = "txtName";
            txtName.Size = new Size(284, 23);
            txtName.TabIndex = 2;
            // 
            // txtNric
            // 
            txtNric.Location = new Point(77, 44);
            txtNric.Name = "txtNric";
            txtNric.Size = new Size(284, 23);
            txtNric.TabIndex = 3;
            // 
            // txtRace
            // 
            txtRace.Location = new Point(77, 145);
            txtRace.Name = "txtRace";
            txtRace.Size = new Size(284, 23);
            txtRace.TabIndex = 4;
            // 
            // txtPostcode
            // 
            txtPostcode.Location = new Point(77, 241);
            txtPostcode.Name = "txtPostcode";
            txtPostcode.Size = new Size(167, 23);
            txtPostcode.TabIndex = 5;
            // 
            // txtStreet
            // 
            txtStreet.Location = new Point(77, 179);
            txtStreet.Multiline = true;
            txtStreet.Name = "txtStreet";
            txtStreet.Size = new Size(284, 53);
            txtStreet.TabIndex = 6;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(12, 13);
            lblName.Name = "lblName";
            lblName.Size = new Size(42, 15);
            lblName.TabIndex = 7;
            lblName.Text = "Name:";
            // 
            // lblGender
            // 
            lblGender.AutoSize = true;
            lblGender.Location = new Point(10, 81);
            lblGender.Name = "lblGender";
            lblGender.Size = new Size(48, 15);
            lblGender.TabIndex = 8;
            lblGender.Text = "Gender:";
            // 
            // lblRace
            // 
            lblRace.AutoSize = true;
            lblRace.Location = new Point(13, 148);
            lblRace.Name = "lblRace";
            lblRace.Size = new Size(35, 15);
            lblRace.TabIndex = 9;
            lblRace.Text = "Race:";
            // 
            // lblDob
            // 
            lblDob.AutoSize = true;
            lblDob.Location = new Point(12, 115);
            lblDob.Name = "lblDob";
            lblDob.Size = new Size(34, 15);
            lblDob.TabIndex = 10;
            lblDob.Text = "DOB:";
            // 
            // lblAge
            // 
            lblAge.AutoSize = true;
            lblAge.Location = new Point(195, 116);
            lblAge.Name = "lblAge";
            lblAge.Size = new Size(49, 15);
            lblAge.TabIndex = 11;
            lblAge.Text = "Age: ???";
            // 
            // lblNric
            // 
            lblNric.AutoSize = true;
            lblNric.Location = new Point(12, 48);
            lblNric.Name = "lblNric";
            lblNric.Size = new Size(37, 15);
            lblNric.TabIndex = 12;
            lblNric.Text = "NRIC:";
            // 
            // cmbGender
            // 
            cmbGender.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGender.FormattingEnabled = true;
            cmbGender.Items.AddRange(new object[] { "--Select--", "Male", "Female" });
            cmbGender.Location = new Point(77, 77);
            cmbGender.Name = "cmbGender";
            cmbGender.RightToLeft = RightToLeft.No;
            cmbGender.Size = new Size(99, 23);
            cmbGender.TabIndex = 13;
            // 
            // dtpDob
            // 
            dtpDob.Format = DateTimePickerFormat.Short;
            dtpDob.Location = new Point(77, 112);
            dtpDob.Name = "dtpDob";
            dtpDob.Size = new Size(99, 23);
            dtpDob.TabIndex = 14;
            // 
            // lblCity
            // 
            lblCity.AutoSize = true;
            lblCity.Location = new Point(15, 278);
            lblCity.Name = "lblCity";
            lblCity.Size = new Size(31, 15);
            lblCity.TabIndex = 15;
            lblCity.Text = "City:";
            // 
            // lblPostcode
            // 
            lblPostcode.AutoSize = true;
            lblPostcode.Location = new Point(13, 244);
            lblPostcode.Name = "lblPostcode";
            lblPostcode.Size = new Size(59, 15);
            lblPostcode.TabIndex = 16;
            lblPostcode.Text = "Postcode:";
            // 
            // lblStreet
            // 
            lblStreet.AutoSize = true;
            lblStreet.Location = new Point(13, 181);
            lblStreet.Name = "lblStreet";
            lblStreet.Size = new Size(40, 15);
            lblStreet.TabIndex = 17;
            lblStreet.Text = "Street:";
            // 
            // lblState
            // 
            lblState.AutoSize = true;
            lblState.Location = new Point(15, 309);
            lblState.Name = "lblState";
            lblState.Size = new Size(36, 15);
            lblState.TabIndex = 18;
            lblState.Text = "State:";
            // 
            // txtState
            // 
            txtState.Location = new Point(77, 306);
            txtState.Name = "txtState";
            txtState.Size = new Size(167, 23);
            txtState.TabIndex = 19;
            // 
            // txtCity
            // 
            txtCity.Location = new Point(77, 274);
            txtCity.Name = "txtCity";
            txtCity.Size = new Size(167, 23);
            txtCity.TabIndex = 20;
            // 
            // btnRead
            // 
            btnRead.Location = new Point(242, 342);
            btnRead.Name = "btnRead";
            btnRead.Size = new Size(75, 23);
            btnRead.TabIndex = 21;
            btnRead.Text = "Read";
            btnRead.UseVisualStyleBackColor = true;
            btnRead.Click += btnRead_Click;
            // 
            // MyKad_Reader_Form
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(559, 397);
            Controls.Add(btnRead);
            Controls.Add(txtCity);
            Controls.Add(txtState);
            Controls.Add(lblState);
            Controls.Add(lblStreet);
            Controls.Add(lblPostcode);
            Controls.Add(lblCity);
            Controls.Add(dtpDob);
            Controls.Add(cmbGender);
            Controls.Add(lblNric);
            Controls.Add(lblAge);
            Controls.Add(lblDob);
            Controls.Add(lblRace);
            Controls.Add(lblGender);
            Controls.Add(lblName);
            Controls.Add(txtStreet);
            Controls.Add(txtPostcode);
            Controls.Add(txtRace);
            Controls.Add(txtNric);
            Controls.Add(txtName);
            Controls.Add(imgHolder);
            Name = "MyKad_Reader_Form";
            Text = "MyKad Reader";
            Load += MyKad_Reader_Form_Load;
            ((System.ComponentModel.ISupportInitialize)imgHolder).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox imgHolder;
        private TextBox txtName;
        private TextBox txtNric;
        private TextBox txtRace;
        private TextBox txtPostcode;
        private TextBox txtStreet;
        private Label lblName;
        private Label lblGender;
        private Label lblRace;
        private Label lblDob;
        private Label lblAge;
        private Label lblNric;
        private ComboBox cmbGender;
        private DateTimePicker dtpDob;
        private Label lblCity;
        private Label lblPostcode;
        private Label lblStreet;
        private Label lblState;
        private TextBox txtState;
        private TextBox txtCity;
        private Button btnRead;
    }
}
