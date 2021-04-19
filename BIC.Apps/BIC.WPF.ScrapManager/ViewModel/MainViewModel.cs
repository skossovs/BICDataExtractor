using BIC.WPF.ScrapManager.Data;
using BIC.WPF.ScrapManager.MVVM.Messages;
using GalaSoft.MvvmLight;
using System;
using System.Diagnostics;

namespace BIC.WPF.ScrapManager.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private ProcessDetails _processDetailsScrapper;
        private ProcessDetails _processDetailsEtl;
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {
                _processDetailsScrapper = new ProcessDetails()
                {// TODO:
                    IsRunning       = false,
                    ProcessInfo     = new ProcessStartInfo(@"C:\Users\Stan\Documents\GitHub\BICDataExtractor\BIC.Apps\BIC.Apps.MSMQExtractorCommander\bin\Debug\BIC.Apps.MSMQExtractorCommander.exe")
                };

                _processDetailsEtl = new ProcessDetails()
                { // TODO:
                    IsRunning       = false,
                    ProcessInfo     = new ProcessStartInfo(@"C:\Users\Stan\Documents\GitHub\BICDataExtractor\BIC.Apps\BIC.Apps.MSMQEtlProcess\bin\Debug\BIC.Apps.MSMQEtlProcess.exe")
                };


                GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<ProcessStartMessage>(this, ReceiveStartCommand);
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<ProcessStopMessage> (this, ReceiveStopCommand);
            }
        }

        private void ReceiveStartCommand(ProcessStartMessage processStartMessage)
        {

        }
        private void ReceiveStopCommand(ProcessStopMessage processStopMessage)
        {

        }


        private void StartProcess(EProcessType processOption)
        {
            ProcessDetails procDetails = null;
            switch(processOption)
            {
                case EProcessType.ETL:
                    procDetails = _processDetailsEtl;
                    break;
                case EProcessType.Scrapper:
                    procDetails = _processDetailsScrapper;
                    break;
            }

            procDetails.ProcessObject = Process.Start(procDetails.ProcessInfo);
            procDetails.ProcessObject.EnableRaisingEvents = true;

            switch(processOption)
            {
                case EProcessType.ETL:
                    procDetails.ProcessObject.Exited += new EventHandler(EtlProcessExited);
                    break;
                case EProcessType.Scrapper:
                    procDetails.ProcessObject.Exited += new EventHandler(ScrapperProcessExited);
                    break;
            }

            procDetails.IsRunning = true;
        }

        private void ScrapperProcessExited(object sender, System.EventArgs e)
        {
            //TODO:
            _processDetailsScrapper.IsRunning = false;
        }

        private void EtlProcessExited(object sender, System.EventArgs e)
        {
            //TODO:
            _processDetailsEtl.IsRunning = false;
        }

    }
}