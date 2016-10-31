using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using ZeusPark.Data.MySql;
using ZeusPark.Service.Model;

namespace ZeusPark.Service.Product
{
    public class OrderService
    {
        public OrderService()
        {

        }

        public void CreateOrder(OrderVM ordervm)
        {
            using (zeusparkEntities entity = new zeusparkEntities())
            {
                order ord = new order();
                ord.OrderUnique = ordervm.OrderUnique;
                ord.OrderUserId = ordervm.OrderUserId;
                ord.OrderAmount = ordervm.OrderAmount;
                ord.DiscountAmount = ordervm.DiscountAmount;
                ord.OrderDetail = ordervm.OrderDetail;
                ord.Status = 1;
                ord.Contactor = ordervm.Contactor == null? "" : ordervm.Contactor;
                ord.Mobile = ordervm.Mobile == null ? "" : ordervm.Mobile;
                ord.Email = ordervm.Email == null ? "" : ordervm.Email;
                ord.Telephone = ordervm.Telephone;
                ord.City = ordervm.City == null ? "" : ordervm.City;
                ord.PostCode = ordervm.PostCode;
                ord.Address = ordervm.Address == null ? "" : ordervm.Address;
                ord.DiscountCode = ordervm.DiscountCode;
                ord.Note = ordervm.Note;
                ord.OrderImg = ordervm.OrderImg;
                ord.ProdIds = ordervm.ProdIds;
                ord.CreateTime = DateTime.Now;
                ord.LastUpdateTime = DateTime.Now;

                using (var tran = new TransactionScope())
                {
                    entity.orders.Add(ord);
                    entity.SaveChanges();
                    tran.Complete();
                }

            }
        }

        public List<OrderVM> GetOrdersByUser(int userid)
        {
            List<OrderVM> ordervmlist = new List<OrderVM>();
            using (zeusparkEntities entity = new zeusparkEntities())
            {
                var orders = entity.orders.Where(x => x.OrderUserId == userid).OrderByDescending(x => x.LastUpdateTime).ToList();
                foreach(var order in orders)
                {
                    OrderVM vm = new OrderVM();
                    vm.OrderID = order.OrderId;
                    vm.OrderUnique = order.OrderUnique;
                    vm.Status = order.Status;
                    vm.StatusDisplay = EnumUtil.GetOrderStatus(order.Status);
                    vm.OrderAmount = order.OrderAmount;
                    vm.OrderDetail = order.OrderDetail.Replace("null", "").Replace("##", "<br>");
                    vm.Address = order.Address;
                    vm.Contactor = order.Contactor;
                    vm.Mobile = order.Mobile;
                    vm.Note = order.Note;
                    vm.OrderImg = order.OrderImg;
                    vm.OrderUserId = order.OrderUserId;

                    ordervmlist.Add(vm);
                }
            }

            return ordervmlist;
        }

        public OrderVM GetOrder(int orderid)
        {
            using (zeusparkEntities entity = new zeusparkEntities())
            {
                var order = entity.orders.FirstOrDefault(x => x.OrderId == orderid);
                OrderVM vm = new OrderVM();
                vm.OrderID = order.OrderId;
                vm.OrderUnique = order.OrderUnique;
                vm.Status = order.Status;
                vm.StatusDisplay = EnumUtil.GetOrderStatus(order.Status);
                vm.OrderAmount = order.OrderAmount;
                vm.OrderDetail = order.OrderDetail.Replace("null", "").Replace("##", "<br>");
                vm.Address = order.Address;
                vm.Contactor = order.Contactor;
                vm.Mobile = order.Mobile;
                vm.Note = order.Note;
                vm.OrderImg = order.OrderImg;
                vm.OrderUserId = order.OrderUserId;

                return vm;
            }
        }

        public void UpdateOrderStatus(int orderid, int status)
        {
            using (zeusparkEntities entity = new zeusparkEntities())
            {
                var order = entity.orders.FirstOrDefault(x => x.OrderId == orderid);
                if (null != order)
                {
                    order.Status = status;
                    order.LastUpdateTime = DateTime.Now;
                    entity.SaveChanges();
                }
            }
        }

        public List<OrderVM> GetActiveOrders()
        {
            List<OrderVM> ordervmlist = new List<OrderVM>();
            using (zeusparkEntities entity = new zeusparkEntities())
            {
                var orders = entity.orders.Where(x => x.Status > 0).OrderByDescending(x => x.LastUpdateTime).ToList();
                foreach (var order in orders)
                {
                    OrderVM vm = new OrderVM();
                    vm.OrderID = order.OrderId;
                    vm.OrderUnique = order.OrderUnique;
                    vm.Status = order.Status;
                    vm.StatusDisplay = EnumUtil.GetOrderStatus(order.Status);
                    vm.OrderAmount = order.OrderAmount;
                    vm.OrderDetail = order.OrderDetail.Replace("null","").Replace("##", "<br>");
                    vm.Address = order.Address;
                    vm.Contactor = order.Contactor;
                    vm.Mobile = order.Mobile;
                    vm.Telephone = order.Telephone;
                    vm.Note = order.Note;
                    vm.OrderImg = order.OrderImg;
                    vm.OrderUserId = order.OrderUserId;

                    ordervmlist.Add(vm);
                }
            }

            return ordervmlist;
        }


    }
}
