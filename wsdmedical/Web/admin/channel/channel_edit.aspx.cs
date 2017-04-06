using System;
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
    public partial class channel_edit : ManagePage
    {
        private string action = DTEnums.ActionEnum.Add.ToString(); //操作类型
        //private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString + HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["DbPath"]);
        //string connectionstring = Common.CommomFunction.MyAccessSqlString().Replace("sdy147258369", "sdy123456");
        private string connectionstring = Common.CommomFunction.SqlServerString();
        private int id = 0;
        protected string quanxian = string.Empty;//权限
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            { 
            string _action = DTRequest.GetQueryString("action");
            quanxian = DTRequest.GetQueryString("qx");
            if (!string.IsNullOrEmpty(_action) && _action == DTEnums.ActionEnum.Edit.ToString())
            {
                action = DTEnums.ActionEnum.Edit.ToString();//修改类型
                id = DTRequest.GetQueryInt("id");
                if (id == 0)
                {
                    JscriptMsg("传输参数不正确！", "back");
                    return;
                }
            }
            if (!Page.IsPostBack)
            {

                if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ChkAdminLevel(id, "Edit"); //检查权限
                    ShowInfo(id);
                }
                else
                {
                    txtName.Attributes.Add("ajaxurl", "../../tools/admin_ajax.ashx?action=channel_name_validate");
                }
            }
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("channel_edit.aspx", "Page_Load", ex.Message);
            }
        }


        private void ShowInfo(int _id)
        {
            channelBll bll = new channelBll();
            Model.channel model = bll.GetModel(_id);
            txtName.Text = model.name;
            txtName.Focus(); //设置焦点，防止JS无法提交
            txtName.Attributes.Add("ajaxurl", "../../tools/admin_ajax.ashx?action=channel_name_validate&old_channel_name=" + Utils.UrlEncode(model.name));
            txtTitle.Text = model.title;
            txtSortId.Text = Convert.ToString(model.sort_id);
            if (model.is_albums == 1)
            {
                cbIsAlbums.Checked = true;
            }
            if (model.is_attach == 1)
            {
                cbIsAttach.Checked = true;
            }
            if (model.is_video == 1)
            {
                cbIsVideo.Checked = true;
            }
            if (model.is_details == 1)
            {
                cbIsDetail.Checked = true;
            }
          
            if (model.is_fylx == 1)
            {
                cbIsFylx.Checked = true;
            }

            if (model.is_city == 1)
            {
                cbIsCity.Checked = true;
            }
            if (model.is_hospl == 1)
            {
                cbIsHospl.Checked = true;
            }
            if (model.is_zyxlzt == 1)
            {
                cbIsZyxlzt.Checked = true;
            }
            if (model.is_paymoney == 1)
            {
                cbIsPaymoney.Checked = true;
            }
            if (model.is_zslx == 1)
            {
                cbIsZslx.Checked = true;
            }
            if (model.is_mdcity == 1)
            {
                cbIsMdcity.Checked = true;
            }
            if (model.is_szjc == 1)
            {
                cbIsSzjc.Checked = true;
            }

            if (model.is_yybzj == 1)
            {
                cbIsYybzj.Checked = true;
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            { 
            if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
            {
                if (!DoEdit(id))
                {
                    JscriptMsg("编辑过程中发生错误！", "");
                    return;
                }
                JscriptMsg("编辑站点成功！", "channel_list.aspx");
            }
            else //添加
            {
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误！", "");
                    return;
                }
                JscriptMsg("添加站点成功！", "channel_list.aspx");
            }
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("channel_edit.aspx", "btnSubmit_Click", ex.Message);
            }
        }
        private bool DoEdit(int _id)
        {
            channelBll bll = new channelBll();
            Model.channel model = bll.GetModel(_id);

            string old_title = model.title;
            model.name = txtName.Text.Trim();
            model.title = txtTitle.Text.Trim();
            if (cbIsAlbums.Checked == true)
            {
                model.is_albums = 1;
            }
            else
            {
                model.is_albums = 0;
            }
            if (cbIsAttach.Checked == true)
            {
                model.is_attach = 1;
            }
            else
            {
                model.is_attach = 0;
            }
            if (cbIsVideo.Checked == true)
            {
                model.is_video = 1;
            }
            else
            {
                model.is_video = 0;
            }
            if (cbIsDetail.Checked == true)
            {
                model.is_details = 1;
            }
            else
            {
                model.is_details = 0;
            }

            if (cbIsFylx.Checked == true)
            {
                model.is_fylx = 1;
            }
            else
            {
                model.is_fylx = 0;
            }
            if (cbIsCity.Checked == true)
            {
                model.is_city = 1;
            }
            else
            {
                model.is_city = 0;
            }
            if (cbIsHospl.Checked == true)
            {
                model.is_hospl = 1;
            }
            else
            {
                model.is_hospl = 0;
            }
            if (cbIsZyxlzt.Checked == true)
            {
                model.is_zyxlzt = 1;
            }
            else
            {
                model.is_zyxlzt = 0;
            }
            if (cbIsPaymoney.Checked == true)
            {
                model.is_paymoney = 1;
            }
            else
            {
                model.is_paymoney = 0;
            }
            if (cbIsZslx.Checked == true)
            {
                model.is_zslx = 1;
            }
            else
            {
                model.is_zslx = 0;
            }
            if (cbIsMdcity.Checked == true)
            {
                model.is_mdcity = 1;
            }
            else
            {
                model.is_mdcity = 0;
            }
            if (cbIsSzjc.Checked == true)
            {
                model.is_szjc = 1;
            }
            else
            {
                model.is_szjc = 0;
            }
            if (cbIsYybzj.Checked == true)
            {
                model.is_yybzj = 1;
            }
            else
            {
                model.is_yybzj = 0;
            }

            model.sort_id = Utils.StrToInt(txtSortId.Text.Trim(), 99);
            bll.Edit(model);
            AddAdminLog("编辑", "修改频道"+old_title+"为"+model.title); //记录日志
            return true;
        }
        private bool DoAdd()
        {
            Model.channel model = new Model.channel();
            channelBll bll = new channelBll();
            model.name = txtName.Text.Trim();
            if (cbIsAlbums.Checked == true)
            {
                model.is_albums = 1;
            }
            if (cbIsAttach.Checked == true)
            {
                model.is_attach = 1;
            }
            if (cbIsVideo.Checked == true)
            {
                model.is_video = 1;
            }
            if (cbIsDetail.Checked == true)
            {
                model.is_details = 1;
            }
           

            if (cbIsFylx.Checked == true)
            {
                model.is_fylx = 1;
            }
            if (cbIsCity.Checked == true)
            {
                model.is_city = 1;
            }
            if (cbIsHospl.Checked == true)
            {
                model.is_hospl = 1;
            }
            if (cbIsZyxlzt.Checked == true)
            {
                model.is_zyxlzt = 1;
            }
            if (cbIsPaymoney.Checked == true)
            {
                model.is_paymoney = 1;
            }
            if (cbIsZslx.Checked == true)
            {
                model.is_zslx = 1;
            }
            if (cbIsMdcity.Checked == true)
            {
                model.is_mdcity = 1;
            }
            if (cbIsSzjc.Checked == true)
            {
                model.is_szjc = 1;
            }
            if (cbIsYybzj.Checked == true)
            {
                model.is_yybzj = 1;
            }
            model.title = txtTitle.Text.Trim();
            model.sort_id = Utils.StrToInt(txtSortId.Text.Trim(), 99);
            bll.Add(model);
            AddAdminLog("新增", "新增频道" + model.title); //记录日志
            return true;
        }


        public int GetPage()
        {
            int tid = 0;
            navigationBll bll = new navigationBll();
            manager_roleBll bllRole = new manager_roleBll();
            tid = bll.GetIdByPage(quanxian);//表示页面对应的id
            return tid;
        }
    }
}