using System;
using System.Data.SqlTypes;

namespace Model
{
   public class dt_order
    {
       public Guid oids{get;set;}    //主键编号
       public string obh{get;set;}//订单编号
       public string opay{get;set;}//预约的id article id相同
       public string otype{get;set;}//订单类型
       public SqlDateTime oyysj { get; set; } //预约时间
       public SqlDateTime oyyjz { get;set;}//预约截至时间
       public string  oyydw { get; set; } //预约单位
       public string oyybzj{get;set;} //预约保证金
       public string ojjlx{get;set;}   //接机类型
       public string omdcouty{get;set;}  //接机国家
       public string omdcity{get;set;}   //接机城市
       public string ohkgs { get; set; }//国际机场
       public string ohbgs { get; set; }//航班公司
       public string ohbh{get;set;}      //航班号
       public string ouser{get;set;}     //预约人
       public SqlDateTime ouyy{get;set;}     //添加时间
       public int ouzt { get; set; }      //状态
       public string ojsr{get;set;}           //受理人
       public SqlDateTime? ojsj { get; set; }  //受理时间
       public string obz{get;set;}              //受理备注
       public string oal{get;set;}              //保留字段1
       public string obl{get;set;}              //保留字段2


       public dt_order()
       {
           oids = Guid.NewGuid();
           obh = string.Empty;
           opay = string.Empty;
           otype = string.Empty;
           oyysj = SqlDateTime.MinValue;
           oyyjz = SqlDateTime.MinValue;
           oyydw = string.Empty;
           oyybzj = string.Empty;
           ojjlx = string.Empty;
           omdcouty = string.Empty;
           omdcity = string.Empty;
           ohkgs = string.Empty;
           ohbh = string.Empty;
           ouser = string.Empty;
           ouyy = SqlDateTime.MinValue;
           ouzt = 0;
           ojsr = "0";
           ojsj = SqlDateTime.MinValue;
           obz = string.Empty;
           oal = string.Empty;
           obl = string.Empty;
           ohbgs = string.Empty;
       }
    }
}
