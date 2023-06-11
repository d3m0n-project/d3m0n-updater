using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Web;
using System.IO;
using System.Windows.Input;



namespace d3m0n_X1_updater
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
		public static string outDrive;
		public static string SelectedDrive="";
		
        public MainWindow()
        {
            InitializeComponent();
			
			ServicePointManager.Expect100Continue = true; 
			ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xc00);	
			
			load_ports();
        }
		public void load_ports()
		{
			DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in drives)
            {
                if (drive.DriveType == DriveType.Removable)
                {
                    if(File.Exists(drive.RootDirectory+"INFO_UF2.TXT"))
					{
						Ports.Items.Add(drive.RootDirectory);
						if(SelectedDrive=="")
						{
							SelectedDrive = drive.RootDirectory.ToString();
							Ports.Text = SelectedDrive;
						}
					}
                }
            }
		}
		private void openproject(object sender, RoutedEventArgs e)
		{
			System.Diagnostics.Process.Start("https://github.com/orgs/d3m0n-project/repositories");
		}
		private void reload(object sender, RoutedEventArgs e)
		{
			SelectedDrive="";
			load_ports();
		}
		private void install(object sender, RoutedEventArgs e)
		{
			Mouse.SetCursor(Cursors.Wait);
			string outDrive = Ports.Text.ToString()+"new.uf2";
			
			if(outDrive == "new.uf2")
			{
				MessageBox.Show("Select your D3M0N device port to start downloading firmware");
				Mouse.SetCursor(Cursors.Arrow);
				return;
			}
			
			try
			{
				new WebClient().DownloadFile("https://raw.githubusercontent.com/d3m0n-project/firmware/main/api/firmware/firmware.uf2", outDrive);
			}
			catch(Exception error)
			{
				Mouse.SetCursor(Cursors.Arrow);
				MessageBox.Show(error.ToString());
				return;
			}
			MessageBox.Show("D3M0N-X1 firmware is successfully installed!");
			Mouse.SetCursor(Cursors.Arrow);
		}
		private void close(object sender, RoutedEventArgs e)
		{
			Mouse.SetCursor(Cursors.Arrow);
			this.Close();
		}
    }
}
