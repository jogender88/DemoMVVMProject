using DemoCrudMVVM.Model;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;

namespace DemoCrudMVVM.ViewModel
{

    public class FormViewModel:INotifyPropertyChanged
    {
        private ObservableCollection<UserDetails> _AllUsers;
        public ObservableCollection<UserDetails> AllUsers { 
            get {
                return _AllUsers;
            }
            set {
                _AllUsers = value;
                NotifyPropertyChanged("AllUsers");
            }
        }

        HelperClass hc = new HelperClass();
        public FormViewModel()
        {
            _AllUsers = new ObservableCollection<UserDetails>(hc.GetAllUsers());
            FormModel = new FormModel();
        }


    private FormModel _formDetailsModel;
        public FormModel FormModel
        {
            get { return _formDetailsModel; }
            set { _formDetailsModel = value;
                NotifyPropertyChanged("FormModel");
            }
        }

        private ICommand _SubmitCommand;
        public ICommand SubmitCommand
        {
            get
            {
                if (_SubmitCommand == null)
                {
                    _SubmitCommand = new RelayCommand(SubmitExecute, null);
                }
                return _SubmitCommand; 
            }
        }


        private void SubmitExecute(object parameter)
        {
            if (validate() == true)
            {

                if (_formDetailsModel.SubmitType == "Update")
                {
                    List<UserDetails> user = new List<UserDetails>(hc.Save(_formDetailsModel));
                    int count = 0;
                    foreach (UserDetails details in _AllUsers)
                    {
                        if (details.Id == user[0].Id)
                        {
                            _AllUsers.RemoveAt(count);
                            details.Id = user[0].Id;
                            details.Name = user[0].Name;
                            details.Address = user[0].Address;
                            details.Phone = user[0].Phone;
                            _AllUsers.Insert(count,details);
                            break;
                        }
                        count++;
                    }
                  
                }
                else
                {
                    _formDetailsModel.Id=null;
                    List<UserDetails> user = new List<UserDetails>(hc.Save(_formDetailsModel));
                    UserDetails userDetails = new UserDetails();
                    userDetails.Id = user[0].Id;
                    userDetails.Name = user[0].Name;
                    userDetails.Address = user[0].Address;
                    userDetails.Phone = user[0].Phone;
                    _AllUsers.Add(userDetails);
                }
                Clear();
            }
            else
            {
                MessageBox.Show("Please Enter details");

            }
        }

        private ICommand _EditCommand;
        public ICommand EditCommand
        {
            get
            {
                if (_EditCommand == null)
                {
                    _EditCommand = new RelayCommand(EditExecute, null);
                }
                return _EditCommand;
            }
        }

        private void EditExecute(object User)
        {
            UserDetails formModel = (UserDetails)User;
            FormModel.Id = formModel.Id.Trim();
             FormModel.Name= formModel.Name.Trim();
            FormModel.Address = formModel.Address.Trim();
            FormModel.Phone = formModel.Phone.Trim();
            FormModel.SubmitType = "Update";
        }

        private ICommand _DeleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (_DeleteCommand == null)
                {
                    _DeleteCommand = new RelayCommand(DeleteExecute, null);
                }
                return _DeleteCommand;
            }
        }
        private void DeleteExecute(object User)
        {
            UserDetails formModel = (UserDetails)User;
            if (hc.Delete(formModel.Id))
            {
                int index=_AllUsers.IndexOf(formModel);
                _AllUsers.RemoveAt(index);
            }
        }
        private bool validate()
        {
            if(string.IsNullOrEmpty(FormModel.Name) || string.IsNullOrEmpty(FormModel.Address)|| string.IsNullOrEmpty(FormModel.Phone))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private ICommand _CancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                if (_CancelCommand == null)
                {
                    _CancelCommand = new RelayCommand(CancelExecute, null);
                }
                return _CancelCommand;
            }
        }
        private void CancelExecute(object parameter)
        {
            Clear();
        }
        private void Clear()
        {
            _formDetailsModel.Name = string.Empty;
            _formDetailsModel.Address = string.Empty;
            _formDetailsModel.Phone = string.Empty;
            _formDetailsModel.SubmitType = "Save";
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
