using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POS_API.Models
{   
    // enumeration represent the status 
    public enum OperationStatus
    {
            Success,
            Failure,
            Pending,
            Error
    }
        // enumeration represent the discription
    public enum OperationDescription
    {
        Created,
        Updated,
        Deleted,
        NotFound,
        Error,
        Success,
        InvalidCredentials
    }

    // view model class representing response  from the api
    public class ResponseViewModel
    {
        public OperationStatus Status { get; set; }
        public OperationDescription Description { get; set; }
        public string Message { get; set; }
        public int UserID { get; internal set; }
    }
}