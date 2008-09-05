using System;
#if NET1
#else
using System.Collections.Generic;
#endif
using System.Text;

namespace Discuz.Web.Services.API
{
    public enum FormatType
    { 
        XML,
        JSON
    }

    public enum ErrorType
    {
        API_EC_UNKNOWN = 1, //An unknown error occurred. Please resubmit the request. 
        API_EC_SERVICE = 2, //The service is not available at this time.
        API_EC_METHOD = 3, //Unknown method 
        API_EC_TOO_MANY_CALLS = 4, //The application has reached the maximum number of requests allowed. More requests are allowed once the time window has completed. 
        API_EC_BAD_IP = 5, //The request came from a remote address not allowed by this application. 
        //API_EC_HOST_API = 6, //This method must run on  
        API_EC_PARAM = 100, //One of the parameters specified was missing or invalid.
        API_EC_APPLICATION = 101, //The API key submitted is not associated with any known application.
        API_EC_SESSIONKEY = 102, //The session key was improperly submitted or has reached its timeout. Direct the user to log in again to obtain another key.
        API_EC_CALLID = 103, //The submitted call_id was not greater than the previous call_id for this session. 
        API_EC_SIGNATURE = 104, //Incorrect signature.
    }
}
