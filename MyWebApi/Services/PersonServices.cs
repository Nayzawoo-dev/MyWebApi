using Database.Shared;
using Microsoft.Data.SqlClient;
using MyWebApi.Models;

namespace MyWebApi.Services
{
    public class PersonServices
    {
        DapperServices _dapperservices;

        public PersonServices()
        {
            _dapperservices = new DapperServices(new SqlConnectionStringBuilder
            {
                DataSource = "DELL",
                InitialCatalog = "DotNetTraining",
                UserID = "SA",
                Password = "root",
                TrustServerCertificate = true
            });
        }

        public ResponseModel GetPersonList()
        {
            string query = "select * from Tbl_Window";
            var list = _dapperservices.Query<PersonModels>(query);
            ResponseModel res = new ResponseModel
            {
                Complete = true,
                Message = "Successful",
                Data = list
            };
            return res;
        }

        public ResponseModel GetPersonListById(int id)
        {
            //if (DevCode.isInt(id))
            //{
            //    var result = new ResponseModel
            //    {
            //        Complete = false,
            //        Message = "Your Id is Invalid"
            //    };
            //    return result;
            //}
            string query = "select * from Tbl_Window where Id = @Id";
            var list = _dapperservices.Query<PersonModels>(query, new PersonModels
            {
                Id = id
            });
            var res = new ResponseModel
            {
                Complete = list.Count > 0,
                Message = list.Count > 0 ? "Person Found" : "Person not found",
                Data = list
            };
            return res;
        }

        public ResponseModel PostPerson(PersonModels model)
        {
            string field = string.Empty;
            
            string password = model.Password.Replace(" ", "")!;
            model.Password = password;

            if (!model.UserName.isNull())
            {
                field += "@UserName,";
            }

            if (!model.Password.isNull())
            {
                field += "@Password,";
            }


            if (field.Length == 0)
            {
                var res1 = new ResponseModel
                {
                    Complete = false,
                    Message = "No Field To Post"
                };
                return res1;
            }
            else
            {
                field = field.Substring(0, field.Length - 1);
                string query = $@"INSERT INTO [dbo].[Tbl_Window]
           ([UserName]
           ,[Password])
     VALUES
           ({field})";
                var result = _dapperservices.Execute(query, model);
                var res2 = new ResponseModel
                {
                    Complete = result > 0,
                    Message = result > 0 ? "Post Success" : "Post Fail"
                };
                return res2;
            }

        }

        public ResponseModel UpdateAndPostPerson(int id, PersonModels model)
        {
            model.Id = id;
            string field = string.Empty;
            string query = "select * from Tbl_Window where Id = @Id";
            var list = _dapperservices.Query<PersonModels>(query, model);
            if (list.Count == 0)
            {
                if (!model.UserName.isNull())
                {
                    field += "@UserName,";
                }

                if (!model.Password.isNull())
                {
                    string password = model.Password.Replace(" ", "")!;
                    model.Password = password;
                    field += "@Password,";
                }

                if (field.Length == 0)
                {
                    var res1 = new ResponseModel
                    {
                        Complete = false,
                        Message = "No Field To Post"
                    };
                    return res1;
                }

                field = field.Substring(0, field.Length - 1);
                string query2 = $@"INSERT INTO [dbo].[Tbl_Window]
           ([UserName]
           ,[Password])
     VALUES
           ({field})";
                var result1 = _dapperservices.Execute(query2, model);
                var res2 = new ResponseModel
                {
                    Complete = result1 > 0,
                    Message = result1 > 0 ? "Post Success" : "Post Fail"
                };
                return res2;

            }

            if (!model.UserName.isNull())
            {
                field += "[UserName] = @UserName,";
            }

            if (!model.Password.isNull())
            {
                string password = model.Password.Replace(" ", "")!;
                model.Password = password;
                field += "[Password] = @Password,";
            }

            if (field.Length == 0)
            {
                var res3 = new ResponseModel
                {
                    Complete = false,
                    Message = "No Field To Update"
                };
                return res3;
            }

            field = field.Substring(0, field.Length - 1);
            string query3 = $@"UPDATE [dbo].[Tbl_Window]
   SET {field}
 WHERE Id = @Id";
            var result = _dapperservices.Execute(query3, model);
            var finalres = new ResponseModel
            {
                Complete = result > 0,
                Message = result > 0 ? "Complete" : "Fail"
            };
            return finalres;
        }

        public ResponseModel UpdatePerson(int id, PersonModels model)
        {
            model.Id = id;
            string field = string.Empty;
            string query = "select * from Tbl_Window where Id = @Id";
            var list = _dapperservices.Query<PersonModels>(query, model);
            if (list.Count == 0)
            {
                ResponseModel res = new ResponseModel
                {
                    Complete = false,
                    Message = "Person not found"
                };
                return res;
            }

            if (!model.UserName.isNull())
            {
                field += "[UserName] = @UserName,";
            }

            if (!model.Password.isNull())
            {
                string password = model.Password.Replace(" ", "")!;
                model.Password = password;
                field += "[Password] = @Password,";
            }

            if (field.Length == 0)
            {
                ResponseModel res1 = new ResponseModel
                {
                    Complete = false,
                    Message = "No Filed To Update"
                };
                return res1;
            }

            if (field.Length > 0)
            {
                field = field.Substring(0, field.Length - 1);
            }

            string query2 = $@"UPDATE [dbo].[Tbl_Window]
   SET {field}
 WHERE Id = @Id";
            var res2 = _dapperservices.Execute(query2, model);
            ResponseModel resopn = new ResponseModel
            {
                Complete = res2 > 0,
                Message = res2 > 0 ? "Update Complete" : "Update Fail"
            };

            return resopn;
        }

        public ResponseModel DeletePerson(int id)
        {
            string query = "select * from Tbl_Window where Id = @Id";
            var list = _dapperservices.Query<PersonModels>(query, new PersonModels
            {
                Id = id
            });
            if (list.Count == 0)
            {
                ResponseModel res1 = new ResponseModel
                {
                    Complete = false,
                    Message = "Person not found"
                };
                return res1;
            }
            string query2 = @"DELETE FROM [dbo].[Tbl_Window]
      WHERE Id = @Id";
            int res = _dapperservices.Execute(query2, new PersonModels
            {
                Id = id
            });
            ResponseModel model = new ResponseModel
            {
                Complete = res > 0,
                Message = res > 0 ? "Delete Success" : "Delete Fail"
            };
            return model;

        }


    }
}