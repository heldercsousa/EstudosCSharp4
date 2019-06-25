using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace EstudosCSharp.Delegates
{
    /// <summary>
    /// Descreve o uso das interfaces INotifyPropertyChanging e INotifyPropertyChanged para programação de UI
    /// </summary>
    public class Person : INotifyPropertyChanged, INotifyPropertyChanging
    {
        private string _name;
        private int _age;

        public Person(string name, int age)
        {
            _name = name;
            _age = age;
        }

        public event PropertyChangedEventHandler PropertyChanged;       
        public event PropertyChangingEventHandler PropertyChanging; ////*implementing  implicitly interface INotifyPropertyChanging
       
        /*implementing  explicitly interface INotifyPropertyChanging
         
        event PropertyChangingEventHandler INotifyPropertyChanging.PropertyChanging
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }
        */

        public int Age
        {
            get { return _age; }
            set {
                if (value != _age)
                {
                    OnPropertyChanging("Age");
                    _age = value;
                    OnPropertyChanged("Age");
                }
            }
        }

        public string Name
        {
            get { return _name; }
            set { 
                if (value!=_name)
                {
                    OnPropertyChanging("Name");
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public virtual void OnPropertyChanging(string propertyName)
        {
            var propertyChanging = PropertyChanging;
            if (propertyChanging != null)
                propertyChanging(this, new PropertyChangingEventArgs(propertyName));
        }

        public virtual void OnPropertyChanged(string propertyName)
        {
            var propertyChanged = PropertyChanged;
            if (propertyChanged != null)
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
            
    }
}
