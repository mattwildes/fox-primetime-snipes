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
            /*
            const string name = "PATH";
            string pathvar = System.Environment.GetEnvironmentVariable(name);
            var value = pathvar + @";C:\Program Files\Adobe\Adobe After Effects CC 2019\Support Files\\";
            var target = EnvironmentVariableTarget.Machine;
            System.Environment.SetEnvironmentVariable(name, value, target);
            value = pathvar + @";C:\Program Files (x86)\VideoLAN\VLC\\";
            target = EnvironmentVariableTarget.Machine;
            System.Environment.SetEnvironmentVariable(name, value, target);
            */
        }

        private string renderedPath;
        public void buttonRender_Click(object sender, RoutedEventArgs e)
        {
            
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
            
            //OpenSecondWindow();
        }

        public async void OpenSecondWindow(string renderedPath)
        {
            StatusWindow secondWindow = new StatusWindow();
            this.Visibility = Visibility.Hidden;
            secondWindow.Show();
            secondWindow.Title = "Generating Snipe ...";
            secondWindow.StatusLabel.Content = "Processing Text ...";
            secondWindow.renderBar.Value = 25;
            await Task.Delay(2000);

            CMD_EditText();
            if (TxtHeader.Text.Length < 25)
            {
                secondWindow.StatusLabel.Content = ("Exporting to " + renderedPath);
                secondWindow.renderBar.Value = 50;
                await Task.Delay(2000);
                CMD_AERender1LineHeader(renderedPath);
            }
            else
            {
                secondWindow.StatusLabel.Content = ("Exporting to " + renderedPath);
                secondWindow.renderBar.Value = 50;
                await Task.Delay(2000);
                CMD_AERender2LineHeader(renderedPath);
            }
            secondWindow.renderBar.Value = 100;
            Process.Start(renderedPath);
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
            cmd.StandardInput.WriteLine("cd C:\\Users\\Public\\Documents\\FTS Graphics Hub\\Fox Primetime Snipes\\Template");
            cmd.StandardInput.WriteLine("echo Header=\"" + TxtHeader.Text + "\" > Header.txt");
            cmd.StandardInput.WriteLine("echo BodyCopy=\"" + TxtBody.Text + "\" > BodyCopy.txt");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
            cmd.Close();
        }

        public void CMD_AERender1LineHeader(string renderedPath)
        {
            string templatePath = @"C:\Users\Public\Documents\FTS Graphics Hub\Fox Primetime Snipes\Template\";
            string templateFileName = "CurrentTemplate.aep";
            string templateComp1LineHeader = "PrimeTime_Snipe_1Line_Header";
            //string renderedPath = @"C:\Program Files\FTS Graphics Hub\Fox Primetime Snipes\Output\";
            string renderedFileName = "\\" + templateComp1LineHeader + "_" + DateTime.Now.ToString("yyyy-M-dd_HH-mm");




            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();
            cmd.StandardInput.WriteLine("cd C:\\Program Files\\Adobe\\Adobe After Effects CC 2019\\Support Files");
            cmd.StandardInput.WriteLine("aerender.exe -project \"" + templatePath + templateFileName + "\" -comp " + templateComp1LineHeader + " -output \"" + renderedPath + renderedFileName + ".mov\" -v ERRORS_AND_PROGRESS -sound ON -reuse -mem_usage 20 20");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
            cmd.WaitForExit();
            if (cmd.ExitCode == 1)
            {
                System.Windows.MessageBox.Show("Error Exporting Snipe");
            }

            else
            {
                cmd.Close();


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
                cmd_VLC.Close();
            }
        }

        public void CMD_AERender2LineHeader(string renderedPath)
        {
            string templatePath = @"C:\Users\Public\Documents\FTS Graphics Hub\Fox Primetime Snipes\Template\";
            string templateFileName = "CurrentTemplate.aep";
            string templateComp2LineHeader = "PrimeTime_Snipe_2Line_Header";
            //string renderedPath = @"C:\Program Files\FTS Graphics Hub\Fox Primetime Snipes\Output\";
            string renderedFileName = "\\" + templateComp2LineHeader + "_" + DateTime.Now.ToString("yyyy-M-dd_HH-mm");


            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();
            cmd.StandardInput.WriteLine("cd C:\\Program Files\\Adobe\\Adobe After Effects CC 2019\\Support Files");
            cmd.StandardInput.WriteLine("aerender.exe -project \"" + templatePath + templateFileName + "\" -comp " + templateComp2LineHeader + " -output \"" + renderedPath + renderedFileName + ".mov\" -v ERRORS_AND_PROGRESS -sound ON -reuse -mem_usage 20 20");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
            cmd.WaitForExit();
            if (cmd.ExitCode == 1)
            {
                System.Windows.MessageBox.Show("Error Exporting Snipe");
            }

            else
            {

                cmd.Close();

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
                cmd_VLC.Close();
            }


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
                OpenSecondWindow(renderedPath);
            }
        }

        private void TxtBody_OnKeyDownHandler(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                OpenSecondWindow(renderedPath);
            }
        }



        private void PreviewButton_Click(object sender, RoutedEventArgs e)
        {
            OpenSecondWindow_Preview();
        }

        private async void OpenSecondWindow_Preview()
        {
            StatusWindow secondWindow = new StatusWindow();
            this.Visibility = Visibility.Hidden;
            secondWindow.Show();
            secondWindow.Title = "Generating Preview ...";
            secondWindow.StatusLabel.Content = "Processing Text ...";
            secondWindow.renderBar.Value = 25;
            await Task.Delay(2000);

            CMD_EditText();
            if (TxtHeader.Text.Length < 25)
            {
                secondWindow.StatusLabel.Content = "Creating Preview ...";
                secondWindow.renderBar.Value = 50;
                await Task.Delay(2000);
                CMD_AERender1LineHeader_Preview();
            }
            else
            {
                secondWindow.StatusLabel.Content = "Creating Preview ...";
                secondWindow.renderBar.Value = 50;
                await Task.Delay(2000);
                CMD_AERender2LineHeader_Preview();
            }
            secondWindow.renderBar.Value = 100;
            secondWindow.Visibility = Visibility.Hidden;
            this.Visibility = Visibility.Visible;

            File.Delete(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FTS Graphics Hub", "preview.mov"));
        }

        private void CMD_AERender1LineHeader_Preview()
        {
            string templatePath = @"C:\Users\Public\Documents\FTS Graphics Hub\Fox Primetime Snipes\Template\";
            string templateFileName = "CurrentTemplate.aep";
            string templateComp1LineHeader = "PrimeTime_Snipe_1Line_Header";
            //string renderedPath = @"C:\Program Files\FTS Graphics Hub\Fox Primetime Snipes\Output\";
            var renderedFileName = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FTS Graphics Hub", "preview.mov");




            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();
            cmd.StandardInput.WriteLine("cd C:\\Program Files\\Adobe\\Adobe After Effects CC 2019\\Support Files");
            cmd.StandardInput.WriteLine("aerender.exe -project \"" + templatePath + templateFileName + "\" -comp " + templateComp1LineHeader + " -s 360 -e 360 -output \"" + renderedFileName + "\" -v ERRORS_AND_PROGRESS -sound ON -reuse -mem_usage 20 20");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
            cmd.WaitForExit();
            if (cmd.ExitCode == 1)
            {
                System.Windows.MessageBox.Show("Error Exporting Snipe");
            }

            else
            {
                cmd.Close();


                Process cmd_VLC = new Process();
                cmd_VLC.StartInfo.FileName = "cmd.exe";
                cmd_VLC.StartInfo.RedirectStandardInput = true;
                cmd_VLC.StartInfo.RedirectStandardOutput = true;
                cmd_VLC.StartInfo.CreateNoWindow = true;
                cmd_VLC.StartInfo.UseShellExecute = false;
                cmd_VLC.Start();
                cmd_VLC.StandardInput.WriteLine("cd C:\\Program Files (x86)\\VideoLAN\\VLC");
                cmd_VLC.StandardInput.WriteLine("vlc.exe \"" + renderedPath + renderedFileName + "\" --no-osd --play-and-pause");
                cmd_VLC.StandardInput.Flush();
                cmd_VLC.StandardInput.Close();
                Console.WriteLine(cmd_VLC.StandardOutput.ReadToEnd());
                //cmd_VLC.WaitForExit();
                cmd_VLC.Close();
            }
        }

        private void CMD_AERender2LineHeader_Preview()
        {
            string templatePath = @"C:\Users\Public\Documents\FTS Graphics Hub\Fox Primetime Snipes\Template\";
            string templateFileName = "CurrentTemplate.aep";
            string templateComp2LineHeader = "PrimeTime_Snipe_2Line_Header";
            //string renderedPath = @"C:\Program Files\FTS Graphics Hub\Fox Primetime Snipes\Output\";
            var renderedFileName = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FTS Graphics Hub", "preview.mov");


            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();
            cmd.StandardInput.WriteLine("cd C:\\Program Files\\Adobe\\Adobe After Effects CC 2019\\Support Files");
            cmd.StandardInput.WriteLine("aerender.exe -project \"" + templatePath + templateFileName + "\" -comp " + templateComp2LineHeader + " -s 360 -e 360 -output \"" + renderedFileName + "\" -v ERRORS_AND_PROGRESS -sound ON -reuse -mem_usage 20 20");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
            cmd.WaitForExit();
            if (cmd.ExitCode == 1)
            {
                System.Windows.MessageBox.Show("Error Exporting Snipe");
            }

            else
            {

                cmd.Close();

                Process cmd_VLC = new Process();
                cmd_VLC.StartInfo.FileName = "cmd.exe";
                cmd_VLC.StartInfo.RedirectStandardInput = true;
                cmd_VLC.StartInfo.RedirectStandardOutput = true;
                cmd_VLC.StartInfo.CreateNoWindow = true;
                cmd_VLC.StartInfo.UseShellExecute = false;
                cmd_VLC.Start();
                cmd_VLC.StandardInput.WriteLine("cd C:\\Program Files (x86)\\VideoLAN\\VLC");
                cmd_VLC.StandardInput.WriteLine("vlc.exe \"" + renderedPath + renderedFileName + "\" --no-osd --play-and-pause");
                cmd_VLC.StandardInput.Flush();
                cmd_VLC.StandardInput.Close();
                Console.WriteLine(cmd_VLC.StandardOutput.ReadToEnd());
                //cmd_VLC.WaitForExit();
                cmd_VLC.Close();
            }


        }

    }


}
