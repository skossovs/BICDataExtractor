using BIC.Foundation.Interfaces;
using BIC.WPF.ScrapManager.Data;
using BIC.WPF.ScrapManager.MVVM.Messages;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
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

        private Utils.MSMQ.OneToManySenderReceiver<StatusMessage, CommandMessage> _mq;
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

                var receivers = new Dictionary<int, string>()
                {
                    {(int) EProcessType.ETL     , Settings.GetInstance().MsmqNameStatusEtl   },
                    {(int) EProcessType.Scrapper, Settings.GetInstance().MsmqNameStatusScrap }
                };

                _mq = new Utils.MSMQ.OneToManySenderReceiver<StatusMessage, CommandMessage>
                    ( Settings.GetInstance().SleepTimeMsmqReadMsec
                    , receivers
                    , Settings.GetInstance().MsmqNameCommands);

                _mq.MessageRecievedEvent += MSMQ_Status_Receive;
                _mq.StartWatching();
            }
        }

        private void MSMQ_Status_Receive(StatusMessage body)
        {
            var processOption = (EProcessType)body.ChannelID;
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new ProcessStatusMessage(body.ProcessStatus, (EProcessType) body.ChannelID));
        }

        private void ReceiveStartCommand(ProcessStartMessage processStartMessage)
        {
            StartProcess(processStartMessage.ProcessType, processStartMessage.Arguments);
        }
        private void ReceiveStopCommand(ProcessStopMessage processStopMessage)
        {
            StopProcess(processStopMessage.ProcessType);
        }

        private void StopProcess(EProcessType processOption)
        {
            _mq.Send(new CommandMessage() { ProcessCommand = EProcessCommand.Stop, ChannelID = (int) processOption });
        }
        private void StartProcess(EProcessType processOption, string arguments)
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
            procDetails.ProcessInfo.Arguments = arguments.Replace(@"\", ""); // after parsing the json an extra slash pops up in  the string, just clean for now
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
            _processDetailsScrapper.IsRunning    = false;
            _processDetailsScrapper.LastExitCode = (ProcessResult)((Process)sender).ExitCode;

            if(_processDetailsScrapper.LastExitCode == ProcessResult.FORCIBLY_CLOSED)
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new ProcessStatusMessage(EProcessStatus.Killed, EProcessType.Scrapper));
            else
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new ProcessStatusMessage(EProcessStatus.Finished, EProcessType.Scrapper));
        }

        private void EtlProcessExited(object sender, System.EventArgs e)
        {
            _processDetailsEtl.IsRunning    = false;
            _processDetailsEtl.LastExitCode = (ProcessResult)((Process)sender).ExitCode;

            if (_processDetailsScrapper.LastExitCode == ProcessResult.FORCIBLY_CLOSED)
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new ProcessStatusMessage(EProcessStatus.Killed, EProcessType.ETL));
            else
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new ProcessStatusMessage(EProcessStatus.Finished, EProcessType.ETL));
        }


        ~MainViewModel()
        {
            _mq.Dispose();
        }
    }
}