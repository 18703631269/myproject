using BLL;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.UI;

namespace Web.admin.article
{
    public partial class comment_edit : ManagePage
    {
        private int id = 0;
        private string channel_name = string.Empty; //频道名称
        protected Model.article_comment model = new Model.article_comment();
        protected void Page_Load(object sender, EventArgs e)
        {
            id = DTRequest.GetQueryInt("id");
            if (id == 0)
            {
                JscriptMsg("传输参数不正确！", "back");
                return;
            }
            if (!new article_commentBll().Exists(id))
            {
                JscriptMsg("记录不存在或已删除！", "back");
                return;
            }
            model = new article_commentBll().GetModel(id); //取得评论实体
            channel_name = new channelBll().GetChannelName(model.channel_id); //取得频道名称
            if (!Page.IsPostBack)
            {
                ShowInfo();
            }
        }
        #region 赋值操作
        private void ShowInfo()
        {
            txtReContent.Text = Utils.ToTxt(model.reply_content);
            rblIsLock.SelectedValue = model.is_lock.ToString();
        }
        #endregion
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            article_commentBll bll = new article_commentBll();
            model.is_reply = 1;
            model.reply_content = Utils.ToHtml(txtReContent.Text);
            model.is_lock = int.Parse(rblIsLock.SelectedValue);
            model.reply_time = DateTime.Now;
            bll.Update(model);
            AddAdminLog("回复", "回复" + this.channel_name + "频道评论ID:" + model.id); //记录日志
            JscriptMsg("评论回复成功！", "comment_list.aspx?channel_id=" + model.channel_id);
        }
    }
}