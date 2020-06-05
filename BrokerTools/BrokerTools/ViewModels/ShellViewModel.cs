using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using BrokerTools.Base;
using BrokerTools.Views;
using BrokerTools.Data;
using BrokerTools.Data.FundProxy;
using BrokerTools.Data.Controller;
using FundSelectorTool = BrokerTools.FundSelector.Controllers;

namespace BrokerTools.ViewModels
{
    public enum ShellState
    {
        Default,
        ToolSelector,
        ToolInProgress
    }

    public class ShellViewModel : BaseViewModel
    {
        #region :: Fields ::

        BuildView CacheView = new BuildView();

        /// <summary>
        /// Field backing the "CurrentToolStepCount" property.
        /// </summary>
        double _CurrentToolStepCount = 1.0;

        /// <summary>
        /// Field backing the "CurrentToolStepLabels" property.
        /// </summary>
        string[] _CurrentToolStepLabels;

        /// <summary>
        /// Field backing the "CurrentToolCurrentStep" property.
        /// </summary>
        double _CurrentToolCurrentStep = 1.0;

        /// <summary>
        /// Field backing the "State" property.
        /// </summary>
        ShellState _State = ShellState.Default;

        /// <summary>
        /// Field backing the "CurrentTool" property.
        /// </summary>
        UserControl _CurrentTool;

        /// <summary>
        /// Field backing the "AcceptDisclaimerCommand" property. 
        /// </summary>
        DelegateCommand _AcceptDisclaimerCommand;

        /// <summary>
        /// Field backing the "DeclineDisclaimerCommand" property. 
        /// </summary>
        DelegateCommand _DeclineDisclaimerCommand;

        /// <summary>
        /// Field backing the "LoadFundSelectorToolCommand" property. 
        /// </summary>
        DelegateCommand _LoadFundSelectorToolCommand;

        #endregion

        #region :: Properties ::

        /// <summary>
        /// Gets or sets the current step of the current tool.
        /// </summary>
        public double CurrentToolCurrentStep
        {
            get
            {
                return _CurrentToolCurrentStep;
            }
            set
            {
                _CurrentToolCurrentStep = value;
                base.NotifyPropertyChanged("CurrentToolCurrentStep");
            }
        }

        /// <summary>
        /// Gets or sets the labels for each of the current tool's steps.
        /// </summary>
        public string[] CurrentToolStepLabels 
        {
            get
            {
                return _CurrentToolStepLabels;
            }
            set
            {
                _CurrentToolStepLabels = value;
                base.NotifyPropertyChanged("CurrentToolStepLabels");
            }
        }

        /// <summary>
        /// Gets or sets the number of steps that the current tool have.
        /// </summary>
        public double CurrentToolStepCount
        {
            get
            {
                return _CurrentToolStepCount;
            }
            set
            {
                _CurrentToolStepCount = value;
                base.NotifyPropertyChanged("CurrentToolStepCount");
            }
        }

        /// <summary>
        /// Gets or sets the current state of the shell.
        /// </summary>
        public ShellState State
        {
            get
            {
                return _State;
            }
            set
            {
                _State = value;
                base.NotifyPropertyChanged("State");
            }
        }

        /// <summary>
        /// A container for loading the relevant content within the shell's content area.
        /// </summary>
        public UserControl CurrentTool
        {
            get
            {
                return _CurrentTool;
            }
            set
            {
                _CurrentTool = value;
                base.NotifyPropertyChanged("CurrentTool");
            }
        }

        /// <summary>
        /// Gets the "Accept Disclaimer" Command.
        /// </summary>
        public DelegateCommand AcceptDisclaimerCommand
        {
            get
            {
                if (_AcceptDisclaimerCommand == null)
                    _AcceptDisclaimerCommand = new DelegateCommand(OnAcceptDisclaimer);
                return _AcceptDisclaimerCommand;
            }
        }

        /// <summary>
        /// Gets the "Decline Disclaimer" Command.
        /// </summary>
        public DelegateCommand DeclineDisclaimerCommand
        {
            get
            {
                if (_DeclineDisclaimerCommand == null)
                    _DeclineDisclaimerCommand = new DelegateCommand(OnDeclineDisclaimer);
                return _DeclineDisclaimerCommand;
            }
        }

        public DelegateCommand LoadFundSelectorToolCommand
        {
            get
            {
                if (_LoadFundSelectorToolCommand == null)
                    _LoadFundSelectorToolCommand = new DelegateCommand(OnLoadFundSelectorTool);
                return _LoadFundSelectorToolCommand;
            }
        }

        #endregion

        #region :: Delegates ::

        /// <summary>
        /// Handles the "Accept Disclaimer" Command.
        /// </summary>
        void OnAcceptDisclaimer(object parameter)
        {
            State = ShellState.ToolSelector;

            //FundRepository db = new FundRepository();
            //db.GetFundsByProductCode(ProductCode.LLA,
            //(result) =>
            //{
            //    OLife o = result.Result;
            //});

        }

        /// <summary>
        /// Handles the "Decline Disclaimer" Command.
        /// </summary>
        void OnDeclineDisclaimer(object parameter)
        {
            App.Current.MainWindow.Close();
        }

        /// <summary>
        /// Handles the "Load Fund Selector Tool" Command.
        /// </summary>
        void OnLoadFundSelectorTool(object parameter)
        {
            State = ShellState.ToolInProgress;
            CurrentTool = FundSelectorTool.ViewNavigationService.Instance.MainView;
            CurrentToolStepCount = 6.0;
            CurrentToolCurrentStep = 1.0;
            CurrentToolStepLabels = new string[6] { "1", "2", "A", "B", "C", "3" };
        }

        #endregion

        #region :: Constructor ::

        /// <summary>
        /// Class constructor.
        /// </summary>
        public ShellViewModel()
        {
            //CurrentStep = 1;
            //return;

            //if (BuildDBController.HasInitialized())
            //    CurrentStep = 1;
            //else
            //{
            //    CurrentStep = 0;
            //    CurrentContent = CacheView;
            //}
        }

        #endregion
    }
}
