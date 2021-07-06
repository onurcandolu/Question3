using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Question3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string language = "en";
        public string iconpath="";
        SqlConnection connection;
        SqlCommand command;
        SqlDataReader dataReader;
        string connetionString = "Data Source=ARKV\\SQLSERVER;Initial Catalog=ActivityAcount;User ID=sa;Password=123qwe";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RowDefinition_MouseDown(object sender, MouseButtonEventArgs e)
        {
          
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
            
        }
        private void MenuContol(object sender, RoutedEventArgs e)
        {

            Main.Visibility = Visibility.Hidden;
            Data.Visibility = Visibility.Hidden;
            Add.Visibility = Visibility.Hidden;
            User.Visibility = Visibility.Hidden;
            if (sender.Equals(MainButton))
            {
                Main.Visibility = Visibility.Visible;
            }
            else if (sender.Equals(ListButton))
            {
                Data.Children.Clear();
                Data.Visibility = Visibility.Visible;
                string sql = null;
                sql = "select* from Activity";
                connection = new SqlConnection(connetionString);
                try
                {
                    connection.Open();
                    command = new SqlCommand(sql, connection);
                    dataReader = command.ExecuteReader();
                    int margintop = 10;
                    int Total = 0;
                    while (dataReader.Read())
                    {
                        var id= dataReader.GetInt32(0);
                        string Activity = dataReader.GetString(1).ToUpper();
                        string Icon = dataReader.GetString(3);
                        var Money = dataReader.GetInt32(2);


                        string moneystring = Money.ToString();
                        Total += Money;
                        Image image = new Image();
                        image.Source = new BitmapImage(new Uri(Icon));
                        image.Height = 50;
                        image.Width = 50;
                        image.Margin = new Thickness(-400, margintop - 10, 0, 0);
                        image.HorizontalAlignment = HorizontalAlignment.Center;
                        image.VerticalAlignment = VerticalAlignment.Top;

                        Image remove = new Image();
                        remove.Source = new BitmapImage(new Uri("C:\\Users\\onurcan.ArkV\\source\\repos\\Question3\\Question3\\Icon\\remove.png"));
                        remove.Height = 30;
                        remove.Width = 30;
                        remove.Margin = new Thickness(420, margintop, 0, 0);
                        remove.HorizontalAlignment = HorizontalAlignment.Center;
                        remove.VerticalAlignment = VerticalAlignment.Top;
                        remove.DataContext = id.ToString(); ;
                        remove.MouseDown += new MouseButtonEventHandler((object sender, MouseButtonEventArgs ea) => {
                            connection.Open();

                            sql = "DELETE FROM Activity WHERE id=" + remove.DataContext + ";";
                            command = new SqlCommand(sql, connection);
                            command.ExecuteNonQuery();
                            connection.Close();

                            Data.Children.Clear();
                            MenuContol(ListButton,e);

                        });

                        moneystring += "$";
                        TextBlock txtBlock = new TextBlock();
                        txtBlock.Height = 30;
                        txtBlock.Width = 150;
                        txtBlock.Margin = new Thickness(-150, margintop, 0, 0);
                        txtBlock.Text = Activity;
                        txtBlock.Foreground = new SolidColorBrush(Colors.Gray);
                        txtBlock.HorizontalAlignment = HorizontalAlignment.Center;
                        txtBlock.VerticalAlignment = VerticalAlignment.Top;
                        txtBlock.FontSize = 20;

                        TextBlock MoneyBlock = new TextBlock();
                        MoneyBlock.Height = 30;
                        MoneyBlock.Width = 100;
                        MoneyBlock.Margin = new Thickness(350, margintop, 0, 0);
                        MoneyBlock.Text = moneystring;
                        MoneyBlock.Foreground = new SolidColorBrush(Colors.Red);
                        if (Money > 0)
                            MoneyBlock.Foreground = new SolidColorBrush(Colors.Green);
                        MoneyBlock.HorizontalAlignment = HorizontalAlignment.Center;
                        MoneyBlock.VerticalAlignment = VerticalAlignment.Top;
                        MoneyBlock.FontSize = 20;
                        

                        Data.Children.Add(txtBlock);
                        Data.Children.Add(MoneyBlock);
                        Data.Children.Add(image);
                        Data.Children.Add(remove);
                        margintop += 50;

                    }
                    TextBlock MoneySumBlock = new TextBlock();
                    MoneySumBlock.Height = 30;
                    MoneySumBlock.Width = 100;
                    MoneySumBlock.Margin = new Thickness(370, margintop+15, 0, 0);
                    MoneySumBlock.Text = Total.ToString();
                    MoneySumBlock.Foreground = new SolidColorBrush(Colors.Red);
                    if (Total > 0)
                        MoneySumBlock.Foreground = new SolidColorBrush(Colors.Green);
                    MoneySumBlock.HorizontalAlignment = HorizontalAlignment.Center;
                    MoneySumBlock.VerticalAlignment = VerticalAlignment.Top;
                    MoneySumBlock.FontSize = 20;


                    Line line = new Line();
                    line.X1 = 20;
                    line.Y1 = margintop + 15;
                    line.X2 = 500;
                    line.Y2 = margintop + 15;
                    SolidColorBrush grayBrush = new SolidColorBrush();
                    grayBrush.Color = Colors.Gray;
                    line.StrokeThickness = 1;
                    line.Stroke = grayBrush;

                    Data.Children.Add(line);
                    Data.Children.Add(MoneySumBlock);

                    dataReader.Close();
                    command.Dispose();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else if (sender.Equals(AddButton))
            {
                Add.Visibility = Visibility.Visible;
            }
            else if (sender.Equals(ExitButton))
            {
                Close();
            }
        }
          
            private void BorderClickCheck(object sender, MouseButtonEventArgs e)
        {
            salaryBorder.BorderThickness = new Thickness(0);
            carBorder.BorderThickness = new Thickness(0);
            shopingBorder.BorderThickness = new Thickness(0);
            houseBorder.BorderThickness = new Thickness(0);
            coffeeBorder.BorderThickness = new Thickness(0);
            burgerBorder.BorderThickness = new Thickness(0);
            if (sender.Equals(salary))
            {
                iconpath = "C:\\Users\\onurcan.ArkV\\source\\repos\\Question3\\Question3\\Icon\\salary.png";
                salaryBorder.BorderThickness = new Thickness(1);
            }
            else if (sender.Equals(car))
            {
                carBorder.BorderThickness = new Thickness(1);
                iconpath = "C:\\Users\\onurcan.ArkV\\source\\repos\\Question3\\Question3\\Icon\\car.png";
            }
            else if (sender.Equals(shoping))
            {
                shopingBorder.BorderThickness = new Thickness(1);
                iconpath = "C:\\Users\\onurcan.ArkV\\source\\repos\\Question3\\Question3\\Icon\\shopping.png";
            }
            else if (sender.Equals(house))
            {
                houseBorder.BorderThickness = new Thickness(1);
                iconpath = "C:\\Users\\onurcan.ArkV\\source\\repos\\Question3\\Question3\\Icon\\house.png";
            }
            else if (sender.Equals(coffee))
            {
                coffeeBorder.BorderThickness = new Thickness(1);
                iconpath = "C:\\Users\\onurcan.ArkV\\source\\repos\\Question3\\Question3\\Icon\\coffee.png";
            }
            else if (sender.Equals(burger))
            {
                burgerBorder.BorderThickness = new Thickness(1);
                iconpath = "C:\\Users\\onurcan.ArkV\\source\\repos\\Question3\\Question3\\Icon\\burger.png";
            }
            
        }
      
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
     
            private void activity_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(activity))
                    if(activity.Text== "Enter Activity Name ..." || activity.Text == "Aktivite Adı Girin...")
                        activity.Text = "";

             if (sender.Equals(amount))
                    if (amount.Text == "Enter Amount of Activity..."|| amount.Text == "Aktivitenin Miktarinı Girin...")
                        amount.Text = "";
            
        }

        private void languageSelect(object sender, MouseButtonEventArgs e)
        {
            Image languageImage = (Image)sender;
            if(languageImage.Name=="en")
            {
                Title.Text = "Activity Acount";
                MainButton.Content = "Main Page";
                ListButton.Content = "List All Activity";
                AddButton.Content = "Add New Activity";
                ExitButton.Content = "Exit";
                mainText.Text = "     This Program is a program that manage the list of monthly transactions of the user. To do the calculation, please press the add activity button and you can see the result by listing.";
                activity.Text = "Enter Activity Name...";
                amount.Text = "Enter Amount of Activity...";
                addActivityButton.Content = "Add New Activity";
                earning.Content = "Earning";
                expense.Content = "Expense";
                language = "en";
            }
            else if (languageImage.Name == "tr")
            {
                Title.Text = "Etkinlik Hesabı";
                MainButton.Content = "Ana Sayfa";
                ListButton.Content = "Aktiviteleri Listele";
                AddButton.Content = "Yeni Bir Aktivite Ekle";
                ExitButton.Content = "Çıkış";
                mainText.Text = "     Bu Program, kullanıcının aylık işlemlerinin listesini yöneten bir programdır. Hesaplamayı yapmak için lütfen aktivite ekle butonuna basınız ve sonucu listeleyerek görebilirsiniz.";
                activity.Text = "Aktivite Adı Girin...";
                amount.Text = "Aktivitenin Miktarinı Girin...";
                addActivityButton.Content = "Yeni Aktiviteyi Ekleyin";
                earning.Content = "Kazanç";
                expense.Content = "Masraf";
                language = "tr";
            }
                
        }
            private void AddActivity(object sender, RoutedEventArgs e)
        {
            int times = 0;
            connection.Open();
            command = new SqlCommand("Select * from Activity", connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                times += 1;
            }
            if (Control())
            {
                if (times < 10)
                {
                    int newAmount;
                    if (earning.IsChecked == true)
                        newAmount = Int16.Parse(amount.Text);
                    else
                        newAmount = Int16.Parse("(" + amount.Text + ")", NumberStyles.AllowParentheses);

                    connection = new SqlConnection(connetionString);
                    string sql = null;

                    connection.Open();
                    sql = "INSERT INTO Activity (ActivityName,ActivityMoney,ActivityIcon)VALUES('" + activity.Text + "'," + newAmount + ",'" + iconpath + "')";
                    command = new SqlCommand(sql, connection);
                    command.ExecuteNonQuery();
                    if (language == "en")
                        MessageBox.Show("Adding Complete");
                    else
                        MessageBox.Show("Ekleme Başarılı");
                    connection.Close();
                    salaryBorder.BorderThickness = new Thickness(0);
                    carBorder.BorderThickness = new Thickness(0);
                    shopingBorder.BorderThickness = new Thickness(0);
                    houseBorder.BorderThickness = new Thickness(0);
                    coffeeBorder.BorderThickness = new Thickness(0);
                    burgerBorder.BorderThickness = new Thickness(0);
                    if (language == "en")
                    {
                        activity.Text = "Enter Activity Name ...";
                        amount.Text = "Enter Amount of Activity...";
                    }
                    else if (language == "tr")
                    {
                        activity.Text = "Aktivite Adı Girin";
                        amount.Text = "Aktivitenin Miktarinı Girin...";
                    }
                    iconpath = "";
                }
                else
                {
                    if (language == "en")
                        MessageBox.Show("No more activities can be entered maximum number reached");
                    else
                        MessageBox.Show("Daha fazla aktivite girilemez maksimum sayıya ulaşıldı ");

                }
            }
              
            else
            {
                if (language == "en")
                    MessageBox.Show("Please Enter the Value Correctly and Choose some icon for Activity");
                else
                    MessageBox.Show("Lütfen Değeri Doğru Girin ve Etkinlik için bir simge seçin  ");
            }
           

        }
        private bool Control()
        {
            if (amount.Text == "Aktivitenin Miktarinı Girin..." || amount.Text == "" || iconpath == "" || activity.Text == "" || activity.Text == "Enter Activity Name ..." || amount.Text == "Enter Amount of Activity..." || activity.Text == "Aktivite Adı Girin...")
                return false;
            return true;
        }

    }
}
