using BillerClientConsole.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BillerClientConsole.Globals
{
    public class Globals
    {
        public static string service_end_point = "http://localhost:800";//  "http://localhost:4430";//"http://localhost/company"  ;; http://localhost:4430/"https://localhost:44380";//
        public static string searchApplicationID = "";

        public static string tempSearchNameId1 = "";
        public static string tempSearchNameId2 = "";
        public static string tempSearchNameId3 = "";
        public static string tempSearchNameId4 = "";
        public static string tempSearchNameId5 = "";
        public static string tempSearchNameId6 = "";//

        public static string tempSearchId1 = "";

        public static Webdev.Payments.Paynow payment;
        public static Webdev.Core.InitResponse response;

        public static mCompanyInfo companyInfo = new mCompanyInfo();
        public static List<mmainClause> objects = new List<mmainClause>();
        public static List<mMembersPotifolio> members = new List<mMembersPotifolio>();

        //public static mCompany PrivateCompany = new mCompany();

        //missing apis test mode
        //  string applicationId;
        //Advanced search routes
        public static string end_point_FilterNames = $"{service_end_point}/api/v1/FilterNames";


        // public static string end_point_register_office= $"{service_end_point}​/PvtRegistration​/{Dto.applicationId}​/RegisterOffice";
        //reports
        //feedback
        public static string end_point_add_search = $"{service_end_point}/api/v1/postSearch";
        public static string end_point_approve_search_name = $"{service_end_point}/api/v1/ApproveSearchedName";
        public static string end_point_get_name_searches = $"{service_end_point}/api/v1/GetNameSearches";
        public static string end_point_get_name_searches_by_id = $"{service_end_point}/api/v1/GetNameSearchesBySearchID";
        public static string end_point_get_name_searches_by_user = $"{service_end_point}/api/search/GetNameSearchesByUser";
        public static string end_point_get_name_searches_by_examiner = $"{service_end_point}/api/search/GetNameSearchesByExaminer";
        public static string end_point_check_name_availability = $"{service_end_point}/api/v1/CheckName";

        public static string end_point_assign_search_for_examination = $"{service_end_point}/api/v1/AssignSearchForExamine";
        public static string end_point_mark_as_un_read_feedback_by_id = $"{service_end_point}:2001/scpayment/v1/mark_as_un_read_feedback_by_id";

        //payment history
        public static string end_point_biller_paymenthistory = $"{service_end_point}:2001/scpayment/v1/GetAllBillerPaymentHistory";
        public static string end_point_get_paymenthistory_by_id = $"{service_end_point}:2001/scpayment/v1/get_biller_payment_history_by_id";
        public static string end_point_pay_for_search = $"{service_end_point}/api/v1/PayforSearch";

        //transaction history
        public static string end_point_biller_transactionhistory = $"{service_end_point}:2001/scpayment/v1/GetAllBillerTransactionsHistory";
        public static string end_point_get_transactionhistory_by_id = $"{service_end_point}:2001/scpayment/v1/get_transactionhistory_by_id";

       

        //products
        public static string end_point_GetAllBillerProducts = $"{service_end_point}:2001/scpayment/v1/GetAllBillerProducts";
        public static string end_point_postBillerProduct = $"{service_end_point}:2001/scpayment/v1/postBillerProduct";
        public static string end_point_updateBillerProduct = $"{service_end_point}:2001/scpayment/v1/UpdateBillerProductById";
        public static string end_point_fetchBillerProductById = $"{service_end_point}:2001/scpayment/v1/fetchBillerProductById";
        public static string end_point_DeleteProductBillerProductById = $"{service_end_point}:2001/scpayment/v1/DeleteBillerProductByID";


        //biller account info
        public static string end_point_GetBillerInfoByBillerCode = $"{service_end_point}:2001/scpayment/v1/GetBillerInfoByBillerCode";
        public static string end_point_UpdateBillerInfoByBillerCode = $"{service_end_point}:2001/scpayment/v1/UpdateBillerInfoByBillerCode";


        //notifications
        public static string end_point_fetchBillerNotifications = $"{service_end_point}:2001/scpayment/v1/fetchBillerNotifications";
        public static string end_point_fetchBillerNotificationById = $"{service_end_point}:2001/scpayment/v1/fetchBillerNotificationById";
        public static string end_point_markNotificationAsRead = $"{service_end_point}:2001/scpayment/v1/markNotificationAsRead";
        public static string end_point_markNotificationAsUnRead = $"{service_end_point}:2001/scpayment/v1/markNotificationAsUnRead";
        public static string end_point_deleteNotificationById = $"{service_end_point}:2001/scpayment/v1/deleteNotificationById";

        public static string end_point_countUnReadNotificationsForBiller = $"{service_end_point}:2001/scpayment/v1/countUnReadNotificationsForBiller";
        public static string end_point_countReadNotificationsForBiller = $"{service_end_point}:2001/scpayment/v1/countReadNotificationsForBiller";
        public static string end_point_countAllNotificationsForBiller = $"{service_end_point}:2001/scpayment/v1/countAllNotificationsForBiller";


        //products
        public static string end_point_countAllActiveProductsForBiller = $"{service_end_point}:2001/scpayment/v1/countAllActiveProductsForBiller";
        public static string end_point_countAllInActiveProductsForBiller = $"{service_end_point}:2001/scpayment/v1/countAllInActiveProductsForBiller";
        public static string end_point_countAllProductsForBiller  = $"{service_end_point}:2001/scpayment/v1/countAllProductsForBiller";

        //payments and transactions
        public static string end_point_countAllPaymentHistoryForBiller = $"{service_end_point}:2001/scpayment/v1/countAllPaymentHistoryForBiller";
        public static string end_point_countAllTransactionHistoryForBiller = $"{service_end_point}:2001/scpayment/v1/countAllTransactionHistoryForBiller";

        //feedback
        public static string end_point_countAllReadFeedBackForBiller = $"{service_end_point}:2001/scpayment/v1/countAllReadFeedBackForBiller";
        public static string end_point_countAllUnReadFeedBackForBiller = $"{service_end_point}:2001/scpayment/v1/countAllUnReadFeedBackForBiller";


        //enquiries
        public static string end_point_postBillerInquiryQuestions = $"{service_end_point}:2001/scpayment/v1/postBillerInquiryQuestions";
        public static string end_point_fetchEnquiryQuestionByID = $"{service_end_point}:2001/scpayment/v1/fetchEnquiryQuestionByID";
        public static string end_point_UpdateBillerEnquiryByID = $"{service_end_point}:2001/scpayment/v1/UpdateBillerEnquiryByID";
        public static string end_point_delete_biller_enquiry_by_id = $"{service_end_point}:2001/scpayment/v1/delete_biller_enquiry_by_id";
        public static string end_point_GetAllBillerEnquiries = $"{service_end_point}:2001/scpayment/v1/GetAllBillerEnquiries";
        public static string end_point_get_company_applications = $"{service_end_point}/api/v1/GetCompanyApplication";

        //name searches
        public static string end_point_post_search = $"{service_end_point}/api/v1/postSearch";
        public static string end_point_submit_name_search = $"{service_end_point}/api/v1/SubmitSearch";
        public static string end_point_get_name_searches_by_user_v1 = $"{service_end_point}/api/v1/GetNameSearchesByUser";
        public static string end_point_get_name_searches_by_examiner_id = $"{service_end_point}/api/v1/GetNameSearchesByExaminerID";

        //principal examiner
        public static string end_point_post_task = $"{service_end_point}/api/v1/postTask";

        //examiner
        public static string end_point_get_assigned_tasks = $"{service_end_point}/api/v1/GetTasksByExaminer";
        public static string end_point_get_name_search_by_user_by_task_id = $"{service_end_point}/api/v1/GetNameSearchesByUserByTaskID";

        //enquiry questions

        public static string end_point_get_company_application_by_user_id = $"{service_end_point}/api/v1/GetCompanyApplicationByUserID";
        public static string end_point_check_name_start_contains_in_name_search = $"{service_end_point}/api/v1/CheckNameStartContainsInNameSearch";
        public static string end_point_check_name_start_with_name_search = $"{service_end_point}/api/v1/CheckNameStartWithInNameSearch";
        public static string end_point_approved_search_name = $"{service_end_point}/api/v1/ApproveSearchedName";
        public static string end_point_get_name_searches_by_search_id = $"{service_end_point}/api/v1/GetNameSearchesBySearchID";
        public static string end_point_get_update_task = $"{service_end_point}/api/v1/UpdateTask";
        public static string end_point_get_name_search_by_id = $"{service_end_point}/api/v1/GetNameSearchesBySearchID";
        public static string end_point_approve_search = $"{service_end_point}/api/v1/ApproveSearched";


        //companyApplications
        public static string end_point_post_company_application = $"{service_end_point}/api/v1/postCompanyApplication";
        public static string end_point_post_company_application_memo = $"{service_end_point}/api/v1/postCompanyApplicationMemo";
        public static string end_point_post_company_application_articles = $"{service_end_point}/api/v1/postCompanyApplicationArticles";
        public static string end_point_post_company_application_members = $"{service_end_point}/api/v1/postCompanyApplicationMembers";
        public static string end_point_get_company_application_by_search_ref = $"{service_end_point}/api/v1/GetCompanyApplicationBySearcRef";
        public static string end_point_paid_company_application = $"{service_end_point}/api/v1/PaidCompanyApplication";
        public static string end_point_get_company_application = $"{service_end_point}/api/v1/GetCompanyApplication";
        public static string end_point_assign_company_for_examination = $"{service_end_point}/api/v1/AssignCompanyForExamination";
        public static string end_point_get_company_application_by_application_ref = $"{service_end_point}/api/v1/GetCompanyApplicationByApplicationRef";

        //Queries endpoints..
        public static string end_point_post_address_has_query = $"{service_end_point}/api/v1/PostAddressHasQuery";
        public static string end_point_post__has_query = $"{service_end_point}/api/v1/PostHasQuery";// End point for queries
        public static string end_point_get_queries = $"{service_end_point}/api/v1/GetApplicationQueries";// End point to get all queries
        public static string end_point_post_update_registeredoffice = $"{service_end_point}/api/v1/UpdateRegisteredOffice";// End point to get all queries
        public static string end_point_post_update_companyinfo = $"{service_end_point}/api/v1/UpdateCompanyInfo";// update company info queries
        public static string end_point_resolveQuery_companyinfo = $"{service_end_point}/api/v1/ResolveQuery";// update company info queries
        public static string end_point_Update_memoInfo = $"{service_end_point}/UpdateMemoOfArticles";// update company info queries
        public static string end_point_resubmit_application = $"{service_end_point}/CompanyApplicationResubmission";// Resubmit Company Application



        //Payments
        public static string end_point_payments_credit = $"{service_end_point}/Payments/Credit/";
        public static string end_point_payment = $"{service_end_point}/Payments/";
        public static string end_point_payments_credits = $"{service_end_point}/Payments/Credits/";
        public static string end_point_payments_record_payment= $"{service_end_point}​/Payments/RecordPayment/";
        public static string end_point_payments_purchase_credits = $"{service_end_point}/Payments/PurchaseCredits";
        public static string toBePaid { get; set; }

        public static async Task<string> count_un_read_notifications(string billercode)
        {
            var client = new HttpClient();
            var db = new dbContext();
            var notifications = await client.GetAsync($"{end_point_countUnReadNotificationsForBiller}?billercode={billercode}").Result.Content.ReadAsStringAsync();
            return notifications;
        }
    }

}
