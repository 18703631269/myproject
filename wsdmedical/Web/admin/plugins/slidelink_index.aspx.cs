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
    public partial class slidelink_index : ManagePage
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
                    RptBind("id>0" + CombSqlTxt(keywords, property), "sort_id asc,add_time desc");
                }
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("slidelink_index.aspx", "Page_Load", ex.Message);
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
                    AuthorityPage.QuanXianVis(lbtnSave, "Save", _dt, 1);//保存
                    AuthorityPage.QuanXianVis(lbtnUnLock, "Audit", _dt, 1);//审核
                    AuthorityPage.QuanXianVis(lbtnDelete, "Delete", _dt, 1);//删除
                }
                else
                {
                    navigationBll bll = new navigationBll();
                    manager_roleBll bllRole = new manager_roleBll();
                    int tid = bll.GetIdByPage(pagename);//表示页面对应的id
                    _dt = bllRole.GetAuditById(tid.ToString(), adminModel.role_id);
                    AuthorityPage.QuanXianVis(a_add, "Add", _dt, 0);//新增
                    AuthorityPage.QuanXianVis(lbtnSave, "Save", _dt, 0);//保存
                    AuthorityPage.QuanXianVis(lbtnUnLock, "Audit", _dt, 0);//审核
                    AuthorityPage.QuanXianVis(lbtnDelete, "Delete", _dt, 0);//删除
                }
            }
            catch (Exception ex)
            {
                throw new Exception("--slidelink_index.aspx-->Bindquanxian" + ex.Message, ex);
            }
        }
        protected string CombSqlTxt(string _keywords, string _property)
        {
            StringBuilder builder = new StringBuilder();
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                builder.Append(" and title like '%" + _keywords + "%'");
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
            rptList.DataSource = new slidelinkBll().GetList(pageSize, this.page, _strWhere, _orderby, out this.totalCount);
            DataBind();
            txtPageNum.Text = pageSize.ToString();
            string str = Utils.CombUrlTxt("slidelink_index.aspx", "keywords={0}&page={1}&property={2}", new string[] { keywords, "__id__", property });
            PageContent.InnerHtml = Utils.OutPageList(pageSize, page, totalCount, str, 8);
        }

        #endregion
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Utils.CombUrlTxt("slidelink_index.aspx", "keywords={0}&property={1}", new string[] { txtKeywords.Text, property }));
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("slidelink_index.aspx", "lbtnSearch_Click", ex.Message);
            }
        }

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                slidelinkBll lkBll = new slidelinkBll();
                for (int i = 0; i < this.rptList.Items.Count; i++)
                {
                    int num3;
                    int id = Convert.ToInt32(((HiddenField)this.rptList.Items[i].FindControl("hidId")).Value);
                    if (!int.TryParse(((TextBox)this.rptList.Items[i].FindControl("txtSortId")).Text.Trim(), out num3))
                    {
                        num3 = 0x63;
                    }
                    lkBll.UpdateField(id, "sort_id=" + num3.ToString());
                }
                AddAdminLog("编辑", "修改友情链接插件排序:");
                JscriptMsg("保存排序成功！", Utils.CombUrlTxt("slidelink_index.aspx", "keywords={0}&property={1}", new string[] { keywords, property }));
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("slidelink_index.aspx", "lbtnSave_Click", ex.Message);
            }
        }

        protected void lbtnUnLock_Click(object sender, EventArgs e)
        {
            try
            {
                slidelinkBll lkBll = new slidelinkBll();
                for (int i = 0; i < this.rptList.Items.Count; i++)
                {
                    int id = Convert.ToInt32(((HiddenField)this.rptList.Items[i].FindControl("hidId")).Value);
                    CheckBox box = (CheckBox)this.rptList.Items[i].FindControl("chkId");
                    if (box.Checked)
                    {
                        lkBll.UpdateField(id, "is_lock=1");
                    }
                }
                AddAdminLog("审核", "审核友情链接");
                JscriptMsg("批量审核成功！", Utils.CombUrlTxt("slidelink_index.aspx", "keywords={0}&property={1}", new string[] { keywords, property }));
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("slidelink_index.aspx", "lbtnUnLock_Click", ex.Message);
            }
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int num = 0;
                int num2 = 0;
                slidelinkBll lkBll = new slidelinkBll();
                for (int i = 0; i < this.rptList.Items.Count; i++)
                {
                    int id = Convert.ToInt32(((HiddenField)this.rptList.Items[i].FindControl("hidId")).Value);
                    CheckBox box = (CheckBox)this.rptList.Items[i].FindControl("chkId");
                    if (box.Checked)
                    {
                        if (lkBll.Delete(id))
                        {
                            num++;
                        }
                        else
                        {
                            num2++;
                        }
                    }
                }
                AddAdminLog("删除", string.Concat(new object[] { "删除友情链接成功", num, "条，失败", num2, "条" }));
                JscriptMsg(string.Concat(new object[] { "删除成功", num, "条，失败", num2, "条！" }), Utils.CombUrlTxt("slidelink_index.aspx", "keywords={0}", new string[] { keywords }));
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("slidelink_index.aspx", "lbtnDelete_Click", ex.Message);
            }
        }

        protected void ddlProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //channel_id = 14;
                Response.Redirect(Utils.CombUrlTxt("slidelink_index.aspx", "keywords={0}&property={1}", txtKeywords.Text, ddlProperty.SelectedValue), false);
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("slidelink_index.aspx", "ddlProperty_SelectedIndexChanged", ex.Message);
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
                Response.Redirect(Utils.CombUrlTxt("slidelink_index.aspx", "keywords={0}&property={1}", new string[] { keywords, property }));
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("slidelink_index.aspx", "txtPageNum_TextChanged", ex.Message);
            }
        }
    }
}