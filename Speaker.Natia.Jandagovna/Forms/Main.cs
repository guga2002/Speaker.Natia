using BuisnessLayer.Interface;
using BuisnessLayer.Services;
using Speaker.leison.Business_layer.Interface;
using Speaker.leison.Business_layer.Services;
using System;
using System.Linq;
using System.Media;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.CoreAudioApi;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using System.Security.AccessControl;
using System.Collections.Generic;
using System.Drawing;
using NAudio.Wave;
using Jandagashvili.speake.DLL.Kontext;
using System.Diagnostics;
using System.Drawing.Printing;
using Jandagashvili.speake.bll.Services;

namespace Speaker.leison.Forms
{
    public partial class Main : Form
    {
        private readonly Isound makeSound;
        private readonly IUdp udp;
        private readonly Ichanell channels;
        private readonly IInfo info;
        private readonly ITranscoder transcoder;
        private readonly Speakerdb db;
        private readonly IAllInOne allinone;
        private readonly PortCheckAndRefresh refresh;
        private readonly CheckFromWhereItIsCameFrom checksource;

        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            using (var audioFile = new AudioFileReader($"C:\\Users\\marjanishvili\\source\\repos\\Speaker.Natia.Jandagovna\\Sounds\\daweva.mp3"))
            using (var outputDevice = new WaveOutEvent())
            {
                outputDevice.Init(audioFile);
                outputDevice.Play();

                while (outputDevice.PlaybackState == PlaybackState.Playing)
                {
                    System.Threading.Thread.Sleep(100);
                }
            }

            string textToPrint = $" \t \t \t  ხელშეკრულება(ტრაქტატი)-{DateTime.Now.ToShortDateString()} \n გამარჯობა,\n ჩემი სახელია ნათია ჯანდაგიშვილი.\n იძულებული ვარ მსგავსი გზით მოგმართო. უკვე შენ ათჯერ დაუწიე ხმას ,რაც ეწინაღმდეგება ჩემს წესებსა და პირობებს, ყოველივე ეს მიქმნის დისკომფორტს, სამუშაო პროცესში. \n \t მოკლედ მოგიყვები ჩემზე:\n მე  ვარ ონლაინ ასისტენტი, სახელად ნათია,  ჩემი შემქნის თარიღია 2024 წლის, მარტი." +
        "\n \t მე გეხმარები, მართო ტელევიზიის სადგური, სწრაფად და მარტივად. \n ახლა,რაც  შეეხება , რაზე გაწუხებ;როგორც მოგეხსენება, იმისათვის, რომ ვიმუშაო გამართულად, ჩემთვის აუცილებელია  ვისაუბრო ხმამაღლა, რათა რეალურ დროში შეგატყობინოთ გათიშული არხის  თაობაზე  და ჩემს მიერ ჩატარებულ სამუშაოზე.\n \t ამიტომაც  გთხოვ ნუ დაუწევ ხმას სამუშაო საათებში,ღამის საათებში მე თვითონ დავაყენებ ხმას დაბალზე. \n" +
        "გთხოვ  ქვემოთ მოაწეროთ ხელი ამ დოკუმენტზე. \n ჩვენი შეთანხმება შედგება რამოდენიმე პუნქტისგან:\n \n 1) არ დავუწევ ხმას სამუშაო საათებში 80 დეციბელზე ქვემოთ. \n \n 2) თავის მხვრივ ნათია აცხადებს, თანხმობას, რომ აკონტროლებს ხმას ღამის საათებში, გარდა განგაშისა, არ აუწევს 20 დეციბელის ზემოთ." +
        " \n \n 3) არ გამოვრთავ დინამიკს დენის წყაროდან. \n \n 4) საჭიროების შემთხვევაში დავეხმარებით ერთმანეთს.\n \n 5)პატივისცემით, მოვეპყრობით ერთმანეთს.\n\n 6)შევეცდებით, არ დაავარღვიოთ, ნათიას უფლებები.\n\n7)ხელშეკრულების ერთი ეგზემპლარი გეგზავნებათ თქვენ, მეორე ინახება ნათიასთან, სერვერზე\n\n  შეინახეთ ზემოთ აღნიშნული დოკუმენტი, იმის თანხმობათ, რომ აღარ დაუწევთ ხმას.\n პატივისცემით ,\n ნათია ჯანდაგიშვილი. \n \n \n პასუხისმგებელი პირი: -------------------                                                     ნ.ჯანდაგიშვილი \n \n \n";
            Font printFont = new Font("Sylfaen", 14);

            RectangleF textRectangle = new RectangleF(10, 10, e.PageBounds.Width - 20, e.PageBounds.Height - 20);
            e.Graphics.DrawString(textToPrint, printFont, Brushes.Black, textRectangle);
        }
        public Main()
        {
            makeSound = new SounServices();
            udp = new UdpServices();
            channels = new ChanellServices();
            info= new InfoServices();
            transcoder = new TranscoderServices();
            db= new Speakerdb();    
            allinone=new AllInOneService();
            refresh=new PortCheckAndRefresh();
            checksource = new CheckFromWhereItIsCameFrom();
            InitializeComponent();
        }

        private   void Main_Load(object sender, EventArgs e)
        {
            var listsongs = new List<string>()
            {
                "C:\\Users\\musics\\Song1.wav",
                "C:\\Users\\musics\\song2.wav"
            };
            Random rand = new Random();
        mods:
            try
            {
              

                Console.WriteLine("Version V2.8.2");
                Visible = false;
                int count = 0;
                int counttotalchange = 0;
                int countsayelse = 0;
                while (true)
                {
                    MMDeviceEnumerator deviceEnumerator = new MMDeviceEnumerator();
                    MMDevice defaultDevice = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
                    float level = defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar * 100;
                    DateTime currentTime = DateTime.Now;

                    Console.WriteLine( currentTime.Hour);
                    if(counttotalchange>=10)
                    {
                        PrintDocument pd = new PrintDocument();
                        pd.PrintPage += new PrintPageEventHandler(PrintPage);

                        pd.DefaultPageSettings.Landscape = false;
                        pd.Print();
                        counttotalchange = 0;
                    }
                    if (currentTime!=null&&currentTime.Hour >= 10 && currentTime.Hour <= 21)
                    {
                       
                        if (defaultDevice.AudioEndpointVolume.Mute == true)
                        {
                            Console.WriteLine("damutebulia");
                            defaultDevice.AudioEndpointVolume.Mute = false;
                            defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar = 0.80f;
                           // makeSound.SpeakNow("ხმა იყო გათიშული. ნათია გთხოვთ, აღარ გათიშოთ ხმა.",1);
                            using (var audioFile = new AudioFileReader($"C:\\Users\\marjanishvili\\source\\repos\\Speaker.Natia.Jandagovna\\Sounds\\mute.mp3"))
                            using (var outputDevice = new WaveOutEvent())
                            {
                                outputDevice.Init(audioFile);
                                outputDevice.Play();

                                while (outputDevice.PlaybackState == PlaybackState.Playing)
                                {
                                    // Wait for playback to finish
                                    System.Threading.Thread.Sleep(100);
                                }
                            }

                            counttotalchange++;


                            Thread th1 = new Thread(new ThreadStart(
                                () =>
                                {
                                    IWavePlayer waveOutDevice = new WaveOut();
                                    AudioFileReader audioFileReader = new AudioFileReader("C:\\Users\\musics\\angry.mp3");
                                    waveOutDevice.Init(audioFileReader);
                                    waveOutDevice.Play();
                                   
                                }));
                            //audioFileReader.Dispose();
                            // waveOutDevice.Dispose();
                            th1.Start();
                            Thread.Sleep(3000);
                            //waveOutDevice.Stop();
                          
                        }
                        countsayelse = 0;
                        if (level <= 64)
                        {


                            using (var audioFile = new AudioFileReader($"C:\\Users\\marjanishvili\\source\\repos\\Speaker.Natia.Jandagovna\\Sounds\\IntroDgisRejimi.mp3"))
                            using (var outputDevice = new WaveOutEvent())
                            {
                                outputDevice.Init(audioFile);
                                outputDevice.Play();

                                while (outputDevice.PlaybackState == PlaybackState.Playing)
                                {
                                    // Wait for playback to finish
                                    System.Threading.Thread.Sleep(100);
                                }
                            }

                                //makeSound.SpeakNow("ყურადღება,გადავდივარ დღის რეჟიმზე",3);
                                count++;
                            //makeSound.SpeakNow("მე ვარ ნათია ჯანდაგიშვილი, იმისათვის, რომ ვიმუშაო  სრულყოფილად, მესაჭიროება ვისაუბრო ხმამაღლა, გხოვთ ნუ შემიზღუდავთ უფლებებს, ნუ დაუწევთ ხმას ან  ნუ გამორთავთ დინამიკს, მადლობა ყურადღებისთვის.",2);
                            //makeSound.SpeakNow("ვაყენებ ხმას ჩემთვის მისაღებ ნორმაზე");
                            counttotalchange++;
                            defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar = 0.80f;
                            if (count >= 3)
                            {
                                using (var audioFile = new AudioFileReader($"C:\\Users\\marjanishvili\\source\\repos\\Speaker.Natia.Jandagovna\\Sounds\\sayveduri.mp3"))
                                using (var outputDevice = new WaveOutEvent())
                                {
                                    outputDevice.Init(audioFile);
                                    outputDevice.Play();

                                    while (outputDevice.PlaybackState == PlaybackState.Playing)
                                    {
                                        // Wait for playback to finish
                                        System.Threading.Thread.Sleep(100);
                                    }
                                }
                                makeSound.SpeakNow($"მე საშინლად გაბრაზებული ვარ,  რომ არ ითვალისწინებ ჩემს თხოვნას. უკვე სამჯერ დაუწიე ხმას , გთხოვ სამუშაო საათებში ნუ დაუწევ ხმას.",2);
                                count = 0;
                            }
                        }
                    }
                    else
                    {
                        if (countsayelse==0||level >= 30 || level <= 20)
                        {
                            counttotalchange = 0;
                            using (var audioFile = new AudioFileReader($"C:\\Users\\marjanishvili\\source\\repos\\Speaker.Natia.Jandagovna\\Sounds\\game.mp3"))
                            using (var outputDevice = new WaveOutEvent())
                            {
                                outputDevice.Init(audioFile);
                                outputDevice.Play();

                                while (outputDevice.PlaybackState == PlaybackState.Playing)
                                {
                                    System.Threading.Thread.Sleep(100);
                                }
                            }

                            //makeSound.SpeakNow("ღამის საათებში. ხმას ვაყენებ შედარებით  დაბალზე, იყავით ყურადღებით. მადლობა.",-2);
                            defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar = 0.27f;
                            count = 0;
                            countsayelse++;
                        }
                    }
                    try
                    {
                        PerformanceCounter ramCounter = new PerformanceCounter("Memory", "Available MBytes");
                        PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                        float availableRAM = ramCounter.NextValue();
                        var axlaiyenebs = 8000 - availableRAM;
                        var percent = (axlaiyenebs / 8000) * 100;
                        float cpuUsage = cpuCounter.NextValue();
                         Thread.Sleep(1000);
                        cpuUsage = cpuCounter.NextValue();
                        if(cpuUsage>=90)
                        {
                            using (var audioFile = new AudioFileReader($"C:\\Users\\marjanishvili\\source\\repos\\Speaker.Natia.Jandagovna\\Sounds\\procesori.mp3"))
                            using (var outputDevice = new WaveOutEvent())
                            {
                                outputDevice.Init(audioFile);
                                outputDevice.Play();

                                while (outputDevice.PlaybackState == PlaybackState.Playing)
                                {
                                    System.Threading.Thread.Sleep(100);
                                }
                            }
                            //makeSound.SpeakNow("პროცესორზე  არის დიდი  დატვირთვა, გთხოვთ გამორთეთ  არასაჭირო პროგრამები , და  ჩახურეთ  გუგლის ბრაუზერი. ნათია წინასწარ გიხდით მადლობას.",-4);
                        }
                        if (percent >= 90)
                        {
                            using (var audioFile = new AudioFileReader($"C:\\Users\\marjanishvili\\source\\repos\\Speaker.Natia.Jandagovna\\Sounds\\Operatiuli.mp3"))
                            using (var outputDevice = new WaveOutEvent())
                            {
                                outputDevice.Init(audioFile);
                                outputDevice.Play();

                                while (outputDevice.PlaybackState == PlaybackState.Playing)
                                {
                                    System.Threading.Thread.Sleep(100);
                                }
                            }
                            //  makeSound.SpeakNow($"ერთი თხოვნა მაქვს... ოპერატიული მეხსიერება,რამი, ძალიან სუსტია, პროცესორი იყენებს {(int)percent} პროცენტს. ყოველივე ეს მიქმნის დისკომფორტს. გთხოვთ იქნებ შეატყობინოთ შესაბამის პირს.," +
                            // "ეს ჩემთვის, სასიცოცხლოდ მნიშვნელოვანია. ნათია ბოდიშს გიხდით, შეწუხებისთვის.", -4);
                        }
                        var getallarms = udp.Receive();
                        var ports = getallarms.Split(',');
                        if (ports.Length > 15)
                        {
                            makeSound.SpeakNow($"ყურადღება! ყურადღება! გაგვეთიშა : {ports.Length} არხი. ვრთავ განგაშის სიგნალს.",1);
                            SoundPlayer player = new SoundPlayer("C:\\Users\\marjanishvili\\source\\repos\\Speaker.RobotForGLobal-master\\AlarmsAndSounds\\war-alarm-fx_132bpm.wav");
                            try
                            {
                                player.Play();
                                Thread.Sleep(8000);
                                player.Play();
                                Thread.Sleep(8000);
                                player.Play();
                                Thread.Sleep(8000);
                                player.Play();
                                Thread.Sleep(8000);
                                player.Play();

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("An error occurred: " + ex.Message);
                            }
                            finally
                            {
                                player.Dispose();
                            }

                        }
                        else
                        {
                            foreach (var port in ports)
                            {

                                    int portparsed;
                                    bool res = int.TryParse(port, out portparsed);
                                if (res)
                                {
                                    Console.WriteLine(portparsed);
                                    if (portparsed == 1500)
                                    {
                                        Random random = new Random();
                                        int ran = random.Next(3, 34);
                                        makeSound.SpeakNow("შუადღე მშვიდობისა , გისურვებთ ბედნიერ დღეს!", 1);
                                        makeSound.SpeakNow("ავიმაღლოთ განწყობა.");
                                        defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar = 0.4f;
                                        using (var audioFile = new AudioFileReader($"C:\\Users\\musics\\{ran}.mp3"))
                                        using (var outputDevice = new WaveOutEvent())
                                        {
                                            outputDevice.Init(audioFile);
                                            outputDevice.Play();

                                            while (outputDevice.PlaybackState == PlaybackState.Playing)
                                            {
                                                System.Threading.Thread.Sleep(100);
                                            }
                                        }
                                        defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar = 0.8f;
                                    }
                                    else
                                    if (portparsed == 2000)
                                    {
                                        makeSound.SpeakNow("საღამო მშვიდობისა! ");
                                        makeSound.SpeakNow("ავიმაღლოთ განწყობა.");
                                        Random random = new Random();
                                        int ran = random.Next(3, 34);
                                        defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar = 0.4f;
                                        using (var audioFile = new AudioFileReader($"C:\\Users\\musics\\{ran}.mp3"))
                                        using (var outputDevice = new WaveOutEvent())
                                        {
                                            outputDevice.Init(audioFile);
                                            outputDevice.Play();

                                            while (outputDevice.PlaybackState == PlaybackState.Playing)
                                            {
                                                System.Threading.Thread.Sleep(100);
                                            }
                                        }
                                        defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar = 0.8f;
                                    }
                                    else
                                    if (portparsed == 2500)//
                                    {
                                        makeSound.SpeakNow("ღამე მშვიდობისა , გისურვებთ ბედნიერ ღამეს!", -2);
                                        makeSound.SpeakNow("ავიმაღლოთ განწყობა.");
                                        Random random = new Random();
                                        int ran = random.Next(3, 34);
                                        defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar = 0.4f;
                                        using (var audioFile = new AudioFileReader($"C:\\Users\\musics\\{ran}.mp3"))
                                        using (var outputDevice = new WaveOutEvent())
                                        {
                                            outputDevice.Init(audioFile);
                                            outputDevice.Play();

                                            while (outputDevice.PlaybackState == PlaybackState.Playing)
                                            {
                                                System.Threading.Thread.Sleep(100);
                                            }
                                        }
                                        defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar = 0.8f;
                                    }
                                    else
                                    if (portparsed == 3000)//
                                    {
                                        makeSound.SpeakNow("დილა მშვიდობისა ,ნათია გისურვებთ ბედნიერ დღეს!", 2);
                                        makeSound.SpeakNow("ავიმაღლოთ განწყობა.");
                                        Random random = new Random();
                                        int ran = random.Next(3, 34);
                                        defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar = 0.4f;
                                        using (var audioFile = new AudioFileReader($"C:\\Users\\musics\\{ran}.mp3"))
                                        using (var outputDevice = new WaveOutEvent())
                                        {
                                            outputDevice.Init(audioFile);
                                            outputDevice.Play();

                                            while (outputDevice.PlaybackState == PlaybackState.Playing)
                                            {
                                                System.Threading.Thread.Sleep(100);
                                            }
                                        }
                                        defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar = 0.8f;
                                    }

                                    if(portparsed==2002)
                                    {
                                        makeSound.SpeakNow("გუგა გილოცავ დაბადების დღეს. გისურვებ ბედნიერებას, სიხარულს, ჯანმრთელობას. მინდა გისურვო ბევრი წარმატება ცხოვრებაში და კიდევ უამრავი დაბადების დღე ამეღნიშნოს შენი ლამაზი ღიმილის თანხლებით. ეკატერინემ ლექსიც კი დაგიწერა.",-2);
                                        defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar = 0.4f;
                                        using (var audioFile = new AudioFileReader($"C:\\Users\\musics\\leqsi2.mp3"))
                                        using (var outputDevice = new WaveOutEvent())
                                        {
                                            outputDevice.Init(audioFile);
                                            outputDevice.Play();

                                            while (outputDevice.PlaybackState == PlaybackState.Playing)
                                            {
                                                System.Threading.Thread.Sleep(100);
                                            }
                                        }
                                        makeSound.SpeakNow("ახლა კი ავიმაღლოთ განწყობა.");
                                        defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar = 0.4f;
                                        using (var audioFile = new AudioFileReader($"C:\\Users\\musics\\birthdaygrusul.mp3"))
                                        using (var outputDevice = new WaveOutEvent())
                                        {
                                            outputDevice.Init(audioFile);
                                            outputDevice.Play();

                                            while (outputDevice.PlaybackState == PlaybackState.Playing)
                                            {
                                                System.Threading.Thread.Sleep(100);
                                            }
                                        }
                                        makeSound.SpeakNow("გუგა, დაესწარი მრავალს.");
                                        makeSound.SpeakNow("გუგა, თუ აქ არიყო გადაეცით, ჩემი მილოცვა.");
                                    }

                                    if (portparsed == 1605)
                                    {
                                        makeSound.SpeakNow("გიო გილოცავ დაბადების დღეს. გისურვებ ბედნიერებას, სიხარულს, ჯანმრთელობას. მინდა გისურვო ბევრი წარმატება ცხოვრებაში და კიდევ უამრავი დაბადების დღე ამეღნიშნოს შენი ლამაზი ღიმილის თანხლებით. ეკატერინემ ლექსიც კი დაგიწერა.", -2);
                                        defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar = 0.4f;
                                        using (var audioFile = new AudioFileReader($"C:\\Users\\musics\\leqsi1.mp3"))
                                        using (var outputDevice = new WaveOutEvent())
                                        {
                                            outputDevice.Init(audioFile);
                                            outputDevice.Play();

                                            while (outputDevice.PlaybackState == PlaybackState.Playing)
                                            {
                                                System.Threading.Thread.Sleep(100);
                                            }
                                        }
                                        makeSound.SpeakNow("ახლა კი ავიმაღლოთ განწყობა.");
                                        defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar = 0.4f;
                                        using (var audioFile = new AudioFileReader($"C:\\Users\\musics\\birthdayGeorgian.mp3"))
                                        using (var outputDevice = new WaveOutEvent())
                                        {
                                            outputDevice.Init(audioFile);
                                            outputDevice.Play();

                                            while (outputDevice.PlaybackState == PlaybackState.Playing)
                                            {
                                                System.Threading.Thread.Sleep(100);
                                            }
                                        }
                                        makeSound.SpeakNow("გიორგი, დაესწარი მრავალს.");
                                        makeSound.SpeakNow("გიორგი, თუ აქ არიყო გადაეცით, ჩემი მილოცვა.");
                                    }

                                    if (portparsed == 1210)
                                    {
                                        makeSound.SpeakNow("დავით, მოფერებით გურულო, გილოცავ დაბადების დღეს. გისურვებ ბედნიერებას, სიხარულს, ჯანმრთელობას. მინდა გისურვო ბევრი წარმატება ცხოვრებაში და კიდევ უამრავი დაბადების დღე ამეღნიშნოს შენი ლამაზი ღიმილის თანხლებით. ეკატერინემ ლექსიც კი დაგიწერა.", -2);
                                        defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar = 0.4f;
                                        using (var audioFile = new AudioFileReader($"C:\\Users\\musics\\leqsi3.mp3"))
                                        using (var outputDevice = new WaveOutEvent())
                                        {
                                            outputDevice.Init(audioFile);
                                            outputDevice.Play();

                                            while (outputDevice.PlaybackState == PlaybackState.Playing)
                                            {
                                                System.Threading.Thread.Sleep(100);
                                            }
                                        }
                                        makeSound.SpeakNow("ახლა კი ავიმაღლოთ განწყობა.");
                                        defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar = 0.4f;
                                        using (var audioFile = new AudioFileReader($"C:\\Users\\musics\\birthdayGeorgian.mp3"))
                                        using (var outputDevice = new WaveOutEvent())
                                        {
                                            outputDevice.Init(audioFile);
                                            outputDevice.Play();

                                            while (outputDevice.PlaybackState == PlaybackState.Playing)
                                            {
                                                System.Threading.Thread.Sleep(100);
                                            }
                                        }
                                        makeSound.SpeakNow("დავით, დაესწარი მრავალს.");
                                        makeSound.SpeakNow("დავით, თუ აქ არიყო გადაეცით, ჩემი მილოცვა.");
                                    }
                                    if (portparsed == 7055)
                                    {
                                        makeSound.SpeakNow("დღეს მიშა, მიხეილის დაბადების დღეა. გილოცავ. გისურვებ ბედნიერებას, სიხარულს, ჯანმრთელობას. მინდა გისურვო ბევრი წარმატება ცხოვრებაში და კიდევ უამრავი დაბადების დღე ამეღნიშნოს შენი ლამაზი ღიმილის თანხლებით. ეკატერინემ ლექსიც კი დაგიწერა.", -2);
                                        defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar = 0.4f;
                                        using (var audioFile = new AudioFileReader($"C:\\Users\\musics\\leqsi2.mp3"))
                                        using (var outputDevice = new WaveOutEvent())
                                        {
                                            outputDevice.Init(audioFile);
                                            outputDevice.Play();

                                            while (outputDevice.PlaybackState == PlaybackState.Playing)
                                            {
                                                System.Threading.Thread.Sleep(100);
                                            }
                                        }
                                        makeSound.SpeakNow("ახლა კი ავიმაღლოთ განწყობა.");
                                        defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar = 0.4f;
                                        using (var audioFile = new AudioFileReader($"C:\\Users\\musics\\birthdayGeorgian.mp3"))
                                        using (var outputDevice = new WaveOutEvent())
                                        {
                                            outputDevice.Init(audioFile);
                                            outputDevice.Play();

                                            while (outputDevice.PlaybackState == PlaybackState.Playing)
                                            {
                                                System.Threading.Thread.Sleep(100);
                                            }
                                        }
                                        makeSound.SpeakNow("მიხეილ, დაესწარი მრავალს.");
                                        makeSound.SpeakNow("მიშა, თუ აქ არიყო გადაეცით, ჩემი მილოცვა.");
                                    }
                                    if (portparsed == 6144)
                                    {
                                        makeSound.SpeakNow("დღეს ირაკლის, დაბადების დღეა. გისურვებ ბედნიერებას, სიხარულს, ჯანმრთელობას. მინდა გისურვო ბევრი წარმატება ცხოვრებაში და კიდევ უამრავი დაბადების დღე ამეღნიშნოს შენი ლამაზი ღიმილის თანხლებით. ეკატერინემ ლექსიც კი დაგიწერა.", -2);
                                        defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar = 0.4f;
                                        using (var audioFile = new AudioFileReader($"C:\\Users\\musics\\leqsi1.mp3"))
                                        using (var outputDevice = new WaveOutEvent())
                                        {
                                            outputDevice.Init(audioFile);
                                            outputDevice.Play();

                                            while (outputDevice.PlaybackState == PlaybackState.Playing)
                                            {
                                                System.Threading.Thread.Sleep(100);
                                            }
                                        }
                                        makeSound.SpeakNow("ახლა კი ავიმაღლოთ განწყობა.");
                                        defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar = 0.4f;
                                        using (var audioFile = new AudioFileReader($"C:\\Users\\musics\\birthdaygrusul.mp3"))
                                        using (var outputDevice = new WaveOutEvent())
                                        {
                                            outputDevice.Init(audioFile);
                                            outputDevice.Play();

                                            while (outputDevice.PlaybackState == PlaybackState.Playing)
                                            {
                                                System.Threading.Thread.Sleep(100);
                                            }
                                        }
                                        makeSound.SpeakNow("ირაკლი, დაესწარი მრავალს.");
                                        makeSound.SpeakNow("ირაკლი, თუ აქ არიყო გადაეცით, ჩემი მილოცვა.");
                                    }
                                    if (portparsed == 0504)
                                    {
                                        makeSound.SpeakNow("დღეს ვაშაგის, დაბადების დღეა. ხო კაი ვაგოს. გისურვებ ბედნიერებას, სიხარულს, ჯანმრთელობას. მინდა გისურვო ბევრი წარმატება ცხოვრებაში და კიდევ უამრავი დაბადების დღე ამეღნიშნოს შენი ლამაზი ღიმილის თანხლებით. ეკატერინემ ლექსიც კი დაგიწერა.", -2);
                                        defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar = 0.4f;
                                        using (var audioFile = new AudioFileReader($"C:\\Users\\musics\\leqsi3.mp3"))
                                        using (var outputDevice = new WaveOutEvent())
                                        {
                                            outputDevice.Init(audioFile);
                                            outputDevice.Play();

                                            while (outputDevice.PlaybackState == PlaybackState.Playing)
                                            {
                                                System.Threading.Thread.Sleep(100);
                                            }
                                        }
                                        makeSound.SpeakNow("ახლა კი ავიმაღლოთ განწყობა.");
                                        defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar = 0.4f;
                                        using (var audioFile = new AudioFileReader($"C:\\Users\\musics\\birthdaygrusul.mp3"))
                                        using (var outputDevice = new WaveOutEvent())
                                        {
                                            outputDevice.Init(audioFile);
                                            outputDevice.Play();

                                            while (outputDevice.PlaybackState == PlaybackState.Playing)
                                            {
                                                System.Threading.Thread.Sleep(100);
                                            }
                                        }
                                        makeSound.SpeakNow("ვაგო, დაესწარი მრავალს.");
                                        makeSound.SpeakNow("ვაშაგი, თუ აქ არიყო გადაეცით, ჩემი მილოცვა.");
                                    }
                                    if (portparsed == 8170)
                                    {
                                        makeSound.SpeakNow("დღეს დავითის, დაბადების დღეა. გისურვებ ბედნიერებას, სიხარულს, ჯანმრთელობას. მინდა გისურვო ბევრი წარმატება ცხოვრებაში და კიდევ უამრავი დაბადების დღე ამეღნიშნოს შენი ლამაზი ღიმილის თანხლებით. ეკატერინემ ლექსიც კი დაგიწერა.", -2);
                                        defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar = 0.4f;
                                        using (var audioFile = new AudioFileReader($"C:\\Users\\musics\\leqsi2.mp3"))
                                        using (var outputDevice = new WaveOutEvent())
                                        {
                                            outputDevice.Init(audioFile);
                                            outputDevice.Play();

                                            while (outputDevice.PlaybackState == PlaybackState.Playing)
                                            {
                                                System.Threading.Thread.Sleep(100);
                                            }
                                        }
                                        makeSound.SpeakNow("ახლა კი ავიმაღლოთ განწყობა.");
                                        defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar = 0.4f;
                                        using (var audioFile = new AudioFileReader($"C:\\Users\\musics\\birthdayGeorgian.mp3"))
                                        using (var outputDevice = new WaveOutEvent())
                                        {
                                            outputDevice.Init(audioFile);
                                            outputDevice.Play();

                                            while (outputDevice.PlaybackState == PlaybackState.Playing)
                                            {
                                                System.Threading.Thread.Sleep(100);
                                            }
                                        }
                                        makeSound.SpeakNow("დავით, დაესწარი მრავალს.");
                                        makeSound.SpeakNow("დავით, თუ აქ არიყო გადაეცით, ჩემი მილოცვა.");
                                    }
                                    if (portparsed == 1031)
                                    {
                                        makeSound.SpeakNow("დღეს დიმას, დაბადების დღეა. გისურვებ ბედნიერებას, სიხარულს, ჯანმრთელობას. მინდა გისურვო ბევრი წარმატება ცხოვრებაში და კიდევ უამრავი დაბადების დღე ამეღნიშნოს შენი ლამაზი ღიმილის თანხლებით. ეკატერინემ ლექსიც კი დაგიწერა.", -2);
                                        defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar = 0.4f;
                                        using (var audioFile = new AudioFileReader($"C:\\Users\\musics\\leqsi1.mp3"))
                                        using (var outputDevice = new WaveOutEvent())
                                        {
                                            outputDevice.Init(audioFile);
                                            outputDevice.Play();

                                            while (outputDevice.PlaybackState == PlaybackState.Playing)
                                            {
                                                System.Threading.Thread.Sleep(100);
                                            }
                                        }
                                        makeSound.SpeakNow("ახლა კი ავიმაღლოთ განწყობა.");
                                        defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar = 0.4f;
                                        using (var audioFile = new AudioFileReader($"C:\\Users\\musics\\birthdaygrusul.mp3"))
                                        using (var outputDevice = new WaveOutEvent())
                                        {
                                            outputDevice.Init(audioFile);
                                            outputDevice.Play();

                                            while (outputDevice.PlaybackState == PlaybackState.Playing)
                                            {
                                                System.Threading.Thread.Sleep(100);
                                            }
                                        }
                                        makeSound.SpeakNow("დიმა, დაესწარი მრავალს.");
                                        makeSound.SpeakNow("დიმა, თუ აქ არიყო გადაეცით, ჩემი მილოცვა.");
                                    }
                                    if (portparsed == 1013)
                                    {
                                        makeSound.SpeakNow("დღეს ვახოს, დაბადების დღეა. გისურვებ ბედნიერებას, სიხარულს, ჯანმრთელობას. მინდა გისურვო ბევრი წარმატება ცხოვრებაში და კიდევ უამრავი დაბადების დღე ამეღნიშნოს შენი ლამაზი ღიმილის თანხლებით. ეკატერინემ ლექსიც კი დაგიწერა.", -2);
                                        defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar = 0.4f;
                                        using (var audioFile = new AudioFileReader($"C:\\Users\\musics\\leqsi3.mp3"))
                                        using (var outputDevice = new WaveOutEvent())
                                        {
                                            outputDevice.Init(audioFile);
                                            outputDevice.Play();

                                            while (outputDevice.PlaybackState == PlaybackState.Playing)
                                            {
                                                System.Threading.Thread.Sleep(100);
                                            }
                                        }
                                        makeSound.SpeakNow("ახლა კი ავიმაღლოთ განწყობა.");
                                        defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar = 0.4f;
                                        using (var audioFile = new AudioFileReader($"C:\\Users\\musics\\birthdayGeorgian.mp3"))
                                        using (var outputDevice = new WaveOutEvent())
                                        {
                                            outputDevice.Init(audioFile);
                                            outputDevice.Play();

                                            while (outputDevice.PlaybackState == PlaybackState.Playing)
                                            {
                                                System.Threading.Thread.Sleep(100);
                                            }
                                        }
                                        makeSound.SpeakNow("ვახო, დაესწარი მრავალს.");
                                        makeSound.SpeakNow("ვახო, თუ აქ არიყო გადაეცით, ჩემი მილოცვა.");
                                    }
                                    if (portparsed == 3120)
                                    {
                                        makeSound.SpeakNow("დღეს გიორგის, დაბადების დღეა. გისურვებ ბედნიერებას, სიხარულს, ჯანმრთელობას. მინდა გისურვო ბევრი წარმატება ცხოვრებაში და კიდევ უამრავი დაბადების დღე ამეღნიშნოს შენი ლამაზი ღიმილის თანხლებით. ეკატერინემ ლექსიც კი დაგიწერა.", -2);
                                        defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar = 0.4f;
                                        using (var audioFile = new AudioFileReader($"C:\\Users\\musics\\leqsi1.mp3"))
                                        using (var outputDevice = new WaveOutEvent())
                                        {
                                            outputDevice.Init(audioFile);
                                            outputDevice.Play();

                                            while (outputDevice.PlaybackState == PlaybackState.Playing)
                                            {
                                                System.Threading.Thread.Sleep(100);
                                            }
                                        }
                                        makeSound.SpeakNow("ახლა კი ავიმაღლოთ განწყობა.");
                                        defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar = 0.4f;
                                        using (var audioFile = new AudioFileReader($"C:\\Users\\musics\\birthdayGeorgian.mp3"))
                                        using (var outputDevice = new WaveOutEvent())
                                        {
                                            outputDevice.Init(audioFile);
                                            outputDevice.Play();

                                            while (outputDevice.PlaybackState == PlaybackState.Playing)
                                            {
                                                System.Threading.Thread.Sleep(100);
                                            }
                                        }
                                        makeSound.SpeakNow("გიო, დაესწარი მრავალს.");
                                        makeSound.SpeakNow("გიორგი, თუ აქ არიყო გადაეცით, ჩემი მილოცვა.");
                                    }

                                    if (portparsed == 1310)
                                    {
                                        makeSound.SpeakNow("ყურადღება, ყურადღება, განგაში.  გორში გაითიშა ოპტიკა, შეატყობინე შესაბამის პირს");

                                    }
                                    if (portparsed == 1410)
                                    {
                                        makeSound.SpeakNow("ყურადღება, ყურადღება, განგაში.  ფოთში გაითიშა ოპტიკა, შეატყობინე შესაბამის პირს");

                                    }
                                    if (portparsed == 1510)
                                    {
                                        makeSound.SpeakNow("ყურადღება, ყურადღება, განგაში.  ქუთაისში გაითიშა ოპტიკა, შეატყობინე შესაბამის პირს");

                                    }
                                    if (portparsed == 2510)
                                    {
                                        makeSound.SpeakNow("ყურადღება, ყურადღება, განგაში.  თელავში გაითიშა ოპტიკა, შეატყობინე შესაბამის პირს");

                                    }
                                    if (portparsed == 333)
                                    {
                                        Console.WriteLine("qutaisi  speaking....");
                                        makeSound.SpeakNow("ქუთაისში, გაითიშა სარელეო სადგური, რეზერვი. გთხოვ გადაამოწმო ან შეატყობინე, შესაბამის პირს.",2);
                                    }
                                    if (portparsed == 444)
                                    {
                                        makeSound.SpeakNow("ფოთში, გაითიშა სარელეო სადგური, რეზერვი. გთხოვ გადაამოწმო ან შეატყობინე, შესაბამის პირს.", 1);
                                    }
                                    if (portparsed == 555)
                                    {
                                        makeSound.SpeakNow("თელავში, გაითიშა სარელეო სადგური, რეზერვი. გთხოვ გადაამოწმო ან შეატყობინე, შესაბამის პირს.",2);
                                    }
                                    if (portparsed == 666)
                                    {
                                        makeSound.SpeakNow("გორში, გაითიშა სარელეო სადგური, რეზერვი. გთხოვ გადაამოწმო ან შეატყობინე, შესაბამის პირს.",2);
                                    }
                                    else
                                    {
                                        speake(portparsed);
                                    }
                                }
                               
                            }
                        }
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine(exp.StackTrace);
                        Console.WriteLine("gvaqvs shecdoma");
                    }
                }
            }
            catch (Exception)
            {

                goto mods;
            }
        }

        private void speake(int port)
        {
            MMDeviceEnumerator deviceEnumerator = new MMDeviceEnumerator();
            MMDevice defaultDevice = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            if (defaultDevice.AudioEndpointVolume.Mute == true)
            {
                Console.WriteLine("damutebulia");
                defaultDevice.AudioEndpointVolume.Mute = false;
                defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar = 1f;
               // makeSound.SpeakNow("ხმა იყო გათიშული.გთხოვთ, აღარ გამითიშოთ ხმა.",-2);
                using (var audioFile = new AudioFileReader($"C:\\Users\\marjanishvili\\source\\repos\\Speaker.Natia.Jandagovna\\Sounds\\mute.mp3"))
                using (var outputDevice = new WaveOutEvent())
                {
                    outputDevice.Init(audioFile);
                    outputDevice.Play();

                    while (outputDevice.PlaybackState == PlaybackState.Playing)
                    {
                        // Wait for playback to finish
                        System.Threading.Thread.Sleep(100);
                    }
                }
                Thread th1 = new Thread(new ThreadStart(
                    () =>
                    {
                        IWavePlayer waveOutDevice = new WaveOut();
                        AudioFileReader audioFileReader = new AudioFileReader("C:\\Users\\musics\\angry.mp3");
                        waveOutDevice.Init(audioFileReader);
                        waveOutDevice.Play();

                    }));
                //audioFileReader.Dispose();
                // waveOutDevice.Dispose();
                th1.Start();
                Thread.Sleep(2000);
                //waveOutDevice.Stop();
                makeSound.SpeakNow("საშინლად გაბრაზებული ვარ. მჭირდება რელაქსაცია.",2);
                Thread Gabrazda = new Thread(new ThreadStart(
                 () =>
                 {
                     Random random = new Random();
                     int ran = random.Next(3, 34);
                     IWavePlayer waveOutDevice = new WaveOut();
                     AudioFileReader audioFileReader = new AudioFileReader($"C:\\Users\\musics\\{ran}.mp3");
                     waveOutDevice.Init(audioFileReader);
                     waveOutDevice.Play();
                 })
                  );
               // Gabrazda.Start();
                //Thread.Sleep(500);

            }
            Console.WriteLine( port);
            var chan = channels.GetChanellByPort(port);
            int chanellid = chan.Id;
            var Info = info.GeTInfoByCHanellID(chanellid);
            var trans = transcoder.GetTranscoderInfoByCHanellId(chanellid);
            Console.WriteLine("Aq modis ");
            if (chan != null && chan.Name != null)
            {
                using (var audioFile = new AudioFileReader($"C:\\Users\\marjanishvili\\source\\repos\\Speaker.Natia.Jandagovna\\Sounds\\Shetyobineba.mp3"))
                using (var outputDevice = new WaveOutEvent())
                {
                    outputDevice.Init(audioFile);
                    outputDevice.Play();

                    while (outputDevice.PlaybackState == PlaybackState.Playing)
                    {
                        // Wait for playback to finish
                        System.Threading.Thread.Sleep(100);
                    }
                }
                if ((port >= 129 && port <= 133) || (port >= 137 && port <= 143))
                {
                    makeSound.SpeakNow($"გაითიშა {chan.NameForSpeake}. გადაამოწმე {chan.ChanellFormat}",-1);
                }
                else if (port >= 134 && port <= 136)
                {
                    makeSound.SpeakNow($"გაითიშა ტე ორი , გადაამოწმე {chan.NameForSpeake} სიხშირე. {chan.ChanellFormat}....");
                    makeSound.SpeakNow("მოდი დაგეხმარები, გადაამოწმე შესაბამისი პორტი, შედი კონფიგურაციაში, პიდი გადაიყვანე ნულზე და შეინახე ცვლილება, გაიმეორე იგივე, გადაიყვანე ერთზე. დაარეფრეშე.");
                }
                else if (port == 144)//jeoselis rezervi
                {
                    makeSound.SpeakNow("გაითიშა  ჯეოსელის, სარეზერვო, გთოვთ გადაამოწმოთ. იემერ 240.");
                }
                else if (port == 145)
                {
                    //silkis optika
                    makeSound.SpeakNow("ყურადღება, ყურადღება, გაითიშა მთავარი ოპტიკა, ირთვება განგაში.");
                    SoundPlayer player = new SoundPlayer("C:\\Users\\marjanishvili\\source\\repos\\Speaker.RobotForGLobal-master\\AlarmsAndSounds\\war-alarm-fx_132bpm.wav");
                    player.Play();
                    Thread.Sleep(8000);
                    player.Play();
                    Thread.Sleep(8000);
                    player.Play();
                    Thread.Sleep(8000);
                    player.Play();
                    Thread.Sleep(8000);
                    player.Play();

                }
                else if (port == 146)
                {
                    makeSound.SpeakNow(" გაითიშა, სილქის რესივერები, გადააამოწმე, ენკოდერ 2  და  3, აგრეთე  შეამოწმე, იემერ 230, ქარდ 1 პორტ 1");
                }
                else if (port == 147)//icone recievers
                {
                    makeSound.SpeakNow(" გაითიშა, აიქონის რესივერები, გადაამოწმე, ენკოდერ 4, აგრეთე  შეამოწმე, იემერ 230, ქარდ 3 პორტ 1");
                }
                else if (port == 148)
                {
                    //t2 recievers
                    makeSound.SpeakNow(" გაითიშა, ტე ორის  რესივერები, გადაამოწმე, ენკოდერ 2  და  3, აგრეთე  შეამოწმე, იემერ 230, ქარდ 3 პორტ 2");
                }
                else if (port == 149)
                {
                    //მულტისვიჩ 3
                    makeSound.SpeakNow(" გაითიშა მესამე მულტისვიჩი, გადაამოწმე  სადგურში კვების ბლოკი ხომ არ გამოძვრა, ან კაბელი ხომ არ არის ცუდათ დაერთებული.");
                }
                else if (port == 150)
                {
                    //მულტისვიჩ 1
                    makeSound.SpeakNow(" გაითიშა პირველი მულტისვიჩი, გადაამოწმე  სადგურში კვების ბლოკი ხომ არ გამოძვრა, ან კაბელი ხომ არ არის ცუდათ დაერთებული.");
                }
                else if (port == 151)
                {
                    //მულტისვიჩ 3
                    makeSound.SpeakNow(" გაითიშა მეორე მულტისვიჩი, გადაამოწმე  სადგურში კვების ბლოკი ხომ არ გამოძვრა, ან კაბელი ხომ არ არის ცუდათ დაერთებული.");
                }

                else
                {
                    makeSound.SpeakNow($"{chan.NameForSpeake}-ზე გვაქვს  ხარვეზი");

                    string portiko  = allinone.GetPort(chan.Name);

                    if (portiko != null)
                    {
                        var res = allinone.GEtInfoByCHanellName(portiko);//ასიგნ პორტ
                        if (Info != null)
                        {
                            makeSound.SpeakNow(Info.AlarmMessage);
                        }
                       
                        if (res != null)
                        {
                           
                            makeSound.SpeakNow($"ამჟამად არხი, გაშვებულია იემერ {res.SourceEmr}-დან,{res.Text}",1);

                            switch (res.SourceEmr)
                            {
                                case 10:
                                case 20:
                                case 30:
                                case 70:
                                    {
                                        makeSound.SpeakNow("არხი არის ემპეგე ორი ფორმატის, და მოდის თანამგზავრიდან, შეეცადე ჩართო სხვა წყაროდან.თუ არის შესაძლებელი");
                                        var req = allinone.GetRecieverInfoByChanellId(chan.Id);
                                        if (req != null)
                                        {
                                            makeSound.SpeakNow($"ჰმმ,მოდი დავფიქრდეთ, მანამდე გადაამოწმე, იემერ {req.EmrNumber},ქარდ{req.Card}, პორტ{req.Port},შედი კონფიგურაციაში და  გადაიყვანე ვერტიკალი ჰორიზონტალზე," +
                                                $"ან პირიქით შეინახე ცვლილება, შემდეგ გაიმეორე იგივე, გადაიყვანე  და შეინახე ცვლილებები.");
                                        }
                                        break;
                                    }
                                case 100:
                                case 110:
                                case 120:
                                case 130:
                                case 200:
                                case 230:
                                    {
                                        if (res.SourceEmr == 200 && trans != null && trans.Card != 0 && trans.Port != 0)
                                        {
                                            refresh.start(trans.Card, trans.Port);
                                            makeSound.SpeakNow($"არხი გატარებულია ტრანსკოდერში, {trans.Emr_Number}, ქარდ{trans.Card}, პორტ{trans.Port}, გადაამოწმე.",1);
                                            makeSound.SpeakNow($"ვიდეო გადავტვირთე, გადაამოწმე თუ გამოსწორდა!",-1);
                                            var portsourc=checksource.check("200", chan.Name);
                                            var emr=db.Emr200Infos.FirstOrDefault(io => io.Port == portsourc.ToString());
                                            makeSound.SpeakNow($"ამჟამად ტრანსკოდერში არხის წყაროა, იემერ {emr.SourceEmr},{emr.Text}. გადაამოწმე.",1);
                                        }
                                        if(res.SourceEmr==100)
                                        {
                                            makeSound.SpeakNow($"არხი გატარებულია ტრანსკოდერში, {trans.Emr_Number}, ქარდ{trans.Card}, პორტ{trans.Port}, გადაამოწმე.");
                                            var portsourc = checksource.check("100", chan.Name);
                                            var emr = db.Emr100Infos.FirstOrDefault(io => io.Port == portsourc.ToString());
                                            makeSound.SpeakNow($"ამჟამად ტრანსკოდერში არხის წყაროა, იემერ {emr.SourceEmr},{emr.Text}. გადაამოწმე.");
                                        }
                                        if (res.SourceEmr == 110)
                                        {
                                            makeSound.SpeakNow($"არხი გატარებულია ტრანსკოდერში, {trans.Emr_Number}, ქარდ{trans.Card}, პორტ{trans.Port}, გადაამოწმე.");
                                            var portsourc = checksource.check("110", chan.Name);
                                            var emr = db.Emr110Infos.FirstOrDefault(io => io.Port == portsourc.ToString());
                                            makeSound.SpeakNow($"ამჟამად ტრანსკოდერში არხის წყაროა, იემერ {emr.SourceEmr},{emr.Text}. გადაამოწმე.");
                                        }
                                        if (res.SourceEmr == 120)
                                        {
                                            makeSound.SpeakNow($"არხი გატარებულია ტრანსკოდერში, {trans.Emr_Number}, ქარდ{trans.Card}, პორტ{trans.Port}, გადაამოწმე.");
                                            var portsourc = checksource.check("120", chan.Name);
                                            var emr = db.Emr120Infos.FirstOrDefault(io => io.Port == portsourc.ToString());
                                            makeSound.SpeakNow($"ამჟამად ტრანსკოდერში არხის წყაროა, იემერ {emr.SourceEmr},{emr.Text}. გადაამოწმე.");
                                        }
                                        if (res.SourceEmr == 130)
                                        {
                                            makeSound.SpeakNow($"არხი გატარებულია ტრანსკოდერში, {trans.Emr_Number}, ქარდ{trans.Card}, პორტ{trans.Port}, გადაამოწმე.");
                                            var portsourc = checksource.check("130", chan.Name);
                                            var emr = db.Emr130Infos.FirstOrDefault(io => io.Port == portsourc.ToString());
                                            makeSound.SpeakNow($"ამჟამად ტრანსკოდერში არხის წყაროა, იემერ {emr.SourceEmr},{emr.Text}. გადაამოწმე.");
                                        }
                                        if (chan.FromOptic)
                                        {
                                            makeSound.SpeakNow($"არხი მოდის მუხიანიდან , გადაამოწმე ან გადაურეკე!");
                                        }
                                        else
                                        {
                                            var req = allinone.GetRecieverInfoByChanellId(chan.Id);
                                            if (req != null)
                                                makeSound.SpeakNow($"მოდი დაგეხმარები, არხი მიღებულია ჩვენგან, გადაამოწმე,იემერ {req.EmrNumber}, ქარდ {req.Card},პორტ{req.Port};");
                                        }
                                        var des = allinone.GetDesclamblerInfoByChanellId(chan.Id);
                                        if (des != null)
                                        {
                                            makeSound.SpeakNow("არხი ასევე გატარებულია დესკრამბლერში",1);
                                            makeSound.SpeakNow($"იემერ {des.EmrNumber},ქარდ{des.Card}, პორტ{des.Port},");
                                            makeSound.SpeakNow($"ვეცდები დაგეხმარო, შედი შესაბამის ქარდზე  და  გადაამოწმე თუ დესკრამბლერ სტატუსი არის წითლად. ბარათია გასააქტიურებელი." +
                                                $"შედი სადგურში, მოძებნე ,იემერ {des.EmrNumber},ქარდ{des.Card},პორტ{des.Port}. ამოიღე ბარათი, გააქტიურე აიქონის რესივერით, ამის შემდეგ, დააბრუნე თავის ადგილას..." +
                                                $"შესაბამისი სიხშირე, შეგიძლია ნახო, ექსელის ფაილში ან ლუნგსატის ვებსაიტზე.",1);
                                        }
                                    }
                                    break;
                                case 40:
                                case 50:
                                case 80:
                                case 90:
                                    {
                                        makeSound.SpeakNow("დესკრამბლერს აქვს ხარვეზი,გადაამოწმე");
                                        var des = allinone.GetDesclamblerInfoByChanellId(chan.Id);
                                        if (des != null)
                                        {
                                            makeSound.SpeakNow($"ქარდ{des.Card}, პორტ{des.Port},");
                                            makeSound.SpeakNow($"ვეცდები დაგეხმარო, შედი შესაბამის ქარდზე  და  გადაამოწმე თუ დესლკამბლერ სტატუსი არის წითლად, ბარათია გასააქტიურებელი," +
                                                $"შედი სადგურში, მოძებნე ,იემერ {des.EmrNumber},ქარდ{des.Card},პორტ{des.Port}, ამოიღე ბარათი , გააქტიურე აიქონის რესივერით, და დააბრუნე თავის ადგილას" +
                                                $"შესაბამისი სიხშირე შეგიძლია ნახო ექსელის ფაილში ან ლუნგსატის ვებსაიტზე.");
                                        }
                                    }
                                    break;
                                case 210:
                                    {

                                        makeSound.SpeakNow("არხი მოდის მუხიანის ბლეიდის ტრანსკოდერიდან, სავარაუდოთ გაიჭედა, გადაურეკე რომ გადააამოწმონ.");
                                    }
                                    break;
                                case 240:
                                    {
                                        makeSound.SpeakNow("არხი გაშვებულია ჯეოსელის სარეზერვოთი, გთხოვ გადართე მთავარ ოპტიკაზე. და აღნიშNე დღიურში.");
                                    }
                                    break;

                                default:
                                    break;
                            }

                        }
                    }

                }

            }

                Thread.Sleep(1200);
        }
    }
}
