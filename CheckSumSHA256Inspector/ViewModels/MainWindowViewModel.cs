using System;
using System.IO;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Input;
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


        public string CheckSumValue { get; set; }
        public string CheckSumFile { get; set; }


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
            }
        }




    }
}