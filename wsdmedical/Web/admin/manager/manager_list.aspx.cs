using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using BLL;
using Web.UI;

namespace Web.admin.manager
{
    public partial class manager_list : Web.UI.ManagePage
    {
        protected int totalCount;
        protected int page;
        protected static int pageSize;
        protected int types = 0;
        protected string keywords = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                keywords = DTRequest.GetQueryString("keywords");

                pageSize = GetPageSize(15); //每页数量
                if (!Page.IsPostBack)
                {
                    Bindquanxian();
                    BindGroup(); //绑定群组
                    Model.manager model = GetAdminInfo(); //取得当前管理员信息
                    if (!string.IsNullOrEmpty(Request.Params["types"]))
                    {
                        types = DTRequest.GetQueryInt("types");
                    }
                    if (types > 0)
                    {
                        RptBind("role_type>=" + model.role_type + " and role_id=" + types + CombSqlTxt(keywords), "add_time asc,id desc");
                    }
                    else
                    {
                        RptBind("role_type>=" + model.role_type + CombSqlTxt(keywords), "add_time asc,id desc");
                    }
                }
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("manager_list.aspx", "Page_Load", ex.Message);
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
                    AuthorityPage.QuanXianVis(btnDelete, "Delete", _dt, 1);//删除
                }
                else
                {
                    navigationBll bll = new navigationBll();
                    manager_roleBll bllRole = new manager_roleBll();
                    int tid = bll.GetIdByPage(pagename);//表示页面对应的id
                    _dt = bllRole.GetAuditById(tid.ToString(), adminModel.role_id);
                    AuthorityPage.QuanXianVis(a_add, "Add", _dt, 0);//新增
                    AuthorityPage.QuanXianVis(btnDelete, "Delete", _dt, 0);//删除
                }
            }
            catch (Exception ex)
            {
                throw new Exception("--article_list.aspx-->Bindquanxian" + ex.Message, ex);
            }
        }
        /// <summary>
        /// 绑定群组
        /// </summary>
        private void BindGroup()
        {
            manager_roleBll bll = new manager_roleBll();
            DataTable dt = bll.GetList("");

            ddl_type.Items.Clear();
            ddl_type.Items.Add(new ListItem("全部", "0"));
            foreach (DataRow dr in dt.Rows)
            {
                string Id = dr["id"].ToString();
                string Name = dr["role_name"].ToString();
                ddl_type.Items.Add(new ListItem(Name, Id));
            }
        }


        #region 数据绑定=================================
        private void RptBind(string _strWhere, string _orderby)
        {
            this.page = DTRequest.GetQueryInt("page", 1);
            txtKeywords.Text = this.keywords;
            ddl_type.SelectedValue = Convert.ToString(types);//绑定下拉框
            BLL.managerBll bll = new BLL.managerBll();
            this.rptList.DataSource = bll.GetList(pageSize, this.page, _strWhere, _orderby, out this.totalCount);
            this.rptList.DataBind();

            //绑定页码
            txtPageNum.Text = pageSize.ToString();
            string pageUrl = Utils.CombUrlTxt("manager_list.aspx", "keywords={0}&types={1}&page={2}", this.keywords,ddl_type.SelectedValue, "__id__");
            PageContent.InnerHtml = Utils.OutPageList(pageSize, this.page, this.totalCount, pageUrl, 8);
        }
        #endregion

        #region 组合SQL查询语句==========================
        protected string CombSqlTxt(string _keywords)
        {
            StringBuilder strTemp = new StringBuilder();
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append(" and (user_name like  '%" + _keywords + "%' or real_name like '%" + _keywords + "%' or email like '%" + _keywords + "%')");
            }

            return strTemp.ToString();
        }
        #endregion

        #region 返回每页数量=============================
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

        //关健字查询
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Utils.CombUrlTxt("manager_list.aspx", "keywords={0}&types={1}", txtKeywords.Text, ddl_type.SelectedValue), false);
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("manager_list.aspx", "btnSearch_Click", ex.Message);
            }
        }

        //设置分页数量
        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int _pagesize = 0;
                if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
                {
                    pageSize = _pagesize;
                }
                Response.Redirect(Utils.CombUrlTxt("manager_list.aspx", "keywords={0}&types={1}",txtKeywords.Text, ddl_type.SelectedValue), false);
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("manager_list.aspx", "txtPageNum_TextChanged", ex.Message);
            }
        }

        //批量删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int sucCount = 0;
                int errorCount = 0;
                BLL.managerBll bll = new BLL.managerBll();
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
                AddAdminLog("删除", "删除管理员" + sucCount + "条，失败" + errorCount + "条"); //记录日志
                JscriptMsg("删除成功" + sucCount + "条，失败" + errorCount + "条！", Utils.CombUrlTxt("manager_list.aspx", "keywords={0}&types={1}",txtKeywords.Text, ddl_type.SelectedValue));
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("manager_list.aspx", "btnDelete_Click", ex.Message);
            }
        }

        protected void ddl_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Utils.CombUrlTxt("manager_list.aspx", "keywords={0}&types={1}",txtKeywords.Text,ddl_type.SelectedValue), false);
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("manager_list.aspx", "ddl_type_SelectedIndexChanged", ex.Message);
            }
        }
    }
}