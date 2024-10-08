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

        private async void btnRead_Click(object sender, EventArgs e)
        {
            MyKadReader mkr = new MyKadReader();
            mkr.initiateConnection();

            txtName.Text = mkr.readName();
            txtNric.Text = mkr.readNric();
    
            string gender = mkr.readGender();
            if (gender == "M".ToUpper())
            {
                cmbGender.SelectedIndex = 1;
            }else if (gender == "F".ToUpper())
            {
                cmbGender.SelectedIndex = 2;
            }

            string dob = mkr.readDob();
            int age = DateTime.Now.Year - DateTime.Parse(dob).Year;

            dtpDob.Text = dob;
            lblAge.Text = $"Age: {age}";
            txtRace.Text = mkr.readRace();
            txtStreet.Text = mkr.readStreet();
            txtPostcode.Text = mkr.readPostcode();
            txtCity.Text = mkr.readCity();
            txtState.Text = mkr.readState();

            imgHolder.Image = await mkr.readImage();

            mkr.release();
        }
    }
}
