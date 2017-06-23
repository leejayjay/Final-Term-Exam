using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP.NET_FinalTermExam.Models
{
    public class OrderService
    {
        /// <summary>
        /// 取得DB連線字串
        /// </summary>
        /// <returns></returns>
        private string GetDBConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString.ToString();
        }
        /// <summary>
        /// 新增
        /// </summary>
        public void InsertOrder()
        {

        }
        /// <summary>
        /// 刪除
        /// </summary>
        public void DeleteOrderById()
        {

        }
        /// <summary>
        /// 修改
        /// </summary>
        public void UpdateOrder()
        {

        }
        /// <summary>
        /// 依照ID取得訂單
        /// </summary>
        /// <param name="id">訂單ID</param>
        /// <returns></returns>
        public Models.Order GetOrderById(string OrderId)
        {
            DataTable dt = new DataTable();
            Order result = new Order();
            string sql = @"SELECT 
					    A.OrderId,B.CustomerID As CustId,B.CompanyName AS CustName,
					    B.ContactName,B.ContactTitle					    
					    From Sales.Orders As A INNER JOIN Sales.Customers As B ON 
					    A.CustomerID = B.CustomerID
                        WHERE OrderID = @OrderId";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@OrderId", OrderId));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }

            return MapOrderDataToList(dt).FirstOrDefault();
        }
        /// <summary>
        /// 下拉式選單員工
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> DropDownListEmp()
        {
            DataTable dt = new DataTable();
            Order result = new Order();
            string sql = @"SELECT FirstName+' '+LastName as EmployeeName , EmployeeID FROM HR.Employees";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }

            return DropData1(dt);
        }
        private List<SelectListItem> DropData1(DataTable orderData)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            foreach (DataRow row in orderData.Rows)
            {
                result.Add(new SelectListItem()
                {
                    Text = row["EmployeeName"].ToString(),
                    Value = row["EmployeeId"].ToString()
                });
            };
            return result;
        }
        /// <summary>
        /// 下拉式選單出貨公司
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> DropDownListship()
        {
            DataTable dt = new DataTable();
            Order result = new Order();
            string sql = @"SELECT CompanyName , ShipperID FROM Sales.Shippers";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }

            return DropData2(dt);
        }
        public static List<SelectListItem> DropData2(DataTable orderData)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            foreach (DataRow row in orderData.Rows)
            {
                result.Add(new SelectListItem()
                {
                    Text = row["CompanyName"].ToString(),
                    Value = row["ShipperID"].ToString()
                });
            };
            return result;
        }
        /// <summary>
        /// 依照條件取得訂單
        /// </summary>
        /// <returns></returns>
        public List<Models.Order> GetOrderByCondition(Models.Order arg)
        {
            List<Models.Order> result = new List<Order>();
            DataTable dt = new DataTable();
            string sql = @"SELECT 
					A.OrderId,B.CompanyName As CustName,
                    CONVERT(varchar(10),A.OrderDate,111) as OrderDate,CONVERT(varchar(10),A.ShippedDate,111) as ShippedDate
					From Sales.Orders As A 
					INNER JOIN Sales.Customers As B ON A.CustomerID=B.CustomerID
					INNER JOIN HR.Employees As C On A.EmployeeID=C.EmployeeID
					inner JOIN Sales.Shippers As D ON A.shipperid=D.shipperid

					Where (A.OrderId Like @OrderId or @OrderId = '') And
                          (B.Companyname Like @CustName Or @CustName = '') And
                          (c.EmployeeID = @EmployeeId or @EmployeeId = '') And 
                          (D.companyName = @ShipName or  @ShipName= '') And 
                          (A.OrderDate = @OrderDate or @OrderDate = '') And 
                          (A.ShippedDate = @ShippedDate or @ShippedDate = '') And
                          (A.RequiredDate = @RequireDate or @RequireDate = '')";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@OrderId", arg.OrderID == null ? string.Empty : '%' + arg.OrderID + '%'));
                cmd.Parameters.Add(new SqlParameter("@CustName", arg.CustName == null ? string.Empty : '%' + arg.CustName + '%'));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return MapOrderDataToList(dt);
        }
        public static List<Models.Order> MapOrderDataToList(DataTable orderData)
        {
            List<Models.Order> result = new List<Order>();
            foreach (DataRow row in orderData.Rows)
            {
                result.Add(new Order()
                {
                    // = row["CustomerID"].ToString(),
                    CustName = row["CustName"].ToString(),
                    //EmployeeId = row["EmployeeID"].ToString(),
                    //EmpName = row["EmpName"].ToString(),
                    //Freight = (decimal)row["Freight"],
                    //Orderdate = row["Orderdate"].ToString(),
                    OrderID = row["OrderId"].ToString(),
                    //RequireDdate = row["RequireDdate"].ToString(),
                    //ShipAddress = row["ShipAddress"].ToString(),
                    //ShipCity = row["ShipCity"].ToString(),
                    //ShipCountry = row["ShipCountry"].ToString(),
                    //ShipName = row["ShipName"].ToString(),
                    //ShippedDate = row["ShippedDate"].ToString(),
                    //ShipperId = (int)row["ShipperId"],
                    //ShipperName = row["ShipperName"].ToString(),
                    //ShipPostalCode = row["ShipPostalCode"].ToString(),
                    //ShipRegion = row["ShipRegion"].ToString()
                });
            }
            return result;
        }
    }
}