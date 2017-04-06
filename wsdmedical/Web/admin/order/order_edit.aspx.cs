using BLL;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.admin.order
{
    public partial class order_edit: System.Web.UI.Page
    {
        private string id = string.Empty;
        public Model.dt_order model = new Model.dt_order();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.id = DTRequest.GetQueryString("id");
            if (this.id == "")
            {
                JscriptMsg("传输参数不正确！", "back");
                return;
            }
            if (!new BLL.dtorderBll().Exists(this.id))
            {
                JscriptMsg("记录不存在或已被删除！", "back");
                return;
            }
            if (!Page.IsPostBack)
            {
                ShowInfo(this.id);
            }
        }

        #region 赋值操作=================================
        private void ShowInfo(string _id)
        {
            BLL.dtorderBll bll = new BLL.dtorderBll();
            model = bll.GetModel(_id);
            if (model != null)
            {
                lbUserName.Text = model.ouser;
                litBh.Text = model.obh;
                litZt.Text = model.oal;
                litFwtype.Text=model.otype;
                litUyy.Text = string.Format("{0:G}", model.ouyy);
                litYysj.Text = string.Format("{0:g}", model.oyysj); 
                litYydw.Text=model.oyydw;
                litJjlx.Text= model.ojjlx;
                litMdcy.Text=model.omdcouty+"&nbsp;"+model.omdcity;
                litHkgs.Text=model.ohkgs;
                litHbh.Text = model.ohbh;
                litYYjz.Text = string.Format("{0:g}", model.oyyjz); 
                if (litFwtype.Text == "翻译陪同") 
                {
                    yydw.Visible = true;
                }
                else if (litFwtype.Text == "接机送机")
                {
                    Pjj.Visible = true;
                }
                else if (litFwtype.Text == "全程管家") 
                {
                    Yyjz.Visible = true;
                }
                Bind();

            }

            //根据订单状态，显示各类操作按钮
            //switch (model.ouzt)
            //{
            //    case 2: //如果是线下支付，支付状态为0，如果是线上支付，支付成功后会自动改变订单状态为已确认
                 
            //        break;
            //    default:
            //        break;
            //}
        }
        #endregion

        #region JS提示============================================
        /// <summary>
        /// 添加编辑删除提示
        /// </summary>
        /// <param name="msgtitle">提示文字</param>
        /// <param name="url">返回地址</param>
        protected void JscriptMsg(string msgtitle, string url)
        {
            string msbox = "parent.jsprint(\"" + msgtitle + "\", \"" + url + "\")";
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msbox, true);
        }

        /// <summary>
        /// 绑定数据接受人
        /// </summary>
        protected void Bind() 
        {
            dtorderBll ordBll = new dtorderBll();
           
            string strPay = ordBll.getPay(Convert.ToString(litBh.Text));
            rptList.DataSource = ordBll.GetUniArt(strPay);
            rptList.DataBind();
        }
        /// <summary>
        /// 带回传函数的添加编辑删除提示
        /// </summary>
        /// <param name="msgtitle">提示文字</param>
        /// <param name="url">返回地址</param>
        /// <param name="callback">JS回调函数</param>
        protected void JscriptMsg(string msgtitle, string url, string callback)
        {
            string msbox = "parent.jsprint(\"" + msgtitle + "\", \"" + url + "\", " + callback + ")";
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msbox, true);
        }
        #endregion

        /// <summary>
        /// 设为订单接受人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void litBtn_Click(object sender, EventArgs e)
        {
            string strId = ((LinkButton)sender).CommandArgument;
            //opay  obh  
            if (litZt.Text != "已支付")
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(),"ok","<script>alert('未支付，无法接单')</script>");
                return;
            }
            dtorderBll ordBll = new dtorderBll();
            int rws = ordBll.GetJsrCount(litBh.Text);
             bool falg=false;
             if (rws > 0)
             {
                 falg = ordBll.UpdateField(litBh.Text, lbUserName.Text, strId,"1");
             }
             else
             {
                 //暂时无订单接受人
                 falg = ordBll.UpdateField(litBh.Text, lbUserName.Text, strId);
             }
            if (falg)
            {
                Bind();
            }
        }

        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e) 
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Literal litJzt = e.Item.FindControl("litJsr") as Literal;
                Literal litOzt = e.Item.FindControl("litOzt") as Literal;
                if (litJzt.Text == "1")
                {
                    litOzt.Text = "<font style='color:#ff0000; font-size:16px; margin-left:10px'>订单接受人</font>";
                }
                else 
                {
                    litOzt.Text = "";
                }
            }
        }
    }
}