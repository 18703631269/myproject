using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.UI;

namespace Web.admin.channel
{
    public partial class ChannelToNavigation : ManagePage
    {
        protected int channel_id;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    channel_id = DTRequest.GetQueryInt("channel_id");
                    if (channel_id == 0)
                    {
                        JscriptMsg("频道参数不正确！", "back");
                        return;
                    }
                    RptBind();
                }
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("ChannelToNavigation.aspx", "Page_Load", ex.Message);
            }
        }

        //数据绑定
        private void RptBind()
        {
            BLL.navigationBll bll = new BLL.navigationBll();
            DataTable dt = bll.GetListByChannelId(channel_id);
            this.rptList.DataSource = dt;
            this.rptList.DataBind();
        }
    }
}