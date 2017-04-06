using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using Web.UI;
using BLL;

namespace Web.admin.manager
{
    public partial class pmenu_list : Web.UI.ManagePage
    {
        protected string keywords = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.keywords = DTRequest.GetQueryString("keywords");
                if (!Page.IsPostBack)
                {
                    Bindquanxian();
                    Model.manager model = GetAdminInfo(); //取得当前管理员信息
                    RptBind("role_type>=" + model.role_type + CombSqlTxt(this.keywords));
                }
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("role_list.aspx", "Page_Load", ex.Message);
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
        #region 数据绑定=================================
        private void RptBind(string _strWhere)
        {
            this.txtKeywords.Text = this.keywords;
            BLL.manager_roleBll bll = new BLL.manager_roleBll();
            this.rptList.DataSource = bll.GetList(_strWhere);
            this.rptList.DataBind();

        }
        #endregion

        #region 组合SQL查询语句==========================
        protected string CombSqlTxt(string _keywords)
        {
            StringBuilder strTemp = new StringBuilder();
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append(" and role_name like '%" + _keywords + "%'");
            }

            return strTemp.ToString();
        }
        #endregion

        #region 返回角色类型名称=========================
        protected string GetTypeName(int role_type)
        {
            string str = "";
            switch (role_type)
            {
                case 1:
                    str = "超级用户";
                    break;
                default:
                    str = "系统用户";
                    break;
            }
            return str;
        }
        #endregion

        //查询操作
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Utils.CombUrlTxt("role_list.aspx", "keywords={0}", txtKeywords.Text.Trim()),false);
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("role_list.aspx", "btnSearch_Click", ex.Message);
            }
        }

        //批量删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int sucCount = 0; //成功数量
                int errorCount = 0; //失败数量
                BLL.manager_roleBll bll = new BLL.manager_roleBll();
                for (int i = 0; i < rptList.Items.Count; i++)
                {
                    int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                    CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                    if (cb.Checked)
                    {
                        if (bll.Delete(id))
                        {
                            sucCount++;
                        }
                        else
                        {
                            errorCount++;
                        }
                    }
                }
                AddAdminLog("删除", "删除管理角色" + sucCount + "条，失败" + errorCount + "条"); //记录日志
                JscriptMsg("删除成功" + sucCount + "条，失败" + errorCount + "条！", Utils.CombUrlTxt("role_list.aspx", "keywords={0}", this.keywords));
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("role_list.aspx", "btnDelete_Click", ex.Message);
            }
        }

    }
}