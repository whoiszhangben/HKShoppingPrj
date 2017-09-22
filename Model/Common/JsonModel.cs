
namespace HKShoppingManage.Model
{
    /// <summary>
    /// 通用json返回实体
    /// </summary>
    public class JsonModel
    {
        private JsonModel() { }

        /// <summary>
        /// 实例化JsonModel
        /// </summary>
        /// <param name="isSuccess">结果</param>
        /// <param name="errorCode">错误码</param>
        /// <param name="data"></param>
        /// <param name="url">需要返回的数据</param>
        public JsonModel(bool isSuccess, string errorCode, object data = null, string url = "")
        {
            IsSuccess = isSuccess;
            ErrorCode = errorCode;
            Data = data;
            Url = url;
        }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 错误码
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 跳转地址（立即跳转，如果需要操作后跳转，请在Data中添加相应数据，自行操作）
        /// </summary>
        public string Url { get; set; }


        public override string ToString()
        {
            return string.Format("{\"IsSuccess\":{0},\"ErrorCode\": \"{1}\",\"Url\":\"{2}\"}", IsSuccess, ErrorCode, Url);
        }

    }
}
