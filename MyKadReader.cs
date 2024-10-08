using System.Text;
using PCSC;
using PCSC.Iso7816;

namespace Malaysian_NRIC_Reader
{
    internal class MyKadReader
    {
        ISCardContext ctx = ContextFactory.Instance.Establish(SCardScope.System);
        string nameOfReader;
        IsoReader reader;
        int yob = 0;
        byte[] sendData; //data command to send
        byte[] gotData; //data returned
        public MyKadReader()
        {
            string[] readerNames = ctx.GetReaders();
            nameOfReader = string.Empty;

            nameOfReader = readerNames[0];

            reader = new IsoReader(
                        context: ctx,
                        readerName: nameOfReader,
                        mode: SCardShareMode.Shared,
                        protocol: SCardProtocol.Any,
                        releaseContextOnDispose: true);
        }
        /*------------------------------------------------------------------------------------------------------------------------------------------*/
        public void slctapp() //Select application
        {
            var apduSlctApp = new CommandApdu(IsoCase.Case4Extended, SCardProtocol.Any)
            {
                CLA = 0x00,
                INS = 0xA4,
                P1 = 0x04,
                P2 = 0x00,
                P3 = 0x0A,
                Data = [0xA0, 0x00, 0x00, 0x00, 0x74, 0x4A, 0x50, 0x4E, 0x00, 0x10]
            };
            var responseSlctApp = reader.Transmit(apduSlctApp);

            var apduAppResp = new CommandApdu(IsoCase.Case2Extended, SCardProtocol.Any)
            {
                CLA = 0x00,
                INS = 0xC0,
                P1 = 0x00,
                P2 = 0x00,
                P3 = 0x05
            };
            var responseAppResp = reader.Transmit(apduAppResp);
        }
        /*------------------------------------------------------------------------------------------------------------------------------------------*/
        public byte[] getdata(byte ssSetL1, byte ssSetL2, byte[] dataSlctF, byte ssGetD) //Transmit command data, and receive resulting data
        {

            var apduSetL = new CommandApdu(IsoCase.Case4Extended, SCardProtocol.Any)
            {
                CLA = 0xC8,
                INS = 0x32,
                P1 = 0x00,
                P2 = 0x00,
                P3 = 0x05,
                Data = [0x08, 0x00, 0x00, ssSetL1, ssSetL2]
            };
            var responseSetL = reader.Transmit(apduSetL);

            var apduSlctF = new CommandApdu(IsoCase.Case4Extended, SCardProtocol.Any)
            {
                CLA = 0xCC,
                INS = 0x00,
                P1 = 0x00,
                P2 = 0x00,
                P3 = 0x08,
                Data = dataSlctF
            };
            var resposeSlctF = reader.Transmit(apduSlctF);

            var apduGetD = new CommandApdu(IsoCase.Case2Extended, SCardProtocol.Any)
            {
                CLA = 0xCC,
                INS = 0x06,
                P1 = 0x00,
                P2 = 0x00,
                P3 = ssGetD
            };
            var responseGetD = reader.Transmit(apduGetD);
            var endata = responseGetD.GetData();
            return endata;
        }
        public void release() //Release the context
        {
            ctx.Dispose();
        }
        /*------------------------------------------------------------------------------------------------------------------------------------------*/
        public void initiateConnection() //Initiates connection to card, and selects application
        {
            slctapp();
        }
        /*------------------------------------------------------------------------------------------------------------------------------------------*/
        public string readName() //Reads and returns MyKad Name
        {

            sendData = new byte[] { 0x01, 0x00, 0x01, 0x00, 0x03, 0x00, 0x28, 0x00 };
            gotData = getdata(0x28, 0x00, sendData, 0x28);
            return (Encoding.ASCII.GetString(gotData)).Trim();
        }
        /*------------------------------------------------------------------------------------------------------------------------------------------*/
        public string readICPassport() //Reads and returns IC number of MyKad
        {
            sendData = new byte[] { 0x01, 0x00, 0x01, 0x00, 0x11, 0x01, 0x0D, 0x00 };
            gotData = getdata(0x0D, 0x00, sendData, 0x0D);
            return (Encoding.ASCII.GetString(gotData)).Trim();
        }
        /*------------------------------------------------------------------------------------------------------------------------------------------*/
        public string readGender() //Reads and returns gender 
        {
            sendData = new byte[] { 0x01, 0x00, 0x01, 0x00, 0x1E, 0x01, 0x01, 0x00 };
            gotData = getdata(0x01, 0x00, sendData, 0x01);
            string gender = Encoding.ASCII.GetString(gotData);
            if (gender == "L")
            {
                return "M";
            }
            else
            {
                return "F";
            }
        }
        /*------------------------------------------------------------------------------------------------------------------------------------------*/
        public string readDOBsetYOB() //Reads the data of birth, and records the year of birth for later use
        {
            sendData = new byte[] { 0x01, 0x00, 0x01, 0x00, 0x27, 0x01, 0x04, 0x00 };
            gotData = getdata(0x04, 0x00, sendData, 0x04);
            string dob = BitConverter.ToString(gotData);
            string[] DobArr = dob.Split("-");
            dob = DobArr[3] + "/" + DobArr[2] + "/" + DobArr[0] + DobArr[1];
            yob = DateTime.Parse(dob).Year;
            return dob;
        }
        /*------------------------------------------------------------------------------------------------------------------------------------------*/
        public string calcAge() //calculates age of MyKad holder based on deduced year of birth. Does not consider month holder was born. Year only
        {
            DateTime now = DateTime.Now;
            int age = now.Year - yob;
            return age.ToString();
        }
        /*------------------------------------------------------------------------------------------------------------------------------------------*/
        public string readRace() //Reads the ethnicity in the MyKad
        {
            sendData = new byte[] { 0x01, 0x00, 0x01, 0x00, 0x5A, 0x01, 0x19, 0x00 };
            gotData = getdata(0x19, 0x00, sendData, 0x19);
            return (Encoding.ASCII.GetString(gotData)).Trim();
        }
        /*------------------------------------------------------------------------------------------------------------------------------------------*/
        public string readStreet() //Reads the street part of the address stored in MyKad
        {
            sendData = new byte[] { 0x04, 0x00, 0x01, 0x00, 0x03, 0x00, 0x1E, 0x00 };
            gotData = getdata(0x1E, 0x00, sendData, 0x1E);
            string Street1 = Encoding.ASCII.GetString(gotData);
            Street1 = Street1.Trim();
            string[] strarr = Street1.Split();

            sendData = new byte[] { 0x04, 0x00, 0x01, 0x00, 0x21, 0x00, 0x1E, 0x00 };
            gotData = getdata(0x1E, 0x00, sendData, 0x1E);
            string Street2 = Encoding.ASCII.GetString(gotData);
            Street2 = Street2.Trim();

            return (Street1 + " " + Street2);
        }
        /*------------------------------------------------------------------------------------------------------------------------------------------*/
        public string readPostcode() //Reads the Postcode stored in MyKad
        {
            sendData = new byte[] { 0x04, 0x00, 0x01, 0x00, 0x5D, 0x00, 0x03, 0x00 };
            gotData = getdata(0x03, 0x00, sendData, 0x03);
            string postcode = (BitConverter.ToString(gotData)).Trim();
            postcode = postcode.Replace("-", "");
            postcode = postcode.Remove(postcode.Length - 1, 1);
            return postcode;
        }
        /*------------------------------------------------------------------------------------------------------------------------------------------*/
        public string readCity() //Reads the City stored in MyKad
        {
            sendData = new byte[] { 0x04, 0x00, 0x01, 0x00, 0x60, 0x00, 0x19, 0x00 };
            gotData = getdata(0x19, 0x00, sendData, 0x19);
            return (Encoding.ASCII.GetString(gotData)).Trim();
        }
        /*------------------------------------------------------------------------------------------------------------------------------------------*/
        public string readState() //Reads the State stored in MyKad
        {
            sendData = new byte[] { 0x04, 0x00, 0x01, 0x00, 0x79, 0x00, 0x1E, 0x00 };
            gotData = getdata(0x1E, 0x00, sendData, 0x1E);
            return (Encoding.ASCII.GetString(gotData)).Trim();
        }
        /*------------------------------------------------------------------------------------------------------------------------------------------*/
        public Image readImage() //returns photo image of MyKad holder
        {
            byte[] firstOff = { 0x03, 0xFD, 0xF7, 0xF1, 0xEB, 0xE5, 0xDF, 0xD9, 0xD3, 0xCD, 0xC7, 0xC1, 0xBB, 0xB5, 0xAF, 0xA9 };
            byte[] secOff = { 0x00, 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E };
            byte[] arr = Array.Empty<byte>();
            byte[] ph1Data = Array.Empty<byte>();
            byte[] Ph1data = Array.Empty<byte>();
            for (int i = 0; i <= 15; i++)
            {
                ph1Data = new byte[] { 0x02, 0x00, 0x01, 0x00, firstOff[i], secOff[i], 0xFA, 0x00 };
                Ph1data = getdata(0xFA, 0x00, ph1Data, 0xFA);
                arr = arr.Concat(Ph1data).ToArray();
            }
            Image img = (Bitmap)((new ImageConverter()).ConvertFrom(arr));
            return img;

        }
    }
}

