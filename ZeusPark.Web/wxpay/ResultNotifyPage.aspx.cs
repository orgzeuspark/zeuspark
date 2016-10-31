using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace WxPayAPI
{
    public partial class ResultNotifyPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Log.Info("ResultNotifyPage", "receive message");
                ResultNotify resultNotify = new ResultNotify(this);
                resultNotify.ProcessNotify();
            }
            catch(Exception ex)
            {
                Log.Error("ResultNotifyPage", ex.Message + "\r\n" + ex.StackTrace);
            }

        }       
    }
}