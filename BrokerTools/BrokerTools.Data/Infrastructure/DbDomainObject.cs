using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Sqo.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Sqo;

namespace BrokerTools.Data
{
    /// <summary>
    /// Base Entity class.
    /// </summary>
    /// <remarks>
    /// Provides support for implementing <see cref="INotifyPropertyChanged"/> and 
    /// implements <see cref="INotifyDataErrorInfo"/> using <see cref="ValidationAttribute"/> instances
    /// on the validated properties.
    /// </remarks>
    /// 
    
    public abstract class DbDomainObject : INotifyPropertyChanged, INotifyDataErrorInfo
    {

        [Ignore]
        private readonly ICollection<ValidationResult> _validationResults;

        protected DbDomainObject()
        {
            _validationResults = new List<ValidationResult>();
        }


        protected void NotifyPropertyChanged(Expression<Func<object>> propertyExpression)
        {
            if (propertyExpression == null)
                throw new ArgumentNullException("propertyExpression");

            var propertyName = GetPropertyName(propertyExpression);
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            Validate(propertyName);
            OnNotifyPropertyChanged();
        }

        protected virtual void OnNotifyPropertyChanged() { }

        private string GetPropertyName(Expression<Func<object>> propertyExpression)
        {
            var unaryExpression = propertyExpression.Body as UnaryExpression;
            var memberExpression = unaryExpression == null ? (MemberExpression)propertyExpression.Body : (MemberExpression)unaryExpression.Operand;

            var propertyName = memberExpression.Member.Name;

            return propertyName;
        }


        private void NotifyErrorsChanged(string propertyName)
        {
            ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }

        protected void Validate(string propertyName)
        {
            var value = GetPropertyValue(propertyName);
            RemoveErrorsForProperty(propertyName);
            Validator.TryValidateProperty(value, new ValidationContext(this, null, null) { MemberName = propertyName }, _validationResults);
            NotifyErrorsChanged(propertyName);
        }

        public void Validate()
        {
            _validationResults.Clear();
            Validator.TryValidateObject(this, new ValidationContext(this, null, null), _validationResults, true);

            foreach (var result in _validationResults)
                NotifyErrorsChanged(result.MemberNames.First());

        }

        private object GetPropertyValue(string propertyName)
        {
            var type = this.GetType();

            var propertyInfo = type.GetProperty(propertyName);

            if (propertyInfo == null)
            {
                throw new ArgumentException(string.Format("Couldn't find any property called {0} on type {1}", propertyName, type));
            }

            return propertyInfo.GetValue(this, null);
        }

        private void RemoveErrorsForProperty(string propertyName)
        {
            var validationResults = _validationResults.Where(result => result.MemberNames.Contains(propertyName)).ToList();

            foreach (var result in validationResults)
                _validationResults.Remove(result);
        }

        public IEnumerable GetErrors(string propertyName)
        {
            return _validationResults.Where(result => result.MemberNames.Contains(propertyName));
        }

        [Ignore]
        public bool HasErrors
        {
            get { return _validationResults.Any(); }
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public event PropertyChangedEventHandler PropertyChanged;
     
    }
}
