using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.UI;

namespace Web.admin.order
{
    public partial class user_edit : ManagePage
    {
        protected string action = DTEnums.ActionEnum.Add.ToString(); //操作类型
        string defaultpassword = "0|0|0|0"; //默认显示密码
        protected string id = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            string _action = DTRequest.GetQueryString("action");
            if (!string.IsNullOrEmpty(_action) && _action == DTEnums.ActionEnum.Edit.ToString())
            {
                this.action = DTEnums.ActionEnum.Edit.ToString();//修改类型
                id = DTRequest.GetQueryString("id");
                if (string.IsNullOrEmpty(id))
                {
                    JscriptMsg("传输参数不正确！", "back");
                    return;
                }
                if (!new BLL.t_userBll().Exists(this.id))
                {
                    JscriptMsg("信息不存在或已被删除！", "back");
                    return;
                }
            }
            if (!Page.IsPostBack)
            {
                if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                   ShowInfo(this.id);
                }
            }
        }

        private void ShowInfo(string _id)
        {
            Model.t_user model = new Model.t_user();
            BLL.t_userBll bll = new BLL.t_userBll();
            model=bll.GetModel(_id);
            txtUserName.Text = model.ulog;
            txtReallyName.Text = model.uname;
            if (!string.IsNullOrEmpty(model.upwd))
            {
                txtPassword.Attributes["value"] = txtPassword1.Attributes["value"] = defaultpassword;
            }
            rblStatus.SelectedValue = model.ulock;
            txtMobile.Text = model.utel;
            txtEmail.Text = model.uemail;
            rblSex.SelectedValue = model.usex;
        }
        #region 增加操作=================================
        private bool DoAdd()
        {
            bool result = false;
            Model.t_user model = new Model.t_user();
            BLL.t_userBll bll = new BLL.t_userBll();
            model.ulock = rblStatus.SelectedValue;
            model.usex = rblSex.SelectedValue;
            //检测用户名是否重复
            if (bll.Exists("ulog",txtUserName.Text.Trim()))
            {
                return false;
            }
            model.uname = txtReallyName.Text;
            model.ulog = txtUserName.Text.Trim();
            //以随机生成的6位字符串做为密钥加密
            model.upwd =txtPassword.Text.Trim();
            model.utel = txtMobile.Text.Trim();

            if (bll.Add(model))
            {
                AddAdminLog(DTEnums.ActionEnum.Add.ToString(), "添加用户:" + model.uname); //记录日志
                result = true;
            }
            return result;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(string _id)
        {
            bool result = false;
            BLL.t_userBll bll = new BLL.t_userBll();

            Model.t_user model = bll.GetModel(_id);
            //判断密码是否更改
            if (txtPassword.Text.Trim() != model.upwd)
            {
                model.upwd = txtPassword.Text.Trim();
            }
            model.uname = txtReallyName.Text;
            model.utel = txtMobile.Text.Trim();
            model.ulock = rblStatus.SelectedValue;
            model.usex = rblSex.SelectedValue;
            if (bll.Update(model))
            {
                AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "修改用户信息:" + model.uname); //记录日志
                result = true;
            }
            return result;
        }
        #endregion
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
            {
                txtUserName.Enabled = false;
                if (!DoEdit(id))
                {
                    JscriptMsg("保存过程中发生错误！", "");
                    return;
                }
                JscriptMsg("修改会员成功！", "user_list.aspx");
            }
            else //添加
            {
                if (!DoAdd())
                {
                    JscriptMsg("用户名已存在,或者保存过程中发生错误！", "");
                    return;
                }
                JscriptMsg("添加会员成功！", "user_list.aspx");
            }
        }
    }
}