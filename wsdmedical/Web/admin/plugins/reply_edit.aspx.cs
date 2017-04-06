using BLL;
using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.UI;

namespace Web.admin.plugins
{
    public partial class reply_edit : ManagePage
    {
        public reply rly = new reply();
        int id = 0;
        protected string quanxian = string.Empty;//权限
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                quanxian = DTRequest.GetQueryString("qx");
                id = DTRequest.GetQueryInt("id");
                if (id == 0)
                {
                    JscriptMsg("传输参数不正确！", "back");
                }
                else if (!new replayBll().Exists(id))
                {
                    JscriptMsg("信息不存在或已被删除！", "back");
                }
                else if (!this.Page.IsPostBack)
                {
                    ChkAdminLevel(id, "Reply"); //检查权限
                    ShowInfo(this.id);
                }
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("reply_edit.aspx", "Page_Load", ex.Message);
            }

        }
        private void ShowInfo(int _id)
        {
            rly = new replayBll().GetModel(_id);
            txtReContent.Text = Utils.ToTxt(rly.reply_content);
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                replayBll feedback = new replayBll();
                rly = feedback.GetModel(id);
                rly.reply_content = Utils.ToHtml(this.txtReContent.Text);
                rly.reply_time = DateTime.Now;
                feedback.Update(rly);
                AddAdminLog("回复留言", "回复留言内容：" + rly.title);
                JscriptMsg("留言回复成功！", "reply_list.aspx");
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("reply_edit.aspx", "btnSubmit_Click", ex.Message);
            }
        }
        public int GetPage()
        {
            int tid = 0;
            navigationBll bll = new navigationBll();
            manager_roleBll bllRole = new manager_roleBll();
            tid = bll.GetIdByPage(quanxian);//表示页面对应的id
            return tid;
        }
    }
}