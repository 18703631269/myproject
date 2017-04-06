using BLL;
using Common;
using Model;
using System;
using System.Web.UI;
using Web.UI;

namespace Web.admin.plugins
{
    public partial class type_edit : ManagePage
    {
        private string action = DTEnums.ActionEnum.Add.ToString(); //操作类型
        int id = 0;
        protected string quanxian = string.Empty;//权限
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                quanxian = DTRequest.GetQueryString("qx");
                action = DTRequest.GetQueryString("action");
                htype.Value = DTRequest.GetQueryString("type");
                if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    id = DTRequest.GetQueryInt("id");
                    if (id == 0)
                    {
                        JscriptMsg("传输参数不正确！", "back");
                    }
                    if (!new typeBll().Exists(id))
                    {
                        base.JscriptMsg("内容不存在或已被删除！", "back");
                        return;
                    }
                }
                if (!Page.IsPostBack)
                {
                    if (string.Equals(htype.Value, "city") || string.Equals(htype.Value, "hkgs") || string.Equals(htype.Value, "yy")) 
                    {
                        cis.Visible = true;
                        BindD();
                    }

                    if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
                    {
                        ChkAdminLevel(id, "Edit"); //检查权限
                        ShowInfo(id);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("type_edit.aspx?action=" + htype.Value, "Page_Load", ex.Message);
            }
        }

        /// <summary>
        /// 绑定城市
        /// </summary>
        protected void BindD() 
        {
            sys_configBll sysBll=new sys_configBll();
            string strModel = sysBll.ReadConfig();
            ddlCity.DataSource = strModel.Split(';');
            ddlCity.DataBind();
        }


        private void ShowInfo(int _id)
        {
            try
            {
                type model = new typeBll().GetModel(_id);

                txtTitle.Text = model.tname;
                ddlCity.SelectedValue = model.ctype;
                rblIsLock.SelectedValue = model.is_lock.ToString();
                txtSortId.Text = model.sort_id.ToString();
               
            }
            catch (Exception ex)
            {
                throw new Exception("--type_edit-->ShowInfo" + ex.Message, ex);
            }
        }



        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    if (!DoEdit(this.id))
                    {
                        JscriptMsg("保存过程中发生错误！", string.Empty);
                    }
                    else
                    {
                        JscriptMsg("修改类别成功！", "type_index.aspx?action=" + htype.Value);
                    }
                }
                else
                {
                    if (!DoAdd())
                    {
                        JscriptMsg("保存过程中发生错误！", string.Empty);
                    }
                    else
                    {
                        JscriptMsg("添加信息成功！", "type_index.aspx?action=" + htype.Value);
                    }

                }
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("type_edit.aspx?action=" + htype.Value, "btnSubmit_Click", ex.Message);
            }
        }

        private bool DoAdd()
        {
            try
            {
                Model.manager usermodel = Session["userinfo"] as Model.manager;
                bool flag = false;
                type model = new type();
                typeBll lkBll = new typeBll();
                model.tname = this.txtTitle.Text.Trim();
                model.is_lock = Utils.StrToInt(this.rblIsLock.SelectedValue, 0);

                model.sort_id = Utils.StrToInt(this.txtSortId.Text.Trim(), 0x63);
                model.town = htype.Value;
                model.ctype = ddlCity.SelectedValue;
                model.add_time = Utils.StrToDateTime(DateTime.Now.ToString());

                model.add_user = usermodel.id;
                if (lkBll.Add(model) > 0)
                {
                    AddAdminLog("新增", "添加类别：" + model.tname);
                    flag = true;
                }
                return flag;
            }
            catch (Exception ex)
            {
                throw new Exception("--type_edit-->DoAdd" + ex.Message, ex);
            }
        }


        private bool DoEdit(int _id)
        {
            try
            {
                Model.manager usermodel = Session["userinfo"] as Model.manager;
                bool flag = false;
                typeBll lkBll = new typeBll();
                type model = lkBll.GetModel(_id);
                model.tname = this.txtTitle.Text.Trim();
                model.is_lock = Utils.StrToInt(this.rblIsLock.SelectedValue, 0);
                model.sort_id = Utils.StrToInt(this.txtSortId.Text.Trim(), 0x63);
                model.ctype = ddlCity.SelectedValue;
                model.add_user = usermodel.id;
                model.add_time = Utils.StrToDateTime(DateTime.Now.ToString());
                if (lkBll.Update(model))
                {
                    AddAdminLog("编辑", "修改类别信息：" + model.tname);
                    flag = true;
                }
                return flag;
            }
            catch (Exception ex)
            {
                throw new Exception("--type_edit-->DoEdit" + ex.Message, ex);
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