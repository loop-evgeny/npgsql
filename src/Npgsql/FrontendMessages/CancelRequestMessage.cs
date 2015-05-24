﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Npgsql.FrontendMessages
{
    class CancelRequestMessage : SimpleFrontendMessage
    {
        internal int BackendProcessId { get; set; }
        internal int BackendSecretKey { get; set; }

        const int CancelRequestCode = 1234 << 16 | 5678;

        internal CancelRequestMessage(int backendProcessId, int backendSecretKey)
        {
            BackendProcessId = backendProcessId;
            BackendSecretKey = backendSecretKey;
        }

        internal override int Length
        {
            get { return 16; }
        }

        internal override void Write(NpgsqlBuffer buf)
        {
            Contract.Requires(BackendProcessId != 0);

            buf.WriteInt32(Length);
            buf.WriteInt32(CancelRequestCode);
            buf.WriteInt32(BackendProcessId);
            buf.WriteInt32(BackendSecretKey);
        }

        public override string ToString()
        {
            return String.Format("[CancelRequest(BackendProcessId={0})]", BackendProcessId);
        }
    }
}
