using BuisnessLayer.Interface;
using BuisnessLayer.Services;
using Speaker.leison.Business_layer.Interface;
using Speaker.leison.Business_layer.Services;
using Speaker.leison.EventArguments;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.Remoting.Channels;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Speaker.leison.Forms
{
    public partial class Index : Form
    {
        private readonly Ichanell channels;
        private readonly IInfo info;
        private readonly ITranscoder transcoder;

        public Index()
        {
            channels = new ChanellServices();
            InitializeComponent();
        }

        public  bool Dosome()
        {
            Index_Load(new object(),new EventArgs());
            return true;
        }
        public async void Index_Load(object sender, EventArgs e)
        {
            this.Visible = true;

            ArkhisSaxeli.Text = SendDataWithButton.Port;

            CenterFormOnScreen();
            this.Text = "შეტყობინება";

            
            await Task.Delay(500); // Delay to allow form to fully load
        }

        private void CenterFormOnScreen()
        {
            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            int formWidth = this.Width;
            int formHeight = this.Height;
            int x = (screenWidth - formWidth) / 2;
            int y = (screenHeight - formHeight) / 2;
            this.Location = new Point(x, y);
        }

        private void GilakiYes_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Index_Load(sender, e); // Reload the form
        }

        private void GilakiNo_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Index_Load(sender, e); // Reload the form
        }

        private void Satauri_Click(object sender, EventArgs e)
        {
            // Handle Satauri click event
        }
    }
}
