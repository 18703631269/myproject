using BLL;
using Common;
using System;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;
using Web.UI;

namespace Web.admin.order
{
    public partial class user_list : ManagePage
    {
        protected int totalCount;
        protected int page;
        protected static int pageSize;
        protected string property = string.Empty;
        protected string keywords = string.Empty;
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
                    RptBind(" " + CombSqlTxt(keywords, property), "udate desc");
                }
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("user_list.aspx", "Page_Load", ex.Message);
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
                    AuthorityPage.QuanXianVis(a_add, "Add", _dt, 1);//新增
                 //   AuthorityPage.QuanXianVis(lbtnSave, "Save", _dt, 1);//保存
                    
                    AuthorityPage.QuanXianVis(lbtnDelete, "Delete", _dt, 1);//删除
                }
                else
                {
                    navigationBll bll = new navigationBll();
                    manager_roleBll bllRole = new manager_roleBll();
                    int tid = bll.GetIdByPage(pagename);//表示页面对应的id
                    _dt = bllRole.GetAuditById(tid.ToString(), adminModel.role_id);
                    AuthorityPage.QuanXianVis(a_add, "Add", _dt, 0);//新增
                   // AuthorityPage.QuanXianVis(lbtnSave, "Save", _dt, 0);//保存
                   
                    AuthorityPage.QuanXianVis(lbtnDelete, "Delete", _dt, 0);//删除
                }
            }
            catch (Exception ex)
            {
                throw new Exception("--user_list.aspx-->Bindquanxian" + ex.Message, ex);
            }
        }
        protected string CombSqlTxt(string _keywords, string _property)
        {
            StringBuilder builder = new StringBuilder();
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                builder.Append(" uname like '%" + _keywords + "%'");
            }
            if (!string.IsNullOrEmpty(_property))
            {
                switch (_property)
                {
                    case "1":
                        builder.Append(" ulock='0'");
                        break;
                    case "2":
                        builder.Append(" ulock='1'");
                        break;
                }
            }
            return builder.ToString();
        }


        #region 返回图文每页数量=========================
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

        #region 数据绑定=================================
        private void RptBind(string _strWhere, string _orderby)
        {
            page = DTRequest.GetQueryInt("page", 1);
            ddlProperty.SelectedValue = property;
            txtKeywords.Text = keywords;
            rptList.DataSource = new t_userBll().GetList(pageSize, this.page, _strWhere, _orderby, out this.totalCount);
            DataBind();
            txtPageNum.Text = pageSize.ToString();
            string str = Utils.CombUrlTxt("user_list.aspx", "keywords={1}&page={2}&property={3}", new string[] { keywords, "__id__", property });
            PageContent.InnerHtml = Utils.OutPageList(pageSize, page, totalCount, str, 8);
        }

        #endregion

        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int _pagesize = 0;
                if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
                {
                    pageSize = _pagesize;
                }
                Response.Redirect(Utils.CombUrlTxt("user_list.aspx", "keywords={0}&property={1}", new string[] { keywords, property }));
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("user_list.aspx", "txtPageNum_TextChanged", ex.Message);
            }
        }


        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int num = 0;
                int num2 = 0;
                t_userBll userBll = new t_userBll();
                for (int i = 0; i < this.rptList.Items.Count; i++)
                {
                    string id = Convert.ToString(((HiddenField)this.rptList.Items[i].FindControl("hidId")).Value);
                    CheckBox box = (CheckBox)this.rptList.Items[i].FindControl("chkId");
                    if (box.Checked)
                    {
                        if (userBll.Delete(id))
                        {
                            num++;
                        }
                        else
                        {
                            num2++;
                        }
                    }
                }
                AddAdminLog("删除", string.Concat(new object[] { "删除类别成功", num, "条，失败", num2, "条" }));
                JscriptMsg(string.Concat(new object[] { "删除成功", num, "条，失败", num2, "条！" }), Utils.CombUrlTxt("user_list.aspx", "keywords={0}", new string[] { keywords }));
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("user_list.aspx", "lbtnDelete_Click", ex.Message);
            }
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Utils.CombUrlTxt("user_list.aspx", "keywords={0}&property={1}", new string[] { txtKeywords.Text, property }));
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("user_list.aspx", "lbtnSearch_Click", ex.Message);
            }
        }

        protected void ddlProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //channel_id = 14;
                Response.Redirect(Utils.CombUrlTxt("user_list.aspx", "keywords={0}&property={1}", txtKeywords.Text, ddlProperty.SelectedValue), false);
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("user_list.aspx", "ddlProperty_SelectedIndexChanged", ex.Message);
            }
        }
    }
}