using System;
using System.IO;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;

namespace CheckSumSHA256Inspector.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public MainWindowViewModel()
        {
            CloseCommand = new RelayCommand( () => Application.Current.Shutdown( 0 ) );
            FindFileCommand = new RelayCommand(() => FindFile() );
            CopyCheckSumCommand = new RelayCommand( () => CopyCheckSumToClipboard() );
        }

        public ICommand CloseCommand { get; set; }
        public ICommand FindFileCommand { get; set; }
        public ICommand CopyCheckSumCommand { get; set; }


        public string CheckSumValue { get; set; } = string.Empty;
        public string CheckSumFile { get; set; } = string.Empty;

        public Style CheckSumBorderStyle =>
            CheckSumValue?.Length > 0
                ? (Style) Application.Current.Resources["GreenBorder"]
                : (Style) Application.Current.Resources["RedBorder"];

        public Brush CopyButtonColor => 
            Clipboard.GetText() != CheckSumValue
                ? (Brush)Application.Current.Resources["LightPinkBrush"]
                : (Brush)Application.Current.Resources["LightGreenBrush"];

        private void FindFile()
        {
            var dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == true)
            {
                CheckSumFile = dlg.FileName;
                CheckSumValue = GetCheckSumHash(dlg.FileName);
            }
        }
        
        private string GetCheckSumHash(string fileName)
        {
            using (FileStream stream = File.OpenRead(fileName))
            {
                var sha = new SHA256Cng();
                byte[] checksum = sha.ComputeHash(stream);
                return BitConverter.ToString(checksum).Replace("-", String.Empty);
            }
        }

        private void CopyCheckSumToClipboard()
        {
            if (CheckSumValue.Length > 0)
            {
                Clipboard.Clear();
                Clipboard.SetText( CheckSumValue );
                //force CheckSumValue to reevaluate for button color
                var checkSum = CheckSumValue;
                CheckSumValue = string.Empty;
                CheckSumValue = checkSum;
            }
        }




    }


}