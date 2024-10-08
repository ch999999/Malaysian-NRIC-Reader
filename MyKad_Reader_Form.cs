namespace Malaysian_NRIC_Reader
{
    public partial class MyKad_Reader_Form : Form
    {
        public MyKad_Reader_Form()
        {
            InitializeComponent();
        }

        private void MyKad_Reader_Form_Load(object sender, EventArgs e)
        {
            cmbGender.SelectedIndex = 0;
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            MyKadReader mkr = new MyKadReader();
            mkr.initiateConnection();

            txtName.Text = mkr.readName();
            txtNric.Text = mkr.readICPassport();
    
            string gender = mkr.readGender();
            if (gender == "M")
            {
                cmbGender.SelectedIndex = 1;
            }else if (gender == "F")
            {
                cmbGender.SelectedIndex = 2;
            }

            dtpDob.Text = mkr.readDOBsetYOB();
            lblAge.Text = $"Age: {mkr.calcAge()}";
            txtRace.Text = mkr.readRace();
            txtStreet.Text = mkr.readStreet();
            txtPostcode.Text = mkr.readPostcode();
            txtCity.Text = mkr.readCity();
            txtState.Text = mkr.readState();

            imgHolder.Image = mkr.readImage();

            mkr.release();
        }
    }
}
