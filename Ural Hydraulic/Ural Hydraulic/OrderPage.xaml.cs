using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
Тут нет правок за июль, то есть фитинги R7/PTFE стерлись
График съехал на пару пикселей
Каталожный номер только A-Z, a-z, 0-9, " ", -
Вылеты при вводе неправильного кат номера
*/
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ural_Hydraulic
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderPage : ContentPage
    {
        int TypeSize = 0, DySize = 0, PressureSize = 0, LengthSize = 0,
            FitingOneSize = 0, AngleOneSize = 0, RatioOneSize = 0,
            FitingTwoSize = 0, AngleTwoSize = 0, RatioTwoSize = 0,
                               CommentSize = 0, ItemsSelected = 0;
        string[] Type = new string[1], FitingOne = new string[1],
        FitingTwo = new string[1], Dy = new string[1],
        Pressure = new string[1], Length = new string[1],
        AngleOne = new string[1], RatioOne = new string[1],
        AngleTwo = new string[1], RatioTwo = new string[1],
        Comment = new string[1];
        bool[] DyPickerItemsAdded = new bool[10];
        string CatalogNumber;
        bool Notified = false;

        protected override void OnAppearing()
        {
            base.OnAppearing();
            DisplayAlert("", "Если у вас есть каталожный номер, просто укажите его ниже", "Ок");
            PressureLabel.Text = "(авто)";
            for (int i = 0; i < DyPickerItemsAdded.Length; i++) DyPickerItemsAdded[i] = true;
        }

        protected override bool OnBackButtonPressed()
        {
            //Этот код запрещает жест "назад", который ломает всю логику передачи переменных.
            return true;
        }

        int LocalCounter;
        string LocalOrder;
        public OrderPage(string ORDER, int counter)
        {
            LocalOrder = ORDER;
            LocalCounter = counter;
            InitializeComponent();
        }

        /* Что тут происходит. Логика такая:
         * При навигации страница принимает две переменные, счетчик номера рукава и сам заказ
         * При запуске страницы локальные для нее переменные принимают эти значения. 
         * Страница работает с локальными для нее переменными, а при навигации записывает их
         *      значения в основные переменные. 
         * То есть ORDER и counter задействованы только при навигации.
         */

        public void CartButton_Clicked(object sender, EventArgs e)
        {
            /* Что происходит тут.
             * Заказ собирается в локальную переменную чисто для просмотра корзины.
             * При переходе на страницу-корзину собирается уже основная переменная с заказом, 
             * та что отправится на почту работнику. Обычная сбрасывается, а потом собирается по формуле
             * (основной заказ)+(те шланги что пользователь собрал сейчас)
             * Кнопка блокируется чтобы избежать открытия одновременно множества страниц
             */

            CartButton.IsEnabled = false;
            for (int i = 0; i < Type.Length - 1; i++)
            {
                if (Comment[i] != null)
                {
                    LocalOrder += $"\n" +
                    $"--------------------------------------------- \n" +
                    $"Рукав №{LocalCounter + 1} \n" +
                    $"Тип соединения: {Type[i]} \n" +
                    $"Внутрений диаметр: {Dy[i]}ММ \n" +
                    $"Давление: {Pressure[i]}MPa \n" +
                    $"Длина: {Length[i]}ММ \n" +
                    $"ФИТИНГИ: \n" +
                    $"Тип соединения первого фитинга: {FitingOne[i]} \n" +
                    $"Угол фитинга: {AngleOne[i]} \n" +
                    $"Соотношение резьба/диаметр: {RatioOne[i]} \n" +
                    $"Тип соединения второго фитинга: {FitingTwo[i]} \n" +
                    $"Угол второго фитинга: {AngleTwo[i]} \n" +
                    $"Соотношение резьба/диаметр: {RatioTwo[i]} \n" +
                    $"Комментарий: {Comment[i]} \n";
                    LocalCounter++;
                }

                else if (Comment[i] == null)
                {
                    LocalOrder += $"\n" +
                    $"--------------------------------------------- \n" +
                    $"Рукав №{LocalCounter + 1} \n" +
                    $"Тип соединения: {Type[i]} \n" +
                    $"Внутрений диаметр: {Dy[i]}ММ \n" +
                    $"Давление: {Pressure[i]}MPa \n" +
                    $"Длина: {Length[i]}ММ \n" +
                    $"ФИТИНГИ: \n" +
                    $"Тип соединения первого фитинга: {FitingOne[i]} \n" +
                    $"Угол фитинга: {AngleOne[i]} \n" +
                    $"Соотношение резьба/диаметр: {RatioOne[i]} \n" +
                    $"Тип соединения второго фитинга: {FitingTwo[i]} \n" +
                    $"Угол второго фитинга: {AngleTwo[i]} \n" +
                    $"Соотношение резьба/диаметр: {RatioTwo[i]} \n";
                    LocalCounter++;
                }
            }
            Array.Resize(ref Type, 1);
            Type_Picker.SelectedIndex = -1;
            Dy_Picker.SelectedIndex = -1;
            PressureLabel.Text = "";
            Length_Entry.Text = string.Empty;
            FitingOne_Picker.SelectedIndex = -1;
            AngleOne_Entry.Text = string.Empty;
            RatioOne_Entry.Text = string.Empty;
            FitingTwo_Picker.SelectedIndex = -1;
            AngleTwo_Entry.Text = string.Empty;
            RatioTwo_Entry.Text = string.Empty;
            TypeSize = 0;
            DySize = 0;
            PressureSize = 0;
            LengthSize = 0;
            FitingOneSize = 0;
            AngleOneSize = 0;
            RatioOneSize = 0;
            FitingTwoSize = 0;
            AngleTwoSize = 0;
            RatioTwoSize = 0;
            CommentSize = 0;
            //await DisplayAlert("", $"Корзина: {ORDERView}", "Ок");
            Console.WriteLine(LocalOrder);
            Navigation.PushModalAsync(new SendPage(LocalOrder, LocalCounter));
            CartButton.IsEnabled = true;
        }
        async public void AddToCartButton_Clicked(object sender, EventArgs e)
        {
            //if (Type[TypeSize] == null || Dy[DySize] == null || Pressure[PressureSize] == null ||
            //    Length[LengthSize] == null || FitingOne[FitingOneSize] == null || AngleOne[AngleOneSize]
            //    == null || RatioOne[RatioOneSize] == null || AngleTwo[AngleTwoSize] == null || RatioTwo
            //    [RatioTwoSize] == null || FitingTwo[FitingTwoSize] == null && Notified == false)
            //    //ItemsSelected < 8 && Notified == false)
            //{
            //    //Что-то не заполнено
            //    await DisplayAlert("", $"Один или несколько параметров рукава не указаны. Проверьте значения." +
            //    $" Если все указано верно, повторно нажмите ''Добавить в корзину''", "Ок");
            //    Notified = true;
            //}
            //else
            //{
                Array.Resize(ref Type, Type.Length + 1);
                Array.Resize(ref Dy, Dy.Length + 1);
                Array.Resize(ref Pressure, Pressure.Length + 1);
                Array.Resize(ref Length, Length.Length + 1);
                Array.Resize(ref FitingOne, FitingOne.Length + 1);
                Array.Resize(ref AngleOne, AngleOne.Length + 1);
                Array.Resize(ref RatioOne, RatioOne.Length + 1);
                Array.Resize(ref AngleTwo, AngleTwo.Length + 1);
                Array.Resize(ref RatioTwo, RatioTwo.Length + 1);
                Array.Resize(ref FitingTwo, FitingTwo.Length + 1);
                Array.Resize(ref Comment, Comment.Length + 1);
                Type_Picker.SelectedIndex = -1;
                Dy_Picker.SelectedIndex = -1;
                PressureLabel.Text = "";
                Length_Entry.Text = string.Empty;
                FitingOne_Picker.SelectedIndex = -1;
                AngleOne_Entry.Text = string.Empty;
                RatioOne_Entry.Text = string.Empty;
                FitingTwo_Picker.SelectedIndex = -1;
                AngleTwo_Entry.Text = string.Empty;
                RatioTwo_Entry.Text = string.Empty;
                TypeSize++;
                DySize++;
                PressureSize++;
                LengthSize++;
                FitingOneSize++;
                AngleOneSize++;
                RatioOneSize++;
                FitingTwoSize++;
                AngleTwoSize++;
                RatioTwoSize++;
                CommentSize++;
                Notified = false;
                ItemsSelected = 0;
            DisplayAlert("", "Ваш рукав успешно добавлен в корзину!", "Ок");
            //Увеличивает размер массивов, счетчики размера массивов, очищает поля для ввода
            //}
        }
        void Type_Selected(object sender, EventArgs e)
        {
            if (Type_Picker.SelectedIndex != -1)
            {
                Type[TypeSize] = Type_Picker.Items[Type_Picker.SelectedIndex];
                ItemsSelected++;
            }

            if (Type[TypeSize] == "4SP" || Type[TypeSize] == "4SH" ||
                        Type[TypeSize] == "R13" || Type[TypeSize] == "R15")
            {
                DyPickerItemsAdded[0] = false;
                Dy_Picker.Items.Remove("6,4");
            }
            if (Type[TypeSize] == "1SN" || Type[TypeSize] == "2SN")
            {
                if (DyPickerItemsAdded[0] == false)
                {
                    DyPickerItemsAdded[0] = true;
                    Dy_Picker.Items.Add("6,4");
                    //Для 1SN и 2SN нужен 6,4, поэтому он удаляется для остальных типов и добавляется
                    // снова здесь. Попутно делаются пометки в массиве чтобы не было ошибок
                }
            }

            if (Type[TypeSize] == "4SP" || Type[TypeSize] == "4SH" ||
                        Type[TypeSize] == "R13" || Type[TypeSize] == "R15")
            {
                DyPickerItemsAdded[1] = false;
                Dy_Picker.Items.Remove("7,9");
            }
            if (Type[TypeSize] == "1SN" || Type[TypeSize] == "2SN")
            {
                if (DyPickerItemsAdded[1] == false)
                {
                    DyPickerItemsAdded[1] = true;
                    Dy_Picker.Items.Add("7,9");
                }
            }

            if (Type[TypeSize] == "4SH" || Type[TypeSize] == "R13" || Type[TypeSize] == "R15")
            {
                DyPickerItemsAdded[2] = false;
                Dy_Picker.Items.Remove("9,5");
            }
            if (Type[TypeSize] == "1SN" || Type[TypeSize] == "2SN" || Type[TypeSize] == "4SP")
            {
                if (DyPickerItemsAdded[2] == false)
                {
                    DyPickerItemsAdded[2] = true;
                    Dy_Picker.Items.Add("9,5");
                }
            }

            if (Type[TypeSize] == "4SH" || Type[TypeSize] == "R13" || Type[TypeSize] == "R15")
            {
                DyPickerItemsAdded[3] = false;
                Dy_Picker.Items.Remove("12,7");
            }
            if (Type[TypeSize] == "1SN" || Type[TypeSize] == "2SN" || Type[TypeSize] == "4SP")
            {
                if (DyPickerItemsAdded[3] == false)
                {
                    DyPickerItemsAdded[3] = true;
                    Dy_Picker.Items.Add("12,7");
                }
            }
            if (Type[TypeSize] == "4SH" || Type[TypeSize] == "R13" || Type[TypeSize] == "R15")
            {
                DyPickerItemsAdded[4] = false;
                Dy_Picker.Items.Remove("15,9");
            }
            if (Type[TypeSize] == "1SN" || Type[TypeSize] == "2SN" || Type[TypeSize] == "4SP")
            {
                if (DyPickerItemsAdded[4] == false)
                {
                    DyPickerItemsAdded[4] = true;
                    Dy_Picker.Items.Add("15,9");
                }
            }
        }

        string HighPress = "                                          ";

        void Dy_Selected(object sender, EventArgs e)
        {
            if (Dy_Picker.SelectedIndex != -1)
            {
                Dy[DySize] = Dy_Picker.Items[Dy_Picker.SelectedIndex];
                ItemsSelected++;
            }
            PressureLabel.Text = (string)Dy[0];
            FitingOne_Picker.Title = (string)HighPress;
            FitingTwo_Picker.Title = (string)HighPress;
            switch (Dy[DySize])
            {

                //СДЕЛАТЬ ОЧИСТКУ!!!
                case "6,4":
                    //PressureLabel.Text =
                    if (Type[TypeSize] == "1SN")
                    {
                        PressureLabel.Text = "22,5";
                        Pressure[PressureSize] = "22,5";
                    }
                    if (Type[TypeSize] == "2SN")
                    {
                        PressureLabel.Text = "40";
                        Pressure[PressureSize] = "40";
                    }
                    break;
                case "7,9":
                    if (Type[TypeSize] == "1SN")
                    {
                        PressureLabel.Text = "21,5";
                        Pressure[PressureSize] = "21,5";
                    }
                    if (Type[TypeSize] == "2SN")
                    {
                        PressureLabel.Text = "35";
                        Pressure[PressureSize] = "35";
                    }
                    break;
                case "9,5":
                    if (Type[TypeSize] == "1SN")
                    {
                        PressureLabel.Text = "18";
                        Pressure[PressureSize] = "18";
                    }
                    if (Type[TypeSize] == "2SN")
                    {
                        PressureLabel.Text = "33";
                        Pressure[PressureSize] = "33";
                    }
                    if (Type[TypeSize] == "4SP")
                    {
                        PressureLabel.Text = "44,5";
                        Pressure[PressureSize] = "44,5";
                    }
                    break;
                case "12,7":
                    if (Type[TypeSize] == "1SN")
                    {
                        PressureLabel.Text = "16";
                        Pressure[PressureSize] = "16";
                    }
                    if (Type[TypeSize] == "2SN")
                    {
                        PressureLabel.Text = "27,5";
                        Pressure[PressureSize] = "27,5";
                    }
                    if (Type[TypeSize] == "4SP")
                    {
                        PressureLabel.Text = "41,5";
                        Pressure[PressureSize] = "41,5";
                    }
                    break;
                case "15,9":
                    if (Type[TypeSize] == "1SN")
                    {
                        PressureLabel.Text = "13";
                        Pressure[PressureSize] = "13";
                    }
                    if (Type[TypeSize] == "2SN")
                    {
                        PressureLabel.Text = "26";
                        Pressure[PressureSize] = "26";
                    }
                    if (Type[TypeSize] == "4SP")
                    {
                        PressureLabel.Text = "35";
                        Pressure[PressureSize] = "35";
                    }
                    break;
                case "19":
                    if (Type[TypeSize] == "1SN")
                    {
                        PressureLabel.Text = "10,5";
                        Pressure[PressureSize] = "10,5";
                    }
                    if (Type[TypeSize] == "2SN")
                    {
                        PressureLabel.Text = "21,5";
                        Pressure[PressureSize] = "21,5";
                    }
                    if (Type[TypeSize] == "4SP")
                    {
                        PressureLabel.Text = "35";
                        Pressure[PressureSize] = "35";
                    }
                    if (Type[TypeSize] == "4SH")
                    {
                        PressureLabel.Text = "42";
                        Pressure[PressureSize] = "42";
                    }
                    if (Type[TypeSize] == "R13")
                    {
                        PressureLabel.Text = "35";
                        Pressure[PressureSize] = "35";
                    }
                    if (Type[TypeSize] == "R15")
                    {
                        PressureLabel.Text = "42";
                        Pressure[PressureSize] = "42";
                    }
                    break;
                case "25,4":
                    if (Type[TypeSize] == "1SN")
                    {
                        PressureLabel.Text = "88";
                        Pressure[PressureSize] = "88";
                    }
                    if (Type[TypeSize] == "2SN")
                    {
                        PressureLabel.Text = "16,5";
                        Pressure[PressureSize] = "16,5";
                    }
                    if (Type[TypeSize] == "4SP")
                    {
                        PressureLabel.Text = "28";
                        Pressure[PressureSize] = "28";
                    }
                    if (Type[TypeSize] == "4SH")
                    {
                        PressureLabel.Text = "38";
                        Pressure[PressureSize] = "38";
                    }
                    if (Type[TypeSize] == "R13")
                    {
                        PressureLabel.Text = "35";
                        Pressure[PressureSize] = "35";
                    }
                    if (Type[TypeSize] == "R15")
                    {
                        PressureLabel.Text = "42";
                        Pressure[PressureSize] = "42";
                    }
                    break;
                case "31,8":
                    if (Type[TypeSize] == "1SN")
                    {
                        PressureLabel.Text = "63";
                        Pressure[PressureSize] = "63";
                    }
                    if (Type[TypeSize] == "2SN")
                    {
                        PressureLabel.Text = "12,5";
                        Pressure[PressureSize] = "12,5";
                    }
                    if (Type[TypeSize] == "4SP")
                    {
                        PressureLabel.Text = "21";
                        Pressure[PressureSize] = "21";
                    }
                    if (Type[TypeSize] == "4SH")
                    {
                        PressureLabel.Text = "35";
                        Pressure[PressureSize] = "35";
                    }
                    if (Type[TypeSize] == "R13")
                    {
                        PressureLabel.Text = "35";
                        Pressure[PressureSize] = "35";
                    }
                    if (Type[TypeSize] == "R15")
                    {
                        PressureLabel.Text = "42";
                        Pressure[PressureSize] = "42";
                    }
                    break;
                case "38,1":
                    if (Type[TypeSize] == "1SN")
                    {
                        PressureLabel.Text = "50";
                        Pressure[PressureSize] = "50";
                    }
                    if (Type[TypeSize] == "2SN")
                    {
                        PressureLabel.Text = "90";
                        Pressure[PressureSize] = "90";
                    }
                    if (Type[TypeSize] == "4SP")
                    {
                        PressureLabel.Text = "185";
                        Pressure[PressureSize] = "185";
                    }
                    if (Type[TypeSize] == "4SH")
                    {
                        PressureLabel.Text = "29";
                        Pressure[PressureSize] = "29";
                    }
                    if (Type[TypeSize] == "R13")
                    {
                        PressureLabel.Text = "35";
                        Pressure[PressureSize] = "35";
                    }
                    if (Type[TypeSize] == "R15")
                    {
                        PressureLabel.Text = "42";
                        Pressure[PressureSize] = "42";
                    }
                    break;
                case "50,8":
                    if (Type[TypeSize] == "1SN")
                    {
                        PressureLabel.Text = "40";
                        Pressure[PressureSize] = "40";
                    }
                    if (Type[TypeSize] == "2SN")
                    {
                        PressureLabel.Text = "80";
                        Pressure[PressureSize] = "80";
                    }
                    if (Type[TypeSize] == "4SP")
                    {
                        PressureLabel.Text = "165";
                        Pressure[PressureSize] = "165";
                    }
                    if (Type[TypeSize] == "4SH")
                    {
                        PressureLabel.Text = "25";
                        Pressure[PressureSize] = "25";
                    }
                    if (Type[TypeSize] == "R13")
                    {
                        PressureLabel.Text = "35";
                        Pressure[PressureSize] = "35";
                    }
                    if (Type[TypeSize] == "R15")
                    {
                        PressureLabel.Text = "42";
                        Pressure[PressureSize] = "42";
                    }
                    break;
            }
        }

        void Length_Selected(object sender, EventArgs e)
        {
            Length[LengthSize] = ((Entry)sender).Text;
            ItemsSelected++;
        }

        void FitingOne_Selected(object sender, EventArgs e)
        {
            if (FitingOne_Picker.SelectedIndex != -1)
            {
                FitingOne[FitingOneSize] = FitingOne_Picker.Items[FitingOne_Picker.SelectedIndex];
                ItemsSelected++;
            }
        }

        void AngleOne_Selected(object sender, EventArgs e)
        {
            AngleOne[AngleOneSize] = ((Entry)sender).Text;
            ItemsSelected++;
        }

        void RatioOne_Selected(object sender, EventArgs e)
        {
            RatioOne[RatioOneSize] = ((Entry)sender).Text;
            ItemsSelected++;
        }

        void FitingTwo_Selected(object sender, EventArgs e)
        {
            if (FitingTwo_Picker.SelectedIndex != -1)
            {
                FitingTwo[FitingTwoSize] = FitingTwo_Picker.Items[FitingTwo_Picker.SelectedIndex];
                ItemsSelected++;
            }
        }

        void AngleTwo_Selected(object sender, EventArgs e)
        {
            AngleTwo[AngleTwoSize] = ((Entry)sender).Text;
            ItemsSelected++;
        }

        void RatioTwo_Selected(object sender, EventArgs e)
        {
            RatioTwo[RatioTwoSize] = ((Entry)sender).Text;
            ItemsSelected++;
        }

        async void AddComment_Clicked(object sender, EventArgs e)
        {
            Comment[CommentSize] = await DisplayPromptAsync("Добавить комментарий", null, "Добавить", "Отмена ");
            ItemsSelected++;
        }
        async void Catalog_Clicked(object sender, EventArgs e)
        {
            CatalogNumber = await DisplayPromptAsync("Введите каталожный номер", null, "Добавить", null, keyboard: Keyboard.Numeric);
            if (CatalogNumber.Length > 2)
            {
                LocalOrder += $"\n" +
                    $"--------------------------------------------- \n" +
                    $"Рукав №{LocalCounter + 1} \n" +
                    $"Каталожный номер: {CatalogNumber}\n";
                LocalCounter++;
                Type_Picker.SelectedIndex = -1;
                Dy_Picker.SelectedIndex = -1;
                PressureLabel.Text = "";
                Length_Entry.Text = string.Empty;
                FitingOne_Picker.SelectedIndex = -1;
                AngleOne_Entry.Text = string.Empty;
                RatioOne_Entry.Text = string.Empty;
                FitingTwo_Picker.SelectedIndex = -1;
                AngleTwo_Entry.Text = string.Empty;
                RatioTwo_Entry.Text = string.Empty;
            }
            else {}

            async void Checker()
            {

            }

            //Далее алгоритм сборки всего заказа и его отправка по почте. Закоментирован за ненадобностью

            /*
            GeneralComment = await DisplayPromptAsync("Добавить комментарий к заказу", "", "Назад", "Продолжить");
            int Number = 1;
            for (int i = 0; i < Type.Length - 1; i++)
            {
                if (Comment[i] != null)
                {
                    ORDER += $"\n" +
                    $"--------------------------------------------- \n" +
                    $"Рукав №{Number}. \n" +
                    $"Тип соединения: {Type[i]} \n" +
                    $"Внутрений диаметр: {Dy[i]}ММ \n" +
                    $"Давление: {Pressure[i]}MPa \n" +
                    $"Длина: {Length[i]}ММ \n" +
                    $"ФИТИНГИ: \n" +
                    $"Тип соединения первого фитинга: {FitingOne[i]} \n" +
                    $"Угол фитинга: {AngleOne[i]} \n" +
                    $"Соотношение резьба/диаметр: {RatioOne[i]} \n" +
                    $"Тип соединения второго фитинга: {FitingTwo[i]} \n" +
                    $"Угол второго фитинга: {AngleTwo[i]} \n" +
                    $"Соотношение резьба/диаметр: {RatioTwo[i]} \n" +
                    $"Комментарий к шлангу: {Comment[i]}";
                    Number++;
                }

                else if (Comment[i] == null)
                {
                    ORDER += $"\n" +
                    $"--------------------------------------------- \n" +
                    $"Рукав №{Number}. \n" +
                    $"Тип соединения: {Type[i]} \n" +
                    $"Внутрений диаметр: {Dy[i]}ММ \n" +
                    $"Давление: {Pressure[i]}MPa \n" +
                    $"Длина: {Length[i]}ММ \n" +
                    $"ФИТИНГИ: \n" +
                    $"Тип соединения первого фитинга: {FitingOne[i]} \n" +
                    $"Угол фитинга: {AngleOne[i]} \n" +
                    $"Соотношение резьба/диаметр: {RatioOne[i]} \n" +
                    $"Тип соединения второго фитинга: {FitingTwo[i]} \n" +
                    $"Угол второго фитинга: {AngleTwo[i]} \n" +
                    $"Соотношение резьба/диаметр: {RatioTwo[i]} \n";
                    Number++;
                }
            }
            try
            {
                ORDER += $"--------------------------------------------- \n" +
                    $"Комментарий ко всему заказу: {GeneralComment}";
                SmtpClient MysmtpClient = new SmtpClient("smtp.mail.ru");
                MysmtpClient.UseDefaultCredentials = false;
                MysmtpClient.EnableSsl = true;
                System.Net.NetworkCredential BasicAuthenticationInfo = new
                System.Net.NetworkCredential("uralhydraulicrass@mail.ru", "PFMXGbyFzetxgpR1nUgM");
                MysmtpClient.Credentials = BasicAuthenticationInfo;
                //Letter itself
                MailAddress from = new MailAddress("uralhydraulicrass@mail.ru", "Новый заказ");
                MailAddress to = new MailAddress("halin040482@gmail.com", "halin75@mail.ru");
                MailMessage myMail = new System.Net.Mail.MailMessage(from, to);
                myMail.Subject = "Заказ №";
                myMail.SubjectEncoding = System.Text.Encoding.UTF8;
                myMail.Body = $"Заказ: \n {ORDER}";
                myMail.BodyEncoding = System.Text.Encoding.UTF8;
                myMail.IsBodyHtml = false;
                MysmtpClient.Send(myMail);
            
            }

            catch (Exception)
            {
                await DisplayAlert("Ошибка", "Ошибка при отправке заказа, проверьте подключение к интернету", "Ок");
            }
            */
        }
    }
}

/*
        void Type_Selected(object sender, EventArgs e)
        {
            int Selected = 1;
            if (Selected == 1)
            {
                Type_Picker.Items.Add("Selected!!!");                            !!!ВАЖНО!!!
            }
        }

        public async void CartButton_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("", "Когда заказ будет готов, перейдите в корзину справа сверху", "Ок");
        }
        
 */


/*
        bool alert = false;
        int a = 0, b = 0, c = 0, d = 0, E = 0, f = 0, g = 0, h = 0, i = 0;
        string[] Type = new string[0];
        string[] Ratio = new string[0];
        string[] Temperature = new string[0];
        string[] Pressure = new string[0];
        string[] AngleOne = new string[0];
        string[] AngleTwo = new string[0];
        string[] Reversal = new string[0];
        string[] Rubber = new string[0];
        readonly string[] Amount = new string[0];


        void AmountEntry_Completed(object sender, EventArgs e)
        {
            Array.Resize(ref Type, Type.Length + 1);
            Amount[i] = ((Entry)sender).Text;
            i++;
        }

        public Page1()
        {
            InitializeComponent();
        }

        async void CartButtonClicked(object sender, EventArgs e)
        {
            await DisplayAlert("", "Когда заказ будет готов, перейдите в корзину справа сверху", "Ок");
            CartButtonClicked(sender, e);
        }

        void Type_Selected(object sender, EventArgs e)
        {
            Array.Resize(ref Type, Type.Length + 1);
            Type[a] = Type_Picker.Items[Type_Picker.SelectedIndex];
            a++;
        }

        void Temperature_Selected(object sender, EventArgs e)
        {
            Array.Resize(ref Temperature, Temperature.Length + 1);
            Temperature[c] = ((Entry)sender).Text;
            c++;
        }

        void Ratio_Selected(object sender, EventArgs e)
        {
            Array.Resize(ref Ratio, Ratio.Length + 1);
            Ratio[b] = ((Entry)sender).Text;
            b++;
        }

        void Rubber_Selected(object sender, EventArgs e)
        {
            Array.Resize(ref Rubber, Rubber.Length + 1);
            Rubber[h] = Rubber_Picker.Items[Rubber_Picker.SelectedIndex];
            h++;
        }

        void AngleOne_Selected(object sender, EventArgs e)
        {
            Array.Resize(ref AngleOne, AngleOne.Length + 1);
            AngleOne[E] = ((Entry)sender).Text;
            E++;
        }

        void Pressure_Selected(object sender, EventArgs e)
        {
            Array.Resize(ref Pressure, Pressure.Length + 1);
            Pressure[d] = ((Entry)sender).Text;
            d++;
        }
        void AngleTwo_Selected(object sender, EventArgs e)
        {
            Array.Resize(ref AngleTwo, AngleTwo.Length + 1);
            AngleTwo[f] = ((Entry)sender).Text;
            f++;
        }

        void Reversal_Selected(object sender, EventArgs e)
        {
            Array.Resize(ref Reversal, Reversal.Length + 1);
            Reversal[g] = ((Entry)sender).Text;
            g++;
        }

        async void ButtonAddToCartClicked(object sender, EventArgs e)
        {

            if (alert == false)
            {
                await DisplayAlert("", "Когда заказ будет готов, перейдите в корзину справа сверху", "Ок");
                alert = true;
            }
        }
*/