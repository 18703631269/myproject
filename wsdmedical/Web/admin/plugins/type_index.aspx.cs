using BLL;
using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Web.UI;

namespace Web.admin.plugins
{
    public partial class type_index : ManagePage
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
                      haction.Value = Request.QueryString["action"];
                      a_add.HRef = "type_edit.aspx?action=Add&type=" + haction.Value;
                      if (string.Equals(haction.Value, "hkgs")) 
                      {
                          litF.Text = "国际机场";
                          litT.Text = "公司信息";
                      }
                      else if (string.Equals(haction.Value, "zslb")) 
                      {
                          litF.Text = "住宿类型";
                          litT.Text = "住宿类别";
                      }
                      else if (string.Equals(haction.Value, "fy"))
                      {
                          litF.Text = "翻译类型";
                          litT.Text = "翻译类别";
                      }
                      else if (string.Equals(haction.Value, "city"))
                      {
                          litF.Text = "城市名称";
                          litT.Text = "城市管理";
                      }
                      else if (string.Equals(haction.Value, "yy"))
                      {
                          litF.Text = "医院管理";
                          litT.Text = "医院名称";
                      }
                      else if (string.Equals(haction.Value, "hbgs"))
                      {
                          litF.Text = "国际航班";
                          litT.Text = "航空公司";
                      }
                    Bindquanxian();
                    RptBind("tid>0 and town='"+haction.Value+"'" + CombSqlTxt(keywords, property), "sort_id asc,add_time desc");
                }
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("type_index.aspx", "Page_Load", ex.Message);
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
                throw new Exception("--article_list.aspx-->Bindquanxian" + ex.Message, ex);
            }
        }
        protected string CombSqlTxt(string _keywords, string _property)
        {
            StringBuilder builder = new StringBuilder();
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                builder.Append(" and tname like '%" + _keywords + "%'");
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
            rptList.DataSource = new typeBll().GetList(pageSize, this.page, _strWhere, _orderby, out this.totalCount);
            DataBind();
            txtPageNum.Text = pageSize.ToString();
            string str = Utils.CombUrlTxt("type_index.aspx", "action={0}&keywords={1}&page={2}&property={3}", new string[] {haction.Value,keywords, "__id__", property });
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
                Response.Redirect(Utils.CombUrlTxt("type_index.aspx", "action={0}&keywords={1}&property={2}", new string[] {haction.Value,keywords, property }));
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("type_index.aspx?action=" + haction.Value, "txtPageNum_TextChanged", ex.Message);
            }
        }


        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int num = 0;
                int num2 = 0;
                typeBll lkBll = new typeBll();
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
                AddAdminLog("删除", string.Concat(new object[] { "删除类别成功", num, "条，失败", num2, "条" }));
                JscriptMsg(string.Concat(new object[] { "删除成功", num, "条，失败", num2, "条！" }), Utils.CombUrlTxt("type_index.aspx", "action={0}&keywords={1}", new string[] { haction.Value,keywords }));
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("type_index.aspx?action=" + haction.Value, "lbtnDelete_Click", ex.Message);
            }
        }

        protected void lbtnUnLock_Click(object sender, EventArgs e)
        {
            try
            {
                typeBll lkBll = new typeBll();
                for (int i = 0; i < this.rptList.Items.Count; i++)
                {
                    int id = Convert.ToInt32(((HiddenField)this.rptList.Items[i].FindControl("hidId")).Value);
                    CheckBox box = (CheckBox)this.rptList.Items[i].FindControl("chkId");
                    if (box.Checked)
                    {
                        lkBll.UpdateField(id, "is_lock=1");
                    }
                }
                AddAdminLog("审核", "审核类别");
                JscriptMsg("批量审核成功！", Utils.CombUrlTxt("type_index.aspx", "action={0}&keywords={1}&property={2}", new string[] {haction.Value,keywords, property }));
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("type_index.aspx?action=" + haction.Value, "lbtnUnLock_Click", ex.Message);
            }
        }

        protected void lbtnSave_Click(object sender, EventArgs e)
        {

            try
            {
                typeBll lkBll = new typeBll();
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
                AddAdminLog("编辑", "修改类别插件排序:");
                JscriptMsg("保存排序成功！", Utils.CombUrlTxt("type_index.aspx", "action={0}&keywords={1}&property={2}", new string[] { haction.Value,keywords, property }));
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("type_index.aspx", "lbtnSave_Click", ex.Message);
            }
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Utils.CombUrlTxt("type_index.aspx", "action={0}&keywords={1}&property={2}", new string[] {haction.Value,txtKeywords.Text, property }));
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("type_index.aspx?action=" + haction.Value, "lbtnSearch_Click", ex.Message);
            }
        }

        protected void ddlProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //channel_id = 14;
                Response.Redirect(Utils.CombUrlTxt("type_index.aspx", "action={0}&keywords={1}&property={2}",haction.Value,txtKeywords.Text, ddlProperty.SelectedValue), false);
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("type_index.aspx?action=" + haction.Value, "ddlProperty_SelectedIndexChanged", ex.Message);
            }
        }

        /// <summary>
        /// 绑定数据后激发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //头部数据
            if (e.Item.ItemType == ListItemType.Header) 
            {
                if (string.Equals(haction.Value, "city") || string.Equals(haction.Value, "hkgs") || string.Equals(haction.Value, "yy"))
                {
                    HtmlTableCell tr = e.Item.FindControl("scity") as HtmlTableCell;
                    tr.Visible = true;
                }
            }
            //中间数据层
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item) 
            {
                if (string.Equals(haction.Value, "city") || string.Equals(haction.Value, "hkgs") || string.Equals(haction.Value, "yy"))
                {
                    HtmlTableCell td = e.Item.FindControl("tdcity") as HtmlTableCell;
                    td.Visible = true;
                }
            }
        }
    }
}