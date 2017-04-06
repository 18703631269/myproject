using BLL;
using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.UI;

namespace Web.admin.article
{
    public partial class comment_list : ManagePage
    {
        protected int channel_id;
        protected int totalCount;
        protected int page;
        protected static int pageSize;

        protected string channel_name = string.Empty; //频道名称
        protected string property = string.Empty;
        protected string keywords = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            channel_id = DTRequest.GetQueryInt("channel_id");
            channel_name = new BLL.channelBll().GetChannelName(channel_id); //取得频道名称
            property = DTRequest.GetQueryString("property");
            keywords = DTRequest.GetQueryString("keywords");
            if (channel_id == 0)
            {
                JscriptMsg("频道参数不正确！", "back");
                return;
            }

            pageSize = GetPageSize(10); //每页数量
            if (!Page.IsPostBack)
            {
                Bindquanxian();
                RptBind("channel_id=" + channel_id + CombSqlTxt(keywords, property), "add_time desc,id desc");
            }
        }
        public void Bindquanxian()
        {
            try
            {
                Model.manager adminModel = new ManagePage().GetAdminInfo(); //获得当前登录管理员信息
                if (adminModel == null)
                {
                    return;
                }
                string pagename = Request.Params["qx"];
                DataTable _dt = new DataTable();
                if (adminModel.role_type == 1)//表示是超级管理员
                {
                    AuthorityPage.QuanXianVis(btnAudit, "Audit", _dt, 1);//审核
                    AuthorityPage.QuanXianVis(btnDelete, "Delete", _dt, 1);//删除
                }
                else
                {
                    navigationBll bll = new navigationBll();
                    manager_roleBll bllRole = new manager_roleBll();
                    int tid = bll.GetIdByPage(pagename);//表示页面对应的id
                    _dt = bllRole.GetAuditById(tid.ToString(), adminModel.role_id);
                    AuthorityPage.QuanXianVis(btnAudit, "Audit", _dt, 0);//审核
                    AuthorityPage.QuanXianVis(btnDelete, "Delete", _dt, 0);//删除
                }
            }
            catch (Exception ex)
            {
                throw new Exception("--article_list.aspx-->Bindquanxian" + ex.Message, ex);
            }
        }
        #region 组合SQL查询语句==========================
        protected string CombSqlTxt(string _keywords, string _property)
        {
            StringBuilder strTemp = new StringBuilder();
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append(" and (user_name like '%" + _keywords + "%' or content like '%" + _keywords + "%')");
            }
            if (!string.IsNullOrEmpty(_property))
            {
                switch (_property)
                {
                    case "isLock":
                        strTemp.Append(" and is_lock=0");
                        break;
                    case "unLock":
                        strTemp.Append(" and is_lock=1");
                        break;
                }
            }
            return strTemp.ToString();
        }
        #endregion

        #region 数据绑定=================================
        private void RptBind(string _strWhere, string _orderby)
        {
            page = DTRequest.GetQueryInt("page", 1);
            ddlProperty.SelectedValue = property;
            txtKeywords.Text = keywords;
            article_commentBll bll = new article_commentBll();
            rptList.DataSource = bll.GetList(pageSize, page, _strWhere, _orderby, out totalCount);
            rptList.DataBind();

            //绑定页码
            txtPageNum.Text = pageSize.ToString();
            string pageUrl = Utils.CombUrlTxt("comment_list.aspx", "channel_id={0}&keywords={1}&property={2}&page={3}",
                channel_id.ToString(), keywords, property, "__id__");
            PageContent.InnerHtml = Utils.OutPageList(pageSize, page, totalCount, pageUrl, 8);
        }
        #endregion

        #region 返回评论每页数量
        private int GetPageSize(int _default_size)
        {
            int _pagesize = pageSize;//获取设置的新的分页个数
            if (_pagesize > 0)
            {
                return _pagesize;
            }
            return _default_size;
        }
        #endregion
        protected void ddlProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("comment_list.aspx", "channel_id={0}&keywords={1}&property={2}",
             channel_id.ToString(), keywords, ddlProperty.SelectedValue));
        }

        protected void btnAudit_Click(object sender, EventArgs e)
        {
            article_commentBll bll = new article_commentBll();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    bll.UpdateField(id, "is_lock=1");
                }
            }
            AddAdminLog("审核", "审核" + channel_name + "频道评论信息"); //记录日志
            JscriptMsg("审核通过成功！", Utils.CombUrlTxt("comment_list.aspx", "channel_id={0}&keywords={1}&property={2}",
                channel_id.ToString(), keywords, property));
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int sucCount = 0;
            int errorCount = 0;
            article_commentBll bll = new article_commentBll();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    if (bll.Delete(id))
                    {
                        sucCount += 1;
                    }
                    else
                    {
                        errorCount += 1;
                    }
                }
            }
            AddAdminLog("删除", "删除" + this.channel_name + "频道评论成功" + sucCount + "条，失败" + errorCount + "条"); //记录日志
            JscriptMsg("删除成功" + sucCount + "条，失败" + errorCount + "条！",
                Utils.CombUrlTxt("comment_list.aspx", "channel_id={0}&keywords={1}&property={2}", channel_id.ToString(), keywords, property));
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("comment_list.aspx", "channel_id={0}&keywords={1}&property={2}",
               channel_id.ToString(), txtKeywords.Text, property));
        }

        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int _pagesize;
            if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
            {
                pageSize = _pagesize;
            }
            Response.Redirect(Utils.CombUrlTxt("comment_list.aspx", "channel_id={0}&keywords={1}&property={2}",
            channel_id.ToString(), keywords, property));
        }
    }
}