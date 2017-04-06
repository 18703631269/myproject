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

namespace Web.admin.plugins
{
    public partial class reply_list : ManagePage
    {
        protected int totalCount;
        protected int page;
        protected static int pageSize;
        protected string property = string.Empty;
        protected string keywords = string.Empty;
        protected string prolistview = "Txt";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                property = DTRequest.GetQueryString("property");
                keywords = DTRequest.GetQueryString("keywords");
                pageSize = GetPageSize(10);
                if (!this.Page.IsPostBack)
                {
                    Bindquanxian();
                    RptBind("id>0" + CombSqlTxt(keywords, property), "is_lock desc,add_time desc,id desc");
                }
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("reply_list.aspx", "Page_Load", ex.Message);
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
                    AuthorityPage.QuanXianVis(lbtnUnLock, "Audit", _dt, 1);//审核
                    AuthorityPage.QuanXianVis(lbtnDelete, "Delete", _dt, 1);//删除
                }
                else
                {
                    navigationBll bll = new navigationBll();
                    manager_roleBll bllRole = new manager_roleBll();
                    int tid = bll.GetIdByPage(pagename);//表示页面对应的id
                    _dt = bllRole.GetAuditById(tid.ToString(), adminModel.role_id);
                    AuthorityPage.QuanXianVis(lbtnUnLock, "Audit", _dt, 0);//审核
                    AuthorityPage.QuanXianVis(lbtnDelete, "Delete", _dt, 0);//删除
                }
            }
            catch (Exception ex)
            {
                throw new Exception("--article_list.aspx-->Bindquanxian" + ex.Message, ex);
            }
        }
        protected string CombSqlTxt(string _keywords, string _property)
        {
            StringBuilder builder = new StringBuilder();
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                builder.Append(" and (title like '%" + _keywords + "%' or user_name like '%" + _keywords + "%')");
            }
            if (!string.IsNullOrEmpty(_property))
            {
                switch (_property)
                {
                    case "isLock":
                        builder.Append(" and is_lock=0");
                        break;
                    case "unLock":
                        builder.Append(" and is_lock=1");
                        break;
                }
            }
            return builder.ToString();
        }


        private void RptBind(string _strWhere, string _orderby)
        {
            if (!int.TryParse(base.Request.QueryString["page"], out page))
            {
                page = 1;
            }
            ddlProperty.SelectedValue = property;
            txtKeywords.Text = this.keywords;
            rptList.DataSource = new replayBll().GetList(pageSize, page, _strWhere, _orderby, out totalCount);
            rptList.DataBind();
            txtPageNum.Text = pageSize.ToString();
            string str = Utils.CombUrlTxt("reply_list.aspx", "keywords={0}&page={1}&property={2}", new string[] { keywords, "__id__", property });
            PageContent.InnerHtml = Utils.OutPageList(pageSize, page, totalCount, str, 8);
        }



        public int GetPageSize(int _default_size)
        {
            int _pagesize = pageSize;//获取设置的新的分页个数
            if (_pagesize > 0)
            {
                return _pagesize;
            }
            return _default_size;
        }


        protected void lbtnUnLock_Click(object sender, EventArgs e)
        {
            try
            {
                replayBll feedback = new replayBll();
                for (int i = 0; i < this.rptList.Items.Count; i++)
                {
                    int id = Convert.ToInt32(((HiddenField)this.rptList.Items[i].FindControl("hidId")).Value);
                    CheckBox box = (CheckBox)this.rptList.Items[i].FindControl("chkId");
                    if (box.Checked)
                    {
                        feedback.UpdateField(id, "is_lock=1");
                    }
                }
                AddAdminLog("审核", "审核留言插件内容");
                JscriptMsg("批量审核成功！", Utils.CombUrlTxt("reply_list.aspx", "keywords={0}&property={1}", new string[] { keywords, property }));
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("reply_list.aspx", "lbtnUnLock_Click", ex.Message);
            }
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int num = 0;
                int num2 = 0;
                replayBll feedback = new replayBll();
                for (int i = 0; i < this.rptList.Items.Count; i++)
                {
                    int id = Convert.ToInt32(((HiddenField)this.rptList.Items[i].FindControl("hidId")).Value);
                    CheckBox box = (CheckBox)this.rptList.Items[i].FindControl("chkId");
                    if (box.Checked)
                    {
                        if (feedback.Delete(id))
                        {
                            num++;
                        }
                        else
                        {
                            num2++;
                        }
                    }
                }
                AddAdminLog("删除", string.Concat(new object[] { "删除留言成功", num, "条，失败", num2, "条" }));
                JscriptMsg(string.Concat(new object[] { "删除成功", num, "条，失败", num2, "条！" }), Utils.CombUrlTxt("index.aspx", "keywords={0}&property={1}", new string[] { keywords, property }));
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("reply_list.aspx", "lbtnDelete_Click", ex.Message);
            }
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
            Response.Redirect(Utils.CombUrlTxt("reply_list.aspx", "keywords={0}&property={1}", new string[] { txtKeywords.Text, property }));
           }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("reply_list.aspx", "lbtnSearch_Click", ex.Message);
            }
        }

        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int _pagesize = 0;
                if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
                {
                    pageSize = _pagesize;
                }
                Response.Redirect(Utils.CombUrlTxt("reply_list.aspx", "keywords={0}&property={1}", new string[] { keywords, property }));
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("reply_list.aspx", "txtPageNum_TextChanged", ex.Message);
            }
        }

        protected void ddlProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //channel_id = 14;
                Response.Redirect(Utils.CombUrlTxt("reply_list.aspx", "keywords={0}&property={1}", txtKeywords.Text, ddlProperty.SelectedValue), false);
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("reply_list.aspx", "ddlProperty_SelectedIndexChanged", ex.Message);
            }
        }
    }
}