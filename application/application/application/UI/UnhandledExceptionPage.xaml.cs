using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using application.SystemInterface;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace application.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UnhandledExceptionPage
    {
        public UnhandledExceptionPage(Exception exception, RequestCreator requestCreator) : base(requestCreator)
        {
            InitializeComponent();
            MessageLabel.Text = exception.Message;
        }
    }
}