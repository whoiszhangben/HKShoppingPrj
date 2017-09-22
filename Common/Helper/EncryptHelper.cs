using System;
using System.Runtime.InteropServices;
using System.Text;

namespace HKShoppingManage.Common
{
    public class EncryptHelper
    {
        [DllImport("libmism32.dll", EntryPoint = "MIMD5Encrypt", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        private static extern int MuchinfoMD5Encrypt(IntPtr pDst, ref int iDst, IntPtr pSrc, int iSrc);

        [DllImport("libmism32.dll", EntryPoint = "MIMD5GetEncryptDataLen", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        private static extern int MuchinfoMD5GetEncryptDataLen(ref int iRevLen, IntPtr pData, int iLen);

        [DllImport("libmism32.dll", EntryPoint = "MIGetSafeHandle", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        private static extern long MIGetSafeHandle();

        [DllImport("libmism32.dll", EntryPoint = "MIFreeSafeHandle", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        private static extern void MIFreeSafeHandle(long pSafeHandle);

        [DllImport("libmism32.dll", EntryPoint = "MILoad",
        ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        private static extern int MILoad(IntPtr pDst, int iDst, long pSafeHandle);

        [DllImport("libmism32.dll", EntryPoint = "MITransEncrypt",
        ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        private static extern int MITransEncrypt(IntPtr pDst, int iDst, IntPtr pSrc, int iSrc, long pSafeHandle);

        [DllImport("libmism32.dll", EntryPoint = "MIGetEncryptDataLen",
        ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        private static extern int MIGetEncryptDataLen(ref int iRevLen, IntPtr pData, int iLen, long pSafeHandle);

        [DllImport("libmism32.dll", EntryPoint = "MITransDecrypt",
        ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        private static extern int MITransDecrypt(IntPtr pDst, int iDst, IntPtr pSrc, int iSrc, long pSafeHandle);

        [DllImport("libmism32.dll", EntryPoint = "MIGetDecryptDataLen",
        ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        private static extern int MIGetDecryptDataLen(ref int iRevLen, IntPtr pData, int iLen, long pSafeHandle);

        [DllImport("libmism32.dll", EntryPoint = "MIAlterTransPwd",
       ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        private static extern int MIAlterTransPwd(StringBuilder pPwd, long pSafeHandle);

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>System.String.</returns>
        public static string MD5(string source)
        {
            byte[] szOutData = null;
            var szInData = Encoding.UTF8.GetBytes(source);
            int iResult = 0;
            ////加密后的数据长度
            int iOutEncryptDataLen = 0;
            ////待加密数据长度
            int iInEncryptDataLen = szInData.Length;

            ////申请内存拷贝待加密数据
            IntPtr pInEncryptData = Marshal.AllocHGlobal(iInEncryptDataLen);
            Marshal.Copy(szInData, 0, pInEncryptData, iInEncryptDataLen);

            // 获取加密后内存的长度
            iResult = MuchinfoMD5GetEncryptDataLen(ref iOutEncryptDataLen, pInEncryptData, iInEncryptDataLen);
            if (0 == iResult)
            {
                ////创建内存
                IntPtr pOutEncryptData = Marshal.AllocHGlobal(iOutEncryptDataLen);
                ////加密
                iResult = MuchinfoMD5Encrypt(pOutEncryptData, ref iOutEncryptDataLen, pInEncryptData, iInEncryptDataLen);
                if (iResult == 0)
                {
                    ////拷贝到数组上面
                    szOutData = new byte[iOutEncryptDataLen];
                    Marshal.Copy(pOutEncryptData, szOutData, 0, iOutEncryptDataLen);
                }
                ////释放内存
                Marshal.FreeHGlobal(pOutEncryptData);
            }

            ////释放内存
            Marshal.FreeHGlobal(pInEncryptData);
            if (szOutData == null)
            {
                return null;
            }

            return Encoding.UTF8.GetString(szOutData);
        }

        private long pSafeHandle;
        #region 单例
        private static EncryptHelper _instance;
        private static object obj = new object();
        public static EncryptHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (obj)
                    {
                        if (_instance == null)
                        {
                            _instance = new EncryptHelper();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="EncryptHelper"/> class.
        /// </summary>
        private EncryptHelper()
        {
            int iResult = 0;
            int iLoadTemp = 0;
            IntPtr pLoadTemp = IntPtr.Zero;

            this.pSafeHandle = MIGetSafeHandle();
            if (0 == this.pSafeHandle)
            {
                throw new Exception("EncryptHelper(获取加解密类失败...)");
            }

            iResult = MILoad(pLoadTemp, iLoadTemp, this.pSafeHandle);

            if (0 != iResult)
            {
                throw new Exception("EncryptHelper(加载密钥失败...)");
            }
        }

        /// <summary>
        /// 修改加密密钥
        /// </summary>
        /// <param name="keyData">密钥</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool AlterKey(StringBuilder keyData)
        {
            int iResult = 0;
            iResult = MIAlterTransPwd(keyData, this.pSafeHandle);
            return iResult == 0;
        }

        /// <summary>
        /// 释放加密对象。
        /// </summary>
        public void FreeHandle()
        {
            MIFreeSafeHandle(this.pSafeHandle);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="szInStr">需加密的数据</param>
        /// <param name="szOutStr">加密后的数据</param>
        /// <returns>返回加密是否成功</returns>
        public bool Encrypt(string szInStr, out string szOutStr)
        {
            byte[] szOutData = null;
            var szInData = Encoding.UTF8.GetBytes(szInStr);
            int iResult = 0;
            ////加密后的数据长度
            int iOutEncryptDataLen = 0;

            bool iRev = false;
            ////待加密数据长度
            int iInEncryptDataLen = szInData.Length;

            ////申请内存拷贝待加密数据
            IntPtr pInEncryptData = System.Runtime.InteropServices.Marshal.AllocHGlobal(iInEncryptDataLen);
            System.Runtime.InteropServices.Marshal.Copy(szInData, 0, pInEncryptData, iInEncryptDataLen);

            ////获取加密后内存的长度
            iResult = MIGetEncryptDataLen(ref iOutEncryptDataLen, pInEncryptData, iInEncryptDataLen, this.pSafeHandle);

            if (0 == iResult)
            {
                ////创建内存
                IntPtr pOutEncryptData = System.Runtime.InteropServices.Marshal.AllocHGlobal(iOutEncryptDataLen);
                ////加密
                iResult = MITransEncrypt(pOutEncryptData, iOutEncryptDataLen, pInEncryptData, iInEncryptDataLen, this.pSafeHandle);
                if (iResult == 0)
                {
                    iRev = true;
                    ////拷贝到数组上面
                    szOutData = new byte[iOutEncryptDataLen];
                    System.Runtime.InteropServices.Marshal.Copy(pOutEncryptData, szOutData, 0, iOutEncryptDataLen);
                }
                ////释放内存
                System.Runtime.InteropServices.Marshal.FreeHGlobal(pOutEncryptData);
            }
            ////释放内存
            System.Runtime.InteropServices.Marshal.FreeHGlobal(pInEncryptData);

            szOutStr = Convert.ToBase64String(szOutData);
            return iRev;
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="szInData">需解密的数据</param>
        /// <param name="szOutData">解密后的数据</param>
        /// <returns>返回解密是否成功</returns>
        public bool Decrypt(byte[] szInData, ref byte[] szOutData)
        {
            bool iRev = false;
            int iResult = 0;
            ////解密后的数据长度
            int iOutDecryptDataLen = 0;
            ////待解密数据长度
            int iInDecryptDataLen = szInData.Length;

            ////申请内存拷贝待解密数据
            IntPtr pInDecryptData = System.Runtime.InteropServices.Marshal.AllocHGlobal(iInDecryptDataLen);
            System.Runtime.InteropServices.Marshal.Copy(szInData, 0, pInDecryptData, iInDecryptDataLen);

            ////获取解密后内存的长度
            iResult = MIGetDecryptDataLen(ref iOutDecryptDataLen, pInDecryptData, iInDecryptDataLen, this.pSafeHandle);

            if (0 == iResult)
            {
                ////创建内存
                IntPtr pOutDecryptData = System.Runtime.InteropServices.Marshal.AllocHGlobal(iOutDecryptDataLen);

                ////加密
                iResult = MITransDecrypt(pOutDecryptData, iOutDecryptDataLen, pInDecryptData, iInDecryptDataLen, this.pSafeHandle);

                if (iResult == 0)
                {
                    iRev = true;
                    ////拷贝到数组上面
                    szOutData = new byte[iOutDecryptDataLen];
                    System.Runtime.InteropServices.Marshal.Copy(pOutDecryptData, szOutData, 0, iOutDecryptDataLen);
                }
                ////释放内存
                System.Runtime.InteropServices.Marshal.FreeHGlobal(pOutDecryptData);
            }
            ////释放内存
            System.Runtime.InteropServices.Marshal.FreeHGlobal(pInDecryptData);

            return iRev;
        }

        /// <summary>
        /// 加密密码
        /// </summary>
        /// <param name="secretKey">The secret key.</param>
        /// <param name="bankPassword">The bank password.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="MuchinfoException"></exception>
        public string EncryptPassd(string secretKey, string bankPassword)
        {
            string encryptPass = string.Empty;
            var keyByte = Convert.FromBase64String(secretKey);
            byte[] decryptKey = null;
            ////使用本地密钥解密密钥
            this.Decrypt(keyByte, ref decryptKey);
            string bitStr = Encoding.UTF8.GetString(decryptKey);
            var sb = new StringBuilder(bitStr);
            ////修改密钥
            if (this.AlterKey(sb))
            {
                this.Encrypt(bankPassword, out encryptPass);
                this.FreeHandle();
            }
            else
            {
                throw new Exception("God");
            }
            return encryptPass;
        }
    }
}
