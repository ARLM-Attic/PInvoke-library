using System;
#if NET1
#else
using System.Collections.Generic;
#endif
using System.Text;
using Discuz.Config;

namespace Discuz.Web.Services.API.Actions
{
    public abstract class ActionBase
    {
        private int uid = -1;
        private string secret;
        private string api_key;
        private FormatType format;
        private DNTParam[] parameters;
        private ApplicationInfo app;
        private int error_code;
        private float last_call_id;
        private float call_id;
        private string signature;

        internal int Uid
        {
            get { return uid; }
            set { uid = value; }
        }

        internal string Secret
        {
            get { return secret; }
            set { secret = value; }
        }

        internal string ApiKey
        {
            get { return api_key; }
            set { api_key = value; }
        }

        internal FormatType Format
        {
            get { return format; }
            set { format = value; }
        }

        internal ApplicationInfo App
        {
            get { return app; }
            set { app = value; }
        }

        internal DNTParam[] Params
        {
            get { return parameters; }
            set { parameters = value; }
        }

        internal int ErrorCode
        {
            get { return error_code; }
            set { error_code = value; }
        }

        internal float LastCallId
        {
            get { return last_call_id; }
            set { last_call_id = value; }
        }

        internal float CallId
        {
            get { return call_id; }
            set { call_id = value; }
        }

        internal string Signature
        {
            get { return signature; }
            set { signature = value; }
        }

        internal Object GetParam(string key)
        { 
            if (parameters == null)
                return null;
            foreach (DNTParam p in parameters)
            {
                if (p.Name.ToLower() == key.ToLower())
                {
                    return p.Value;
                }
            }
            return null;
        }
    }
}
