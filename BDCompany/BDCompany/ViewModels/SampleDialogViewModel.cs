﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TinyLittleMvvm;

namespace BDCompany.ViewModels.Additions
{
    public class SampleDialogViewModel : DialogViewModel<string>
    {
        private string _text;

        public SampleDialogViewModel()
        {
            OkCommand = new RelayCommand(OnOk, CanOk);
            CancelCommand = new RelayCommand(OnCancel);
            _text = String.Empty;

            AddValidationRule(() => Text, text => !String.IsNullOrEmpty(text), "Text must not be empty");
        }

        public string Text
        {
            get { return _text; }
            set
            {
                if (_text != value)
                {
                    _text = value;
                    ValidateAllRules();
                    NotifyOfPropertyChange(() => Text);
                }
            }
        }

        public ICommand OkCommand { get; }

        public ICommand CancelCommand { get; }

        private bool CanOk()
        {
            return !HasErrors;
        }

        private void OnOk()
        {
            Close(Text);
        }

        private void OnCancel()
        {
            Close(null);
        }
    }
}