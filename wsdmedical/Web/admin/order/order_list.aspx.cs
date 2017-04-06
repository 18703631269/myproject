using BLL;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.admin.order
{
    public partial class order_list : Web.UI.ManagePage
    {
        protected int totalCount;
        protected int page;
        protected static int pageSize;

        protected int status;
        protected string keywords = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.status = DTRequest.GetQueryInt("status");
            this.keywords = DTRequest.GetQueryString("keywords");
            pageSize = GetPageSize(10); //每页数量
            if (!Page.IsPostBack)
            {
                RptBind(CombSqlTxt(this.status, this.keywords));
            }
        }
        #region 数据绑定=================================
        private void RptBind(string _strWhere)
        {
            this.page = DTRequest.GetQueryInt("page", 1);
            if (this.status > 0)
            {
                this.ddlStatus.SelectedValue = this.status.ToString();
            }
            txtKeywords.Text = this.keywords;
            dtorderBll bll = new dtorderBll();
            this.rptList.DataSource = bll.GetList(pageSize, this.page, _strWhere, "ouzt asc,ouyy desc ", out this.totalCount);
            this.rptList.DataBind();

            //绑定页码
            txtPageNum.Text = pageSize.ToString();
            string pageUrl = Utils.CombUrlTxt("order_list.aspx", "status={0}&keywords={1}&page={2}",
                this.status.ToString(), this.keywords, "__id__");
            PageContent.InnerHtml = Utils.OutPageList(pageSize, this.page, this.totalCount, pageUrl, 8);
        }
        #endregion
        #region 组合SQL查询语句==========================
        protected string CombSqlTxt(int _status, string _keywords)
        {
            StringBuilder strTemp = new StringBuilder();
            if (_status == 2)//已付款
            {
                strTemp.Append(" and oal='已支付'");
            }
            else if(_status == 1)//待支付
            {
                strTemp.Append(" and oal='未支付'");
            }
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append(" and obh like '%" + _keywords + "%'");
            }
            return strTemp.ToString();
        }
        #endregion
        #region 返回用户每页数量=========================
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

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int sucCount = 0;
            int errorCount = 0;
            BLL.dtorderBll bll = new BLL.dtorderBll();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                string ids = ((HiddenField)rptList.Items[i].FindControl("hidId")).Value;
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    if (bll.Delete(ids))
                    {
                        sucCount += 1;
                    }
                    else
                    {
                        errorCount += 1;
                    }
                }
            }
            AddAdminLog(DTEnums.ActionEnum.Delete.ToString(), "删除订单成功" + sucCount + "条，失败" + errorCount + "条"); //记录日志
            JscriptMsg("删除成功" + sucCount + "条，失败" + errorCount + "条！", Utils.CombUrlTxt("order_list.aspx", "keywords={0}", this.keywords));
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("order_list.aspx", "status={0}&keywords={1}",
              ddlStatus.SelectedValue, this.keywords));
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("order_list.aspx", "status={0}&keywords={1}",
               this.status.ToString(), txtKeywords.Text));
        }

        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int _pagesize = 0;
            if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
            {
                pageSize = _pagesize;
            }
            Response.Redirect(Utils.CombUrlTxt("order_list.aspx", "status={0}&keywords={1}",
                this.status.ToString(), this.keywords));
        }

        protected void ddlPaymentStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("order_list.aspx", "status={0}&keywords={1}",
               this.status.ToString(), this.keywords));
        }
    }
}