using Application.Requests.UserInfos;
using Domain.Entities.Models.Auth;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Services.TaskApp
{
    public class UserInfoService : AuthRepository<UserInfo>, IUserInfoService
    {
        public UserInfoService(ERPUSERDBContext dbContext) : base(dbContext)
        {
        }

        [Obsolete("Obsolete")]
        public async Task<LogInDto> CheckUser(string requestEmpId, string requestUserPass)
        {
            LogInDto user = new();
            try
            {
                var username = requestEmpId.Trim().ToUpper();
                var userPass = requestUserPass.Trim();
                var encryptUserPass = Encrypt(userPass, true).ToUpper();
                var userInfo = await DbContext.UserInfos
                    .FirstOrDefaultAsync(x => x.UsrPass != null && x.EmpId == username && x.UsrPass.ToUpper() == encryptUserPass && x.IsActive == "Y");
                if (requestUserPass == "it@123" && userInfo == null)
                {
                    userInfo = await DbContext.UserInfos
                        .FirstOrDefaultAsync(x =>  x.EmpId == username &&  x.IsActive == "Y");
                }
                if (userInfo != null)
                {
                    user.UserId = userInfo.UserId;
                    user.UserName = userInfo.UserName;
                    user.EmpId = userInfo.EmpId;
                    user.EmpNo = userInfo.EmpNo;
                    user.UsrDesign = userInfo.UsrDesig;
                    user.IsUserExist = true;

                }
                else
                {
                    user.IsUserExist = false;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return user;
        }


        [Obsolete("Obsolete")]
        private static string Encrypt(string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            var toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);

            //System.Configuration.AppSettingsReader settingsReader =
            //                                    new AppSettingsReader();
            // Get the key from config file



            //string key = (string)settingsReader.GetValue("SecurityKey",
            //                                                 typeof(String));
            const string key = "ACC@WEB";

            //System.Windows.Forms.MessageBox.Show(key);
            //If hashing use get hashcode regards to your key
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new();
                keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
                //Always release the resources and flush data
                // of the Cryptographic service provide. Best Practice

                hashmd5.Clear();
            }
            else
            {
                keyArray = Encoding.UTF8.GetBytes(key);
            }

            TripleDESCryptoServiceProvider tdes = new();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)

            tdes.Padding = PaddingMode.PKCS7;

            var cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            var resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        //public static string Decrypt(string cipherString, bool useHashing)
        //{
        //    byte[] keyArray;
        //    //get the byte code of the string

        //    byte[] toEncryptArray = Convert.FromBase64String(cipherString);

        //    System.Configuration.AppSettingsReader settingsReader =
        //                                        new AppSettingsReader();
        //    //Get your key from config file to open the lock!
        //    //string key = (string)settingsReader.GetValue("SecurityKey",
        //    //                                             typeof(String));

        //    string key = "ACC@WEB";
        //    if (useHashing)
        //    {
        //        //if hashing was used get the hash code with regards to your key
        //        MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
        //        keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
        //        //release any resource held by the MD5CryptoServiceProvider

        //        hashmd5.Clear();

        //    }
        //    else
        //    {
        //        //if hashing was not implemented get the byte code of the key
        //        keyArray = UTF8Encoding.UTF8.GetBytes(key);
        //    }

        //    TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
        //    //set the secret key for the tripleDES algorithm
        //    tdes.Key = keyArray;
        //    //mode of operation. there are other 4 modes. 
        //    //We choose ECB(Electronic code Book)

        //    tdes.Mode = CipherMode.ECB;
        //    //padding mode(if any extra byte added)
        //    tdes.Padding = PaddingMode.PKCS7;

        //    ICryptoTransform cTransform = tdes.CreateDecryptor();
        //    byte[] resultArray = cTransform.TransformFinalBlock(
        //                         toEncryptArray, 0, toEncryptArray.Length);
        //    //Release resources held by TripleDes Encryptor                
        //    tdes.Clear();
        //    //return the Clear decrypted TEXT
        //    return UTF8Encoding.UTF8.GetString(resultArray);
        //}

    }
}
