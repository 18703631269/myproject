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
    public partial class manager_log : Web.UI.ManagePage
    {
        protected int totalCount;
        protected int page;
        protected static int pageSize;

        Model.manager model = new Model.manager();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                pageSize = GetPageSize(15); //每页数量
                if (!Page.IsPostBack)
                {
                    Bindquanxian();
                    model = GetAdminInfo(); //取得当前管理员信息
                    RptBind("id>0", "add_time desc,id desc");
                }
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("manager_log.aspx", "Page_Load", ex.Message);
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
               
                    AuthorityPage.QuanXianVis(btnDelete, "Delete", _dt, 1);//删除
                }
                else
                {
                    navigationBll bll = new navigationBll();
                    manager_roleBll bllRole = new manager_roleBll();
                    int tid = bll.GetIdByPage(pagename);//表示页面对应的id
                    _dt = bllRole.GetAuditById(tid.ToString(), adminModel.role_id);
                    AuthorityPage.QuanXianVis(btnDelete, "Delete", _dt, 0);//删除
                }
            }
            catch (Exception ex)
            {
                throw new Exception("--article_list.aspx-->Bindquanxian" + ex.Message, ex);
            }
        }
        #region 数据绑定=================================
        private void RptBind(string _strWhere, string _orderby)
        {
            page = DTRequest.GetQueryInt("page", 1);
            BLL.manager_logBll bll = new BLL.manager_logBll();
            this.rptList.DataSource = bll.GetList(pageSize, this.page, _strWhere, _orderby, out this.totalCount);
            this.rptList.DataBind();

            //绑定页码
            txtPageNum.Text = pageSize.ToString();
            string pageUrl = Utils.CombUrlTxt("manager_log.aspx", "page={0}", "__id__");
            PageContent.InnerHtml = Utils.OutPageList(pageSize, page, totalCount, pageUrl, 8);
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
                Response.Redirect("manager_log.aspx", false);
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("manager_log.aspx", "txtPageNum_TextChanged", ex.Message);
            }
        }

        //批量删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                BLL.manager_logBll bll = new BLL.manager_logBll();
                int sucCount = bll.Delete(7);
                AddAdminLog("删除", "删除管理日志" + sucCount + "条"); //记录日志
                JscriptMsg("删除日志" + sucCount + "条", "manager_log.aspx");
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("manager_log.aspx", "btnDelete_Click", ex.Message);
            }
        }
    }
}