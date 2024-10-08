using System.Text;
using PCSC;
using PCSC.Iso7816;

//This file documents the various APDU commands used to request data from MyKad

namespace Malaysian_NRIC_Reader
{
    internal class MyKadReader
    {
        ISCardContext ctx = ContextFactory.Instance.Establish(SCardScope.System);
        string nameOfReader;
        IsoReader reader;
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
        public void slctApp() //Select application
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
        public byte[] getData(byte ssSetL1, byte ssSetL2, byte[] dataSlctF, byte ssGetD) //Transmit command data, and receive resulting data
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
            slctApp();
        }
        /*------------------------------------------------------------------------------------------------------------------------------------------*/
        public string readName() //Reads and returns MyKad Name
        {

            sendData = [0x01, 0x00, 0x01, 0x00, 0x03, 0x00, 0x28, 0x00];
            gotData = getData(0x28, 0x00, sendData, 0x28);
            return (Encoding.ASCII.GetString(gotData)).Trim();
        }
        /*------------------------------------------------------------------------------------------------------------------------------------------*/
        public string readNric() //Reads and returns IC number of MyKad
        {
            sendData = [0x01, 0x00, 0x01, 0x00, 0x11, 0x01, 0x0D, 0x00];
            gotData = getData(0x0D, 0x00, sendData, 0x0D);
            return (Encoding.ASCII.GetString(gotData)).Trim();
        }
        /*------------------------------------------------------------------------------------------------------------------------------------------*/
        public string readGender() //Reads and returns gender 
        {
            sendData = [0x01, 0x00, 0x01, 0x00, 0x1E, 0x01, 0x01, 0x00];
            gotData = getData(0x01, 0x00, sendData, 0x01);
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
        public string readDob() //Reads the data of birth
        {
            sendData = [0x01, 0x00, 0x01, 0x00, 0x27, 0x01, 0x04, 0x00];
            gotData = getData(0x04, 0x00, sendData, 0x04);
            string dob = BitConverter.ToString(gotData);
            string[] DobArr = dob.Split("-");
            dob = DobArr[3] + "/" + DobArr[2] + "/" + DobArr[0] + DobArr[1];
            return dob;
        }
        
        /*------------------------------------------------------------------------------------------------------------------------------------------*/
        public string readRace() //Reads the ethnicity in the MyKad
        {
            sendData = [0x01, 0x00, 0x01, 0x00, 0x5A, 0x01, 0x19, 0x00];
            gotData = getData(0x19, 0x00, sendData, 0x19);
            return (Encoding.ASCII.GetString(gotData)).Trim();
        }
        /*------------------------------------------------------------------------------------------------------------------------------------------*/
        public string readStreet() //Reads the street part of the address stored in MyKad
        {
            sendData = [0x04, 0x00, 0x01, 0x00, 0x03, 0x00, 0x1E, 0x00];
            gotData = getData(0x1E, 0x00, sendData, 0x1E);
            string Street1 = Encoding.ASCII.GetString(gotData);
            Street1 = Street1.Trim();

            sendData = [0x04, 0x00, 0x01, 0x00, 0x21, 0x00, 0x1E, 0x00];
            gotData = getData(0x1E, 0x00, sendData, 0x1E);
            string Street2 = Encoding.ASCII.GetString(gotData);
            Street2 = Street2.Trim();

            return (Street1 + " " + Street2);
        }
        /*------------------------------------------------------------------------------------------------------------------------------------------*/
        public string readPostcode() //Reads the Postcode stored in MyKad
        {
            sendData = [0x04, 0x00, 0x01, 0x00, 0x5D, 0x00, 0x03, 0x00];
            gotData = getData(0x03, 0x00, sendData, 0x03);
            string postcode = (BitConverter.ToString(gotData)).Trim();
            postcode = postcode.Replace("-", "");
            postcode = postcode.Remove(postcode.Length - 1, 1);
            return postcode;
        }
        /*------------------------------------------------------------------------------------------------------------------------------------------*/
        public string readCity() //Reads the City stored in MyKad
        {
            sendData = [0x04, 0x00, 0x01, 0x00, 0x60, 0x00, 0x19, 0x00];
            gotData = getData(0x19, 0x00, sendData, 0x19);
            return (Encoding.ASCII.GetString(gotData)).Trim();
        }
        /*------------------------------------------------------------------------------------------------------------------------------------------*/
        public string readState() //Reads the State stored in MyKad
        {
            sendData = [0x04, 0x00, 0x01, 0x00, 0x79, 0x00, 0x1E, 0x00];
            gotData = getData(0x1E, 0x00, sendData, 0x1E);
            return (Encoding.ASCII.GetString(gotData)).Trim();
        }
        /*------------------------------------------------------------------------------------------------------------------------------------------*/
        public async Task<Image> readImage() //returns photo image of MyKad holder
        {
            Image? img = null;
            await Task.Run(() =>
            {
                byte[] firstOffset = { 0x03, 0xFD, 0xF7, 0xF1, 0xEB, 0xE5, 0xDF, 0xD9, 0xD3, 0xCD, 0xC7, 0xC1, 0xBB, 0xB5, 0xAF, 0xA9 };
                byte[] secondOffset = { 0x00, 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E };
                byte[] arr = [];
                byte[] offsetData = Array.Empty<byte>();
                byte[] photoData = Array.Empty<byte>();
                for (int i = 0; i <= 15; i++)
                {
                    offsetData = [0x02, 0x00, 0x01, 0x00, firstOffset[i], secondOffset[i], 0xFA, 0x00];
                    photoData = getData(0xFA, 0x00, offsetData, 0xFA);
                    arr = arr.Concat(photoData).ToArray();
                }
                img = (Bitmap)(new ImageConverter().ConvertFrom(arr));
            });
            
            return img;
        }
    }
}

