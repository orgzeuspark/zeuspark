using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using ZeusPark.Data.MySql;
using ZeusPark.Service.Model;

namespace ZeusPark.Service.Product
{
    public class SearchService
    {
        public SearchService()
        {

        }


        public List<string> GetFilterByGroup(int groupId)
        {
            List<string> filters = new List<string>();
            using (zeusparkEntities entity = new zeusparkEntities())
            {
                var query = from p1 in entity.products
                            join g1 in entity.productgroups on p1.ProductID equals g1.ProductID
                            where g1.GroupID == groupId && p1.Deleted == false && p1.Status == 1
                            select p1.Label;
                foreach (var label in query)
                {
                    if (!string.IsNullOrEmpty(label))
                    {
                        if (label.Contains(";"))
                        {
                            var ls = label.Split(';');
                            foreach(var l in ls)
                            {
                                if (!string.IsNullOrEmpty(l))
                                {
                                    if (!filters.Contains(l.Trim()))
                                    {
                                        filters.Add(l.Trim());
                                    }
                                        
                                }
                            }
                        }
                        else
                        {
                            if (!filters.Contains(label.Trim()))
                            {
                                filters.Add(label.Trim());
                            }
                            
                        }
                        
                    }
                    
                }

                filters.Add("其他");

                return filters;
            }
        }


    }
}
