using CoYqlp.WepApp.Helpers;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoYqlp.WepApp.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// Lấy domain server gọi api
        /// </summary>
        public string DomainRequest
        {
            get
            {
                return Request.Url.Authority;
            }
        }

        /// <summary>
        /// Lấy uid của người dùng
        /// </summary>
        public int GetUserID
        {
            get
            {
                return int.Parse(User.Identity.GetUserId());
            }
        }

        /// <summary>
        /// Lấy IP Server gọi api
        /// </summary>
        public string IPRequest
        {
            get
            {
                return NetworkHelpers.GetIPAddressClient();
            }
        }

        /// <summary>
        ///  Lỗi xảy ra khi thực hiện hàm
        /// </summary>
        /// <returns></returns>
        public JsonResult ErrorJson
        {
            get
            {
                return Json(new { status = "error" }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        ///  Thực hiện hàm thành công
        /// </summary>
        /// <returns></returns>
        public JsonResult SucessJson
        {
            get
            {
                return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        ///  Json rỗng
        /// </summary>
        /// <returns> Empty Json </returns>
        public JsonResult EmptyJson
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Trả về một object
        /// </summary>
        /// <param name="dynamic"> object cần trả về </param>
        /// <returns></returns>
        public JsonResult OK(object dynamic)
        {
            return Json(dynamic, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Kiểm tra chuỗi có NULL || Emply || Space
        /// </summary>
        /// <param name="sData"></param>
        /// <returns></returns>
        internal bool IsInValid(string sData)
        {
            return String.IsNullOrEmpty(sData) || String.IsNullOrWhiteSpace(sData);
        }

        /// <summary>
        /// Hàm hủy các khởi tạo nếu có trước khi đóng tiến trình
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}