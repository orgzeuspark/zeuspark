using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using ZeusPark.Data.MySql;
using ZeusPark.Service.Model;

namespace ZeusPark.Service.Admin
{
    public class UserService
    {
        public void CreateAccount(UserAccount user)
        {
            using (zeusparkEntities entity = new zeusparkEntities())
            {
                useraccount account = new useraccount();
                account.AccountName = user.AccountName;
                account.AccountType = 1;
                account.Mail = user.Mail;
                account.Password = user.Password;
                account.RegisterTime = DateTime.Now;
                account.Telephone = user.Telephone;
                account.UserName = user.UserName;

                using (var tran = new TransactionScope())
                {
                    entity.useraccounts.Add(account);
                    entity.SaveChanges();
                    tran.Complete();
                }
             
            }
        }

        public string IsValidAccount(string name, string password)
        {
            using (zeusparkEntities entity = new zeusparkEntities())
            {
                var acc = entity.useraccounts.Where(x => (x.Telephone == name || x.Mail == name) && x.Password == password).FirstOrDefault();

                if (null != acc)
                {
                    return acc.UserName;
                }
            }

            return null;
        }

        public void CreateWeChatAccount(string name, string openId, string headImg, string unionId)
        {
            using (zeusparkEntities entity = new zeusparkEntities())
            {
                useraccount account = new useraccount();
                account.AccountName = name;
                account.AccountType = 2;
                account.OpenID = openId;
                account.HeadImgUrl = headImg;
                account.UnionId = unionId;
                account.RegisterTime = DateTime.Now;

                using (var tran = new TransactionScope())
                {
                    entity.useraccounts.Add(account);
                    entity.SaveChanges();
                    tran.Complete();
                }

            }
        }

        public bool IsWeChatAccountExist(string openId)
        {
            using (zeusparkEntities entity = new zeusparkEntities())
            {
                var account = entity.useraccounts.FirstOrDefault(a => a.OpenID == openId);
                if (null == account)
                {
                    return false;
                }
            }

            return true;
        }

        public void AddUploadHistory(ProdUploadHistory history)
        {
            using (zeusparkEntities entity = new zeusparkEntities())
            {
                uploadhistory historyObj = new uploadhistory();
                historyObj.Name = history.Name;
                historyObj.CategoryID = history.CategoryId;
                historyObj.Description = history.Description;
                historyObj.GroupID = history.GroupId;
                historyObj.MainImg = history.MainImg;
                historyObj.Price = history.Price;
                historyObj.ProdImg = history.ProdImg;
                historyObj.ProductUnique = history.ProductUnique;
                historyObj.UploadTime = DateTime.Now;
                historyObj.UploadUser = history.UploadUser;

                entity.uploadhistories.Add(historyObj);

                entity.SaveChanges();
            }
        }
    }
}
