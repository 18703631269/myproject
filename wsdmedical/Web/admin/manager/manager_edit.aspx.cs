using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using BLL;

namespace Web.admin.manager
{
    public partial class manager_edit : Web.UI.ManagePage
    {
        string defaultpassword = "0|0|0|0"; //默认显示密码
        private string action = DTEnums.ActionEnum.Add.ToString(); //操作类型
        private int id = 0;
        protected string quanxian = string.Empty;//权限
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                quanxian = DTRequest.GetQueryString("qx");
            string _action = DTRequest.GetQueryString("action");
            if (!string.IsNullOrEmpty(_action) && _action == DTEnums.ActionEnum.Edit.ToString())
            {
                this.action = DTEnums.ActionEnum.Edit.ToString();//修改类型
                if (!int.TryParse(Request.QueryString["id"] as string, out this.id))
                {
                    JscriptMsg("传输参数不正确！", "back");
                    return;
                }
                if (!new BLL.managerBll().Exists(this.id))
                {
                    JscriptMsg("记录不存在或已被删除！", "back");
                    return;
                }
            }
            if (!Page.IsPostBack)
            {
                Model.manager model = GetAdminInfo(); //取得管理员信息
                RoleBind(ddlRoleId, model.role_type);
                if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ChkAdminLevel(id, "Edit"); //检查权限

                    ShowInfo(this.id);
                }
            }
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("manager_edit.aspx", "Page_Load", ex.Message);
            }
        }

        #region 角色类型=================================
        private void RoleBind(DropDownList ddl, int role_type)
        {
            BLL.manager_roleBll bll = new BLL.manager_roleBll();
            DataTable dt = bll.GetList("");

            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("请选择角色...", ""));
            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToInt32(dr["role_type"]) >= role_type)
                {
                    ddl.Items.Add(new ListItem(dr["role_name"].ToString(), dr["id"].ToString()));
                }
            }
        }
        #endregion

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            BLL.managerBll bll = new BLL.managerBll();
            Model.manager model = bll.GetModel(_id);
            ddlRoleId.SelectedValue = model.role_id.ToString();
            if (model.is_lock == 1)
            {
                cbIsLock.Checked = true;
            }
            else
            {
                cbIsLock.Checked = false;
            }
            txtUserName.Text = model.user_name;
            txtUserName.ReadOnly = true;
            txtUserName.Attributes.Remove("ajaxurl");
            if (!string.IsNullOrEmpty(model.password))
            {
                txtPassword.Attributes["value"] = txtPassword1.Attributes["value"] = defaultpassword;
            }
            txtRealName.Text = model.real_name;
            txtTelephone.Text = model.telephone;
            txtEmail.Text = model.email;
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
           
            Model.manager model = new Model.manager();
            BLL.managerBll bll = new BLL.managerBll();
            model.role_id = int.Parse(ddlRoleId.SelectedValue);
            model.role_type = new BLL.manager_roleBll().GetModel(model.role_id).role_type;
            if (cbIsLock.Checked == true)
            {
                model.is_lock = 1;
            }
            else
            {
                model.is_lock = 0;
            }
            //检测用户名是否重复
            if (bll.Exists(txtUserName.Text.Trim()))
            {
                return false;
            }
            model.user_name = txtUserName.Text.Trim();
            //获得6位的salt加密字符串
            model.salt = Utils.GetCheckCode(6);
            //以随机生成的6位字符串做为密钥加密
            model.password = DESEncrypt.Encrypt(txtPassword.Text.Trim(), model.salt);
            model.real_name = txtRealName.Text.Trim();
            model.telephone = txtTelephone.Text.Trim();
            model.email = txtEmail.Text.Trim();
            model.add_time = DateTime.Now;

            if (bll.Add(model) > 0)
            {
                AddAdminLog("添加", "添加管理员:" + model.user_name); //记录日志
                return true;
            }
            return false;
           
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            bool result = false;
            BLL.managerBll bll = new BLL.managerBll();
            Model.manager model = bll.GetModel(_id);

            model.role_id = int.Parse(ddlRoleId.SelectedValue);
            model.role_type = new BLL.manager_roleBll().GetModel(model.role_id).role_type;
            if (cbIsLock.Checked == true)
            {
                model.is_lock = 1;
            }
            else
            {
                model.is_lock = 0;
            }
            //判断密码是否更改
            if (txtPassword.Text.Trim() != defaultpassword)
            {
                //获取用户已生成的salt作为密钥加密
                model.password = DESEncrypt.Encrypt(txtPassword.Text.Trim(), model.salt);
            }
            model.real_name = txtRealName.Text.Trim();
            model.telephone = txtTelephone.Text.Trim();
            model.email = txtEmail.Text.Trim();

            if (bll.Update(model))
            {
                AddAdminLog("编辑", "修改管理员:" + model.user_name); //记录日志
                result = true;
            }

            return result;
        }
        #endregion

        //保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    if (!DoEdit(this.id))
                    {
                        JscriptMsg("保存过程中发生错误！", "");
                        return;
                    }
                    JscriptMsg("修改管理员信息成功！", "manager_list.aspx");
                }
                else //添加
                {
                    if (!DoAdd())
                    {
                        JscriptMsg("保存过程中发生错误！", "");
                        return;
                    }
                    JscriptMsg("添加管理员信息成功！", "manager_list.aspx");
                }
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("manager_edit.aspx", "btnSubmit_Click", ex.Message);
            }
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