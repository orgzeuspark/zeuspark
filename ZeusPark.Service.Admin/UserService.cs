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

        public bool IsAccountExist(UserAccount user)
        {
            using (zeusparkEntities entity = new zeusparkEntities())
            {
                var acc = entity.useraccounts.FirstOrDefault(x => x.AccountName == user.AccountName || x.Mail == user.Mail || x.Telephone == user.Telephone);
                if (null != acc)
                {
                    return true;
                }

                return false;
            }
        }

        public int IsValidAccount(string name, string password, out string username, out int accType)
        {
            using (zeusparkEntities entity = new zeusparkEntities())
            {
                var acc = entity.useraccounts.Where(x => (x.AccountName == name || x.Telephone == name || x.Mail == name) && x.Password == password).FirstOrDefault();

                if (null != acc)
                {
                    username = acc.UserName;
                    accType = acc.AccountType;
                    return acc.UserID;
                }
                else
                {
                    accType = 0;
                    username = string.Empty;
                }
            }

            return 0;
        }

        public int CreateWeChatAccount(string name, string openId, string headImg, string unionId)
        {
            int userid = 0;
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

                userid = account.UserID;
            }

            return userid;
        }

        public bool IsWeChatAccountExist(string openId, out int userid, out int accType)
        {
            using (zeusparkEntities entity = new zeusparkEntities())
            {
                var account = entity.useraccounts.FirstOrDefault(a => a.OpenID == openId);
                if (null == account)
                {
                    userid = 0;
                    accType = 0;
                    return false;
                }
                else
                {
                    accType = account.AccountType;
                    userid = account.UserID;
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

        public List<ProdUploadHistory> GetHistoryByUserId(int userid)
        {
            List<ProdUploadHistory> hisList = new List<ProdUploadHistory>();
            using (zeusparkEntities entity = new zeusparkEntities())
            {
                var histories = entity.uploadhistories.Where(x => x.UploadUser == userid).ToList();

                foreach(var his in histories)
                {
                    ProdUploadHistory prodhis = new ProdUploadHistory();
                    prodhis.UploadTime = his.UploadTime;
                    prodhis.Name = his.Name;
                    prodhis.ProductUnique = his.ProductUnique;

                    hisList.Add(prodhis);
                }
            }

            return hisList;
        }
    }
}
