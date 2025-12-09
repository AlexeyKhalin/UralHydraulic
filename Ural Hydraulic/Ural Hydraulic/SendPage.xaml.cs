using System;
using System.Net.Mail;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ural_Hydraulic
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SendPage : ContentPage
    {
        string Name, Organization, Phone, Mail;
        bool OrderHasBeenSent = false;
        DateTime dateTime = DateTime.Now;

        //Какая во всем приложении структура: при навигации принимаются переменные,
        //  но они локальные: поэтому они перезаписываются сразу же в Local
        //  переменные, которые уже используются во всем классе на странице. 

        int LocalCounter;
        string LocalOrder;
        public SendPage(string ORDER, int counter)
        {
            LocalOrder = ORDER;
            LocalCounter = counter;
            InitializeComponent();
            BackgroundImageSource = "MainPageBackground.png";
        }
        /*
        async protected override bool OnBackButtonPressed()
        {
            //Этот код запрещает жест "назад", который ломает всю логику.
            //return true;
        }
        */
        private void NameChanged(object sender, EventArgs e)
        {
            NameDisplay.Text = "Ваше имя";
            Name = ((Entry)sender).Text;
        }

        private void OrgChanged(object sender, EventArgs e)
        {
            OrgDisplay.Text = "Организация";
            Organization = ((Entry)sender).Text;
        }

        private void MailChanged(object sender, EventArgs e)
        {
            string text = ((Entry)sender).Text;


            if (!text.Contains("@")) // если введенный текст не содержит символ "@"
            {
                MailDisplay.Text = "Некорректный адрес"; // выводим текст об ошибке
            }
            else
            {
                MailDisplay.Text = "Почтовый адрес"; // выводим текст о корректности адреса
                Mail = text; // сохраняем адрес почты

                if (!text.EndsWith("@") && !text.EndsWith(".")) // если адрес не заканчивается на символ "@" или "."
                {
                    int atIndex = text.IndexOf("@");
                    if (atIndex < text.Length - 4) // если после символа "@" есть как минимум 3 символа, включая "."
                    {
                        int dotIndex = text.LastIndexOf(".");
                        if (dotIndex > atIndex) // если после символа "@" есть еще хотя бы один символ
                        {
                            MailDisplay.Text = "Почтовый адрес"; // выводим текст о корректности адреса
                            Mail = text; // сохраняем адрес почты
                        }
                        else
                        {
                            MailDisplay.Text = "Некорректный адрес"; // выводим текст об ошибке
                        }
                    }
                    else
                    {
                        MailDisplay.Text = "Некорректный адрес"; // выводим текст об ошибке
                    }
                }
                else
                {
                    MailDisplay.Text = "Некорректный адрес"; // выводим текст об ошибке
                }
            }
        }

        //private void MailChanged(object sender, EventArgs e)
        //{
        //    string mail = ((Entry)sender).Text;


        //    if (mail.Contains(".") && mail.Contains("@"))
        //    {
        //        MailDisplay.Text = "Почтовый адрес";
        //        Mail = ((Entry)sender).Text;
        //    }
        //    else
        //    {
        //        Mail = null;
        //    }
        //}
        /*
         * private void MailChanged(object sender, EventArgs e) 
         * { 
         * MailDisplay.Text = "Почтовый адрес";
         * Mail = ((Entry)sender).Text; 
         * }
         */

        private void PhoneChanged(object sender, EventArgs e)
        {
            PhoneDisplay.Text = "Контактный номер телефона";
            Phone = ((Entry)sender).Text;
        }
        /*
        async public void BackButton_Clicked(object sender, EventArgs e)
        {
            BackButton.IsEnabled = false;
            await Navigation.PushModalAsync(new OrderPage(LocalOrder, LocalCounter));
            //На этой странице нет нужды в пересборке, так как в заказ не вносятся изменения.
            System.Threading.Thread.Sleep(1000);
            BackButton.IsEnabled = true;
        }
        */

        async void CheckButton_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Содержимое корзины", LocalOrder, "Ок");
        }


        private async void SENDButton_Clicked(object sender, EventArgs e)
        {
            //Проверка контактной информации. Не был ли уже отправлен заказ. Нет ли ошибки. Суммарно три проверки, если
            //  заказ уже был отправлен/не заполнена контактная инфа/ошибка, то просто появляется уведомление.
            if (Name == null | Phone == null | Mail == null)
            {
                DisplayAlert(null, "Пожалуйста, заполните обязательные поля с контактной информацией. Возможно, вы " +
                    "неправильно указали почтовый адрес", "Ок");
            }
            else
            {
                if (OrderHasBeenSent == false)
                {
                    SendButton.IsEnabled = false;
                    try
                    {
                        if (LocalOrder != null) LocalOrder += $"--------------------------------------------- \n";
                        SmtpClient MysmtpClient = new SmtpClient("smtp.mail.ru");
                        MysmtpClient.UseDefaultCredentials = false;
                        MysmtpClient.EnableSsl = true;
                        System.Net.NetworkCredential BasicAuthenticationInfo = new
                        System.Net.NetworkCredential("uralhydraulicrassylka@mail.ru", "4N0XGfNXcTRmtaMyZ6E0");
                        //Эта хрень называется "пароль для внешних приложений"
                        MysmtpClient.Credentials = BasicAuthenticationInfo;
                        //Letter itself
                        MailAddress from = new MailAddress("uralhydraulicrassylka@mail.ru", "Оформлен новый заказ");
                        //MailAddress to = new MailAddress("halin040482@gmail.com", "halin75@mail.ru - кому");
                        MailAddress to = new MailAddress("halin040482@gmail.com", "halin040482@gmail.com - кому");
                        MailMessage myMail = new System.Net.Mail.MailMessage(from, to);
                        myMail.Subject = $"Заказ от {dateTime}";
                        myMail.SubjectEncoding = System.Text.Encoding.UTF8;
                        if (Organization != null) myMail.Body = $"Организация: {Organization}\n" +
                            $"Имя заказчика: {Name} \n" +
                            $"Адрес почты: {Mail} \n" +
                            $"Контактный номер телефона: {Phone} \n" +
                            $"{LocalOrder}";
                        else myMail.Body = $"Заказчик: {Name} \n" +
                            $"Адрес почты: {Mail} \n" +
                            $"Контактный номер телефона: {Phone} \n" +
                            $"{LocalOrder}";
                        //В первом случае указана организация, во втором нет.
                        myMail.BodyEncoding = System.Text.Encoding.UTF8;
                        myMail.IsBodyHtml = false;
                        MysmtpClient.Send(myMail);
                        OrderHasBeenSent = true;
                        DisplayAlert("Оповещение", "Ваш заказ успешно отправлен \n" +
                            "Скоро с вами свяжутся для подтверждения", "Ок");
                    }

                    catch (Exception)
                    {
                        await DisplayAlert("Ошибка", "Ошибка при отправке заказа, проверьте подключение к интернету", "Ок");
                        OrderHasBeenSent = false;
                    }
                    System.Threading.Thread.Sleep(1000);
                    SendButton.IsEnabled = true;
                }
                else if (OrderHasBeenSent == true) DisplayAlert("Оповещение", "Ваш заказ уже был отправлен", "Ок");
            }
        }
    }
}


