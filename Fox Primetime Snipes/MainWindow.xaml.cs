using System;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Net;
using System.Diagnostics;
using System.Management;

namespace Fox_Primetime_Snipes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 


    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            TxtHeader.Focus();

            const string name = "PATH";
            string pathvar = System.Environment.GetEnvironmentVariable(name);
            var value = pathvar + @";C:\Program Files\Adobe\Adobe After Effects CC 2019\Support Files\\";
            var target = EnvironmentVariableTarget.Machine;
            System.Environment.SetEnvironmentVariable(name, value, target);



        }

        //private string renderedPath;

        public void buttonRender_Click(object sender, RoutedEventArgs e)
        {
            /*
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.Desktop;
            folderBrowserDialog.Description = "Select the Render location:";
            DialogResult dialogResult = folderBrowserDialog.ShowDialog();

            if (dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                renderedPath = folderBrowserDialog.SelectedPath;
                OpenSecondWindow(renderedPath);
                
            }
            else if (dialogResult == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            */
            OpenSecondWindow();
        }

        public async void OpenSecondWindow()
        {
            StatusWindow secondWindow = new StatusWindow();
            this.Visibility = Visibility.Hidden;
            secondWindow.Show();
            secondWindow.renderBar.Value = 25;
            await Task.Delay(2000);
            CMD_EditText();
            await Task.Delay(2000);
            secondWindow.renderBar.Value = 50;
            if (TxtHeader.Text.Length < 25)
            {
                CMD_AERender1LineHeader();
            }
            else
            {
                CMD_AERender2LineHeader();
            }
            secondWindow.renderBar.Value = 100;
            //secondWindow.OpenFile.IsEnabled = true;
            await Task.Delay(2000);
            secondWindow.Visibility = Visibility.Hidden;
            this.Visibility = Visibility.Visible;
        }

        private void CMD_EditText()
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();
            cmd.StandardInput.WriteLine("cd C:\\Program Files\\FTS Graphics Hub\\Fox Primetime Snipes\\Template");
            cmd.StandardInput.WriteLine("echo Header=\"" + TxtHeader.Text + "\" > Header.txt");
            cmd.StandardInput.WriteLine("echo BodyCopy=\"" + TxtBody.Text + "\" > BodyCopy.txt");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
            cmd.WaitForExit();
        }

        public void CMD_AERender1LineHeader()
        {
            string templatePath = @"C:\Program Files\FTS Graphics Hub\Fox Primetime Snipes\Template\";
            string templateFileName = "CurrentTemplate.aep";
            string templateComp1LineHeader = "PrimeTime_Snipe_1Line_Header";
            string renderedPath = @"C:\Program Files\FTS Graphics Hub\Fox Primetime Snipes\Output\";
            string renderedFileName = "\\" + templateComp1LineHeader + "_" + DateTime.Now.ToString("yyyy-M-dd--HH-mm-ss");


            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();
            cmd.StandardInput.WriteLine("cd C:\\Program Files\\Adobe\\Adobe After Effects CC 2019\\Support Files");
            cmd.StandardInput.WriteLine("aerender.exe -project \"" + templatePath + templateFileName + "\" -comp " + templateComp1LineHeader + " -output \"" + renderedPath + renderedFileName + ".mov\" -v ERRORS_AND_PROGRESS -sound ON");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
            cmd.WaitForExit();

            Process cmd_VLC = new Process();
            cmd_VLC.StartInfo.FileName = "cmd.exe";
            cmd_VLC.StartInfo.RedirectStandardInput = true;
            cmd_VLC.StartInfo.RedirectStandardOutput = true;
            cmd_VLC.StartInfo.CreateNoWindow = true;
            cmd_VLC.StartInfo.UseShellExecute = false;
            cmd_VLC.Start();
            cmd_VLC.StandardInput.WriteLine("cd C:\\Program Files (x86)\\VideoLAN\\VLC");
            cmd_VLC.StandardInput.WriteLine("vlc.exe \"" + renderedPath + renderedFileName + ".mov\" --no-osd --play-and-pause");
            cmd_VLC.StandardInput.Flush();
            cmd_VLC.StandardInput.Close();
            Console.WriteLine(cmd_VLC.StandardOutput.ReadToEnd());
            //cmd_VLC.WaitForExit();

        }

        public void CMD_AERender2LineHeader()
        {
            string templatePath = @"C:\Program Files\FTS Graphics Hub\Fox Primetime Snipes\Template\";
            string templateFileName = "CurrentTemplate.aep";
            string templateComp2LineHeader = "PrimeTime_Snipe_2Line_Header";
            string renderedPath = @"C:\Program Files\FTS Graphics Hub\Fox Primetime Snipes\Output\";
            string renderedFileName = "\\" + templateComp2LineHeader + "_" + DateTime.Now.ToString("yyyy-M-dd--HH-mm-ss");


            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();
            cmd.StandardInput.WriteLine("cd C:\\Program Files\\Adobe\\Adobe After Effects CC 2019\\Support Files");
            cmd.StandardInput.WriteLine("aerender.exe -project \"" + templatePath + templateFileName + "\" -comp " + templateComp2LineHeader + " -output \"" + renderedPath + renderedFileName + ".mov\" -v ERRORS_AND_PROGRESS -sound ON");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
            cmd.WaitForExit();

            Process cmd_VLC = new Process();
            cmd_VLC.StartInfo.FileName = "cmd.exe";
            cmd_VLC.StartInfo.RedirectStandardInput = true;
            cmd_VLC.StartInfo.RedirectStandardOutput = true;
            cmd_VLC.StartInfo.CreateNoWindow = true;
            cmd_VLC.StartInfo.UseShellExecute = false;
            cmd_VLC.Start();
            cmd_VLC.StandardInput.WriteLine("cd C:\\Program Files (x86)\\VideoLAN\\VLC");
            cmd_VLC.StandardInput.WriteLine("vlc.exe \"" + renderedPath + renderedFileName + ".mov\" --no-osd --play-and-pause");
            cmd_VLC.StandardInput.Flush();
            cmd_VLC.StandardInput.Close();
            Console.WriteLine(cmd_VLC.StandardOutput.ReadToEnd());
            //cmd_VLC.WaitForExit();



        }


        private void TxtHeader_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();

        }

        private void TxtHeader_OnKeyDownHandler(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                OpenSecondWindow();
            }
        }

        private void TxtBody_OnKeyDownHandler(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                OpenSecondWindow();
            }
        }
    }


}
