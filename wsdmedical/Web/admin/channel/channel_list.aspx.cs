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
using System.Configuration;

namespace Web.admin.channel
{
    public partial class channel_list : ManagePage
    {
        protected int totalCount;
        protected int page;
        protected static int pageSize;

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
                    RptBind("id>0" + CombSqlTxt(keywords), "sort_id asc,id desc");
                }
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("channel_list.aspx", "Page_Load", ex.Message);
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
                    AuthorityPage.QuanXianVis(btnSave, "Save", _dt, 1);//保存
                    AuthorityPage.QuanXianVis(btnDelete, "Delete", _dt, 1);//删除
                }
                else
                {
                    navigationBll bll = new navigationBll();
                    manager_roleBll bllRole = new manager_roleBll();
                    int tid = bll.GetIdByPage(pagename);//表示页面对应的id
                    _dt = bllRole.GetAuditById(tid.ToString(), adminModel.role_id);
                    AuthorityPage.QuanXianVis(a_add, "Add", _dt, 0);//新增
                    AuthorityPage.QuanXianVis(btnSave, "Save", _dt, 0);//保存
                    AuthorityPage.QuanXianVis(btnDelete, "Delete", _dt, 0);//删除
                }
            }
            catch (Exception ex)
            {
                throw new Exception("--article_list.aspx-->Bindquanxian" + ex.Message, ex);
            }
        }


        #region 组合SQL查询语句==========================
        protected string CombSqlTxt(string _keywords)
        {
            StringBuilder strTemp = new StringBuilder();
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append(" and title like '%" + _keywords + "%')");
            }

            return strTemp.ToString();
        }
        #endregion

        #region 数据绑定=================================
        private void RptBind(string _strWhere, string _orderby)
        {
            this.page = DTRequest.GetQueryInt("page", 1);
            txtKeywords.Text = keywords;
            channelBll bll = new channelBll();
            this.rptList.DataSource = bll.GetList(pageSize, this.page, _strWhere, _orderby, out this.totalCount);
            this.rptList.DataBind();

            //绑定页码
            txtPageNum.Text = pageSize.ToString();
            string pageUrl = Utils.CombUrlTxt("channel_list.aspx", "keywords={0}&page={1}", txtKeywords.Text, "__id__");
            PageContent.InnerHtml = Utils.OutPageList(pageSize, this.page, this.totalCount, pageUrl, 8);
        }
        #endregion
        //关健字查询
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Utils.CombUrlTxt("channel_list.aspx", "keywords={0}", txtKeywords.Text), false);
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("channel_list.aspx", "btnSearch_Click", ex.Message);
            }
        }

        //保存排序
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                channelBll bll = new channelBll();
                for (int i = 0; i < rptList.Items.Count; i++)
                {
                    int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                    int sortId;
                    if (!int.TryParse(((TextBox)rptList.Items[i].FindControl("txtSortId")).Text.Trim(), out sortId))
                    {
                        sortId = 99;
                    }
                    bll.UpdateField(id, "sort_id=" + sortId.ToString());
                }
                AddAdminLog("编辑", "频道页面-保存排序"); //记录日志
                JscriptMsg("保存排序成功！", Utils.CombUrlTxt("channel_list.aspx", "keywords={0}", txtKeywords.Text));
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("channel_list.aspx", "btnSave_Click", ex.Message);
            }
        }

        //批量删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int sucCount = 0;
                int errorCount = 0;
                channelBll bll = new channelBll();
                for (int i = 0; i < rptList.Items.Count; i++)
                {
                    int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                    CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                    if (cb.Checked)
                    {
                        //检查该频道下是否还有文章
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
                AddAdminLog("删除", "删除频道列表" + sucCount + "条，失败" + errorCount + "条"); //记录日志
                JscriptMsg("删除成功" + sucCount + "条，失败" + errorCount + "条！",
                    Utils.CombUrlTxt("channel_list.aspx", "keywords={0}", this.keywords));
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("channel_list.aspx", "btnDelete_Click", ex.Message);
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
                Response.Redirect(Utils.CombUrlTxt("channel_list.aspx", "keywords={0}", txtKeywords.Text), false);
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("channel_list.aspx", "txtPageNum_TextChanged", ex.Message);
            }
        }


        private int GetPageSize(int _default_size)
        {
            int _pagesize = pageSize;//获取设置的新的分页个数
            if (_pagesize > 0)
            {
                return _pagesize;
            }
            return _default_size;
        }
    }
}