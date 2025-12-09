using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Ural_Hydraulic
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BackgroundImageSource = "MainPageBackground.png";
        }

        string ORDER = "";
        int counter = 0;


        async void buttonf_Clicked(object sender, EventArgs e)
        {
            buttonf.IsEnabled = false;
            await Navigation.PushModalAsync(new OrderPage(ORDER, counter));
        }
        async void buttons_Clicked(object sender, EventArgs e)
        {
            buttonf.IsEnabled = false;
            await Browser.OpenAsync(new Uri("https://www.ug96.ru/"));
            System.Threading.Thread.Sleep(1000);
            buttonf.IsEnabled = true;
        }
        async void buttont_Clicked(object sender, EventArgs e)
        {
            buttonf.IsEnabled = false;
            await Navigation.PushModalAsync(new LinkPage());
            buttonf.IsEnabled = true;
        }
        override protected void OnAppearing()
        {
            /*
            Image image = new Image { Source = "MainPageBackground.png" };
            ImageButton buttonf = new ImageButton { Source = "WebButton.png", ScaleX = 0.7, ScaleY = 0.7, Aspect = Aspect.AspectFill };
            ImageButton buttons = new ImageButton { Source = "OrderButton.png", ScaleX = 0.7, ScaleY = 0.7, Aspect = Aspect.AspectFill };
            ImageButton buttont = new ImageButton { Source = "ContactsButton.png", ScaleX = 0.7, ScaleY = 0.7, Aspect = Aspect.AspectFill };
            

            buttonf.Clicked += buttonf_Clicked;
            buttons.Clicked += buttons_Clicked;
            buttont.Clicked += buttont_Clicked;

            StackLayout stackLayout = new StackLayout()
            {
                BackgroundColor = Color.FromHex("#8f8e94"),
                Spacing = 80,
                //Children = { buttons, buttont, image }

            };

            async void buttons_Clicked(object sender, EventArgs e)
            {
                buttons.IsEnabled = false;
                await Navigation.PushModalAsync(new OrderPage(ORDER, counter));
            }

            async void buttont_Clicked(object sender, EventArgs e)
            {
                buttont.IsEnabled = false;
                //await Navigation.PushModalAsync(new LinkPage());
            }

            Content = stackLayout;

            */
        }
    }
}

