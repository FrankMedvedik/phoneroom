using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;



    namespace PhoneLogic.Repository
    {
        public class FormatUtil
        {
            public static string DatabasePhoneFormatter = "T-SQL Formatter";
            public static global::System.String formatPhoneNumber(string PhoneNumber)
            {

                string pn = FormatUtil.StripPhoneNumber(PhoneNumber);
                string p = "";
                if (!string.IsNullOrEmpty(pn))
                {
                    var connectionString = ConfigurationManager.ConnectionStrings["respondentConn"].ConnectionString;
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        using (SqlCommand comm = new SqlCommand("dbo.udf_FormatPhone", conn))
                        {
                            comm.CommandType = System.Data.CommandType.StoredProcedure;

                            SqlParameter p1 = new SqlParameter("@phone_number", SqlDbType.Text);
                            SqlParameter p2 = new SqlParameter("@Result", SqlDbType.Text);

                            p1.Direction = ParameterDirection.Input;
                            p2.Direction = ParameterDirection.ReturnValue;

                            p1.Value = pn;

                            comm.Parameters.Add(p1);
                            comm.Parameters.Add(p2);

                            conn.Open();
                            comm.ExecuteNonQuery();

                            if (p2.Value != DBNull.Value)
                            { 
                                p = (string) p2.Value;
                                //strip out brace!
                                p = p.Replace('}',' ');
                                
                            }
                        }
                    }
                }
                else
                    p = PhoneNumber;
                return p;
            }

            public static global::System.String StripPhoneNumber(string PhoneNumber)
            {
                {
                    if (PhoneNumber == null)
                        return "";
                    else
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (char c in PhoneNumber)
                        {
                            if (c >= '0' && c <= '9')
                            { sb.Append(c); }
                        }
                        return sb.ToString();
                    }
                }
            }

        }
    }
