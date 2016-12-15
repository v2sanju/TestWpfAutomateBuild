using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
using WpfAppViewModel;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    |||
        Stopwatch watch = new Stopwatch();

        MainViewModel vm;
        DataTable dtCountry;
        DataTable dtState;
        DataTable dtCity;

        DataTable dtUGEducation;
        DataTable dtGraduationEducation;
        DataTable dtPostGraduationEducation;
        DataTable dtCertification;
        DataTable dtBankList;
        DataTable dtBankBranchList;

        public MainWindow()
        {        
            InitializeComponent();

            vm = new MainViewModel();           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("This is clicked");
            if (cb_Country.SelectedValue != null)
                MessageBox.Show(cb_Country.SelectedValue.ToString());
            else
                MessageBox.Show("Select from drop-downs");
        }

       
       
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            watch.Start();

            //Country
            dtCountry = vm.GetCountryData();
            cb_Country_Load();
            //State
            dtState = vm.GetStateData();
            cb_State_Load();
            //City
            dtCity = vm.GetCityData();
            cb_City_Load();

            dtUGEducation = vm.GetUGEducationData();
            dtGraduationEducation = vm.GetGraduationEducationData();
            dtPostGraduationEducation = vm.GetPostGraduationEducationData();
            dtCertification = vm.GetCertificationData();
            dtBankList = vm.GetBankListData();
            dtBankBranchList = vm.GetBankBranchData(null);

            watch.Stop();
            lbl_Status.Content = "Window loaded took time Milliseconds: " + watch.ElapsedMilliseconds.ToString();
        }

       
        private void cb_Country_Load()
        {           
            // ... Assign the ItemsSource to the List.
            cb_Country.ItemsSource = dtCountry.DefaultView;
            cb_Country.DisplayMemberPath = dtCountry.Columns[1].ToString();
            cb_Country.SelectedValuePath = dtCountry.Columns[0].ToString();

            // ... Make the first item selected.
            cb_Country.SelectedIndex = 0;
        }

        private void cb_State_Load()
        {          
            // ... Assign the ItemsSource to the List.
            cb_State.ItemsSource = dtState.DefaultView;
            cb_State.DisplayMemberPath = dtState.Columns[1].ToString();
            cb_State.SelectedValuePath = dtState.Columns[0].ToString();

            // ... Make the first item selected.
            cb_State.SelectedIndex = 0;
        }


        private void cb_City_Load()
        {
            // ... Assign the ItemsSource to the List.
            cb_City.ItemsSource = dtCity.DefaultView;
            cb_City.DisplayMemberPath = dtCity.Columns[1].ToString();
            cb_City.SelectedValuePath = dtCity.Columns[0].ToString();

            // ... Make the first item selected.
            cb_City.SelectedIndex = 0;
        }

        private void cb_UGEducation_Load()
        {
            // ... Assign the ItemsSource to the List.
            cb_UGEducation.ItemsSource = dtUGEducation.DefaultView;
            cb_UGEducation.DisplayMemberPath = dtUGEducation.Columns[1].ToString();
            cb_UGEducation.SelectedValuePath = dtUGEducation.Columns[0].ToString();

            // ... Make the first item selected.
            cb_UGEducation.SelectedIndex = 0;
        }

        private void cb_GraduationEducation_Load()
        {
            // ... Assign the ItemsSource to the List.
            cb_GraduationEducation.ItemsSource = dtGraduationEducation.DefaultView;
            cb_GraduationEducation.DisplayMemberPath = dtGraduationEducation.Columns[1].ToString();
            cb_GraduationEducation.SelectedValuePath = dtGraduationEducation.Columns[0].ToString();

            // ... Make the first item selected.
            cb_GraduationEducation.SelectedIndex = 0;
        }

        private void cb_PostGraduationEducation_Load()
        {
            // ... Assign the ItemsSource to the List.
            cb_PostGraduationEducation.ItemsSource = dtPostGraduationEducation.DefaultView;
            cb_PostGraduationEducation.DisplayMemberPath = dtPostGraduationEducation.Columns[1].ToString();
            cb_PostGraduationEducation.SelectedValuePath = dtPostGraduationEducation.Columns[0].ToString();

            // ... Make the first item selected.
            cb_PostGraduationEducation.SelectedIndex = 0;
        }

        private void cb_Certification_Load()
        {
            // ... Assign the ItemsSource to the List.
            cb_Certification.ItemsSource = dtCertification.DefaultView;
            cb_Certification.DisplayMemberPath = dtCertification.Columns[1].ToString();
            cb_Certification.SelectedValuePath = dtCertification.Columns[0].ToString();

            // ... Make the first item selected.
            cb_Certification.SelectedIndex = 0;
        }

        private void cb_BankName_Load()
        {
            // ... Assign the ItemsSource to the List.
            cb_BankName.ItemsSource = dtBankList.DefaultView;
            cb_BankName.DisplayMemberPath = dtBankList.Columns[1].ToString();
            cb_BankName.SelectedValuePath = dtBankList.Columns[0].ToString();

            // ... Make the first item selected.
            cb_BankName.SelectedIndex = 0;
        }

        private void cb_BranchName_Load()
        {
            // ... Assign the ItemsSource to the List.
            cb_BranchName.ItemsSource = dtBankBranchList.DefaultView;
            cb_BranchName.DisplayMemberPath = dtBankBranchList.Columns[1].ToString();
            cb_BranchName.SelectedValuePath = dtBankBranchList.Columns[0].ToString();

            // ... Make the first item selected.
            cb_BranchName.SelectedIndex = 0;
        }

        private void cb_BankName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dtBankBranchList = vm.GetBankBranchData(Convert.ToInt32(cb_BankName.SelectedValue.ToString()));
            cb_BranchName_Load();
        }

      
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // ... Get TabControl reference.
            var item = sender as TabControl;
            // ... Set Title to selected tab header.
            var selected = item.SelectedItem as TabItem;
            string strTabHeader = selected.Header.ToString();
            
            //For Education tab
            if (e.Source.GetType().ToString().Equals("System.Windows.Controls.TabControl") && strTabHeader.Equals("Education Details"))
            {
                watch.Start();

                cb_UGEducation_Load();
                cb_GraduationEducation_Load();
                cb_PostGraduationEducation_Load();
                cb_Certification_Load();

                watch.Stop();
               lbl_Status.Content = "Education tab took time Milliseconds: " + watch.ElapsedMilliseconds.ToString();
            }

            //For Bank Details tab
            if (e.Source.GetType().ToString().Equals("System.Windows.Controls.TabControl") && strTabHeader.Equals("Bank Details"))
            {
                watch.Start();

                cb_BankName_Load();
                cb_BranchName_Load();

                watch.Stop();
                lbl_Status.Content = "Bank Details tab took time Milliseconds: " + watch.ElapsedMilliseconds.ToString();
            }
        }

       
    }
}
