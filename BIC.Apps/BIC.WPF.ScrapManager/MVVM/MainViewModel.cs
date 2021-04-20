using BIC.Foundation.Interfaces;
using BIC.WPF.ScrapManager.Data;
using BIC.WPF.ScrapManager.MVVM.Messages;
using GalaSoft.MvvmLight;
using System;
using System.Diagnostics;

namespace BIC.WPF.ScrapManager.MVVM
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

        private Utils.MSMQ.SenderReciever<StatusMessage, CommandMessage> _mq;
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
                {
                    IsRunning       = false,
                    ProcessInfo     = new ProcessStartInfo(Settings.GetInstance().ScrapperFilePath)
                };

                _processDetailsEtl = new ProcessDetails()
                {
                    IsRunning       = false,
                    ProcessInfo     = new ProcessStartInfo(Settings.GetInstance().EtlProcessFilePath)
                };


                GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<ProcessStartMessage>(this, ReceiveStartCommand);
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<ProcessStopMessage> (this, ReceiveStopCommand);

                _mq = new Utils.MSMQ.SenderReciever<StatusMessage, CommandMessage>
                    (Settings.GetInstance().MsmqNameCommands
                    , Settings.GetInstance().MsmqNameStatus
                    , Settings.GetInstance().SleepTimeMsmqReadMsec);

                _mq.MessageRecievedEvent += MSMQ_Status_Receive;
                _mq.StartWatching();
            }
        }

        private void MSMQ_Status_Receive(StatusMessage body)
        {
            // TODO: signal status change to UI elements
        }

        private void ReceiveStartCommand(ProcessStartMessage processStartMessage)
        {
            StartProcess(processStartMessage.ProcessType);
        }
        private void ReceiveStopCommand(ProcessStopMessage processStopMessage)
        {
            StopProcess(processStopMessage.ProcessType);
        }

        private void StopProcess(EProcessType processOption)
        {
            _mq.Send(new CommandMessage() { ProcessCommand = EProcessCommand.Stop });
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

            procDetails.ProcessInfo.UseShellExecute = false;
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
            //TODO: signal to other elements 
            _processDetailsScrapper.IsRunning    = false;
            _processDetailsScrapper.LastExitCode = (ProcessResult)((Process)sender).ExitCode;
        }

        private void EtlProcessExited(object sender, System.EventArgs e)
        {
            //TODO: signal to other elements 
            _processDetailsEtl.IsRunning    = false;
            _processDetailsEtl.LastExitCode = (ProcessResult)((Process)sender).ExitCode;
        }


        ~MainViewModel()
        {
            _mq.Dispose();
        }
    }
}