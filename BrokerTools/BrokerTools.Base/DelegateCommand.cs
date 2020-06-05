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

namespace BrokerTools.Base
{
    public class DelegateCommand : ICommand
    {
        #region :: Fields ::

        private Predicate<object> _CanExecute;
        private Action<object> _Method;

        #endregion

        #region :: Events ::

        public event EventHandler CanExecuteChanged;

        #endregion

        #region :: Constructor ::


        public DelegateCommand(Action<object> method)
            : this(method, null)
        {
        }

        public DelegateCommand(Action<object> method, Predicate<object> canExecute)
        {
            _Method = method;
            _CanExecute = canExecute;
        }

        #endregion

        #region :: Public Methods ::

        public bool CanExecute(object parameter)
        {
            if (_CanExecute == null)
            {
                return true;
            }

            return _CanExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _Method.Invoke(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged(EventArgs.Empty);
        }

        #endregion

        #region :: Protected Methods ::

        protected virtual void OnCanExecuteChanged(EventArgs e)
        {
            var canExecuteChanged = CanExecuteChanged;

            if (canExecuteChanged != null)
                canExecuteChanged(this, e);
        }

        #endregion
    }
}
