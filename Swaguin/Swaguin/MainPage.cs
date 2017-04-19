using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Swaguin
{
    public class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            var listPage = new ContactListPage();
            Master = new NavigationPage(listPage) { Title = listPage.Title, Icon = listPage.Icon };
            Detail = new NavigationPage(new ContactListPage());
        }
    }
}
