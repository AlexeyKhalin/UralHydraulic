using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ural_Hydraulic
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LinkPage : ContentPage
    {
        public ICommand OfficeNumberClicked => new Command<string>((url) =>
        {
            try
            {
                PhoneDialer.Open("+73432862756");
            }catch (Exception ex) { }
            
        });

        public ICommand OfficeMailClicked => new Command<string>(async (url) =>
        {
            //Device.OpenUri(new System.Uri(url));
            Device.OpenUri(new Uri("mailto:" + "info@ug96.ru"));
        });

        public ICommand HPSNumberClicked => new Command<string>((url) =>
        {
            try
            {
                PhoneDialer.Open("+79222256569");
            } catch { }
            
        });

        public ICommand HPSMailClicked => new Command<string>(async (url) =>
        {
            //Device.OpenUri(new System.Uri(url));
            Device.OpenUri(new Uri("mailto:" + "hps@ug96.ru"));
        });

        public ICommand SergNumberClicked => new Command<string>((url) =>
        {
            try
            {
                PhoneDialer.Open("+791936654749");
            }catch (Exception ex) { }
            
        });

        public ICommand SergMailClicked => new Command<string>((url) =>
        {
            //Device.OpenUri(new System.Uri(url));
            Device.OpenUri(new Uri("mailto:" + "tsn@ug96.ru"));
        });

        public ICommand DanNumberClicked => new Command<string>((url) =>
        {
            try
            {
                PhoneDialer.Open("+79827160176");
            } catch { }
        });

        public ICommand DanMailClicked => new Command<string>((url) =>
        {
            //Device.OpenUri(new System.Uri(url));
            Device.OpenUri(new Uri("mailto:" + "bdi@ug96.ru"));
        });

        public LinkPage()
        {
            InitializeComponent();
            BindingContext = this;
        }
    }
}