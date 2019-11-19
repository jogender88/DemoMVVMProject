using System.ComponentModel;
namespace DemoCrudMVVM.Model
{
   public class FormModel: INotifyPropertyChanged
    {

        private string submitType="Save";
        public string SubmitType { 
            get { 
                return submitType; 
            }
            set {
                submitType = value;
                NotifyPropertyChanged("SubmitType");
            } 
        }

        private string id;
        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                NotifyPropertyChanged("Id");
            }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set {
                name = value;
                NotifyPropertyChanged("Name");
                }
        }
        private string address;
        public string Address
        {
            get { return address; }
            set { 
                address = value;
                NotifyPropertyChanged("Address");
                }
        }
        private string phone;
        public string Phone
        {
            get { return phone; }
            set {
                phone = value;
                NotifyPropertyChanged("Phone");
                }
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
